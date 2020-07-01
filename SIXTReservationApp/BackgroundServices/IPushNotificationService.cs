using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SIXTReservationApp.Controllers;
using SIXTReservationApp.Hubs;
using SIXTReservationApp.Models;
using SIXTReservationApp.SignalR;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Helper;
using SIXTReservationBL.Hendlers;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SIXTReservationApp.BackgroundServices
{
    internal interface IPushNotificationService
    {
        Task DoWork();
    }

    internal class PushNotificationService : IPushNotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Notify notificationHub;
        IWebHostEnvironment hostEnvironment;
        public IConfiguration Configuration { get; }
        public PushNotificationService(
            IUnitOfWork unitOfWork,
            Notify webPush,
              IWebHostEnvironment _hostEnvironment,
              Notify notify,
              IConfiguration configuration
              )
        {
            this.unitOfWork = unitOfWork;
            notificationHub = webPush;
            hostEnvironment = _hostEnvironment;
            Configuration = configuration;
        }

        public async Task DoWork2(DateTime fromDate, DateTime toDate, CancellationToken stoppingToken)
        {
            //var notifications = await _unitOfWork.NotificationBL.GetNotificationsNeedToBeSent(fromDate, toDate);
            //var ss = _unitOfWork.SystemNotificationSettingsBL.GetAll().FirstOrDefault();
            //for (int i = 0; i < notifications.Count; i++)
            //{
            //    var notification = notifications[i];
            //    var us = notification.Recipient
            //                                   .UserNotificationSettings
            //                                   .FirstOrDefault(/*p => p.NotificationEventId == notification.NotificationEventId*/);

            //    if (ss.EnableWebPush == true && us.IsWebPushEnabled == true)
            //    {
            //        await _webPush.SendNotification(notification.Recipient.AppUserId, notification.Body);
            //    }
            //    if (ss.EnableEmails == true && us.IsEmailEnabled == true)
            //    {
            //        // to be done
            //    }
            //    if (ss.EnableMobilePush == true && us.IsMobilePushEnabled == true)
            //    {
            //        // to be done
            //    }
            //    if (!string.IsNullOrEmpty(notification.Recipient.MobileNumber))
            //    {
            //        if (ss.EnableSms == true && us.IsSmsenabled == true)
            //        {
            //            await _smsService.TrySendMessage(notification.Body, notification.Recipient.MobileNumber);
            //        }
            //        if (ss.EnableWhatsapp == true && us.IsWhatsappEnabled == true)
            //        {
            //            await _whatsappService.TrySendMessage(notification.Body, notification.Recipient.MobileNumber);
            //        }
            //    }
            //}

            //await _unitOfWork.NotificationBL.UpdateNotificationSentRange(notifications);
            // _unitOfWork.Complete();
        }

        public async Task DoWork()
        {
            await SavingUploadedFiles();
            await SendNotAssignNotifications();
            await SendNoActionTakenNotifications();
            await SendNotAssignNotificationsCBySixt();
            await SendNoActionTakenNotificationsCBySixt();
            await SendNotAssignNotificationsNoShow();
            await SendNoActionTakenNotificationsNoShow();
            await SendNotAssignNotificationsOpen();
            await SendNoActionTakenNotificationsOpen();
            await SendNotAssignNotificationsOpenConfirmed();
            await SendNoActionTakenNotificationsOpenConfirmed();
            await SendEmailToCustomerOpenConfirmed();
            await SendNotificationToBranchMangerConfirmed();
            await SendNotAssignNotificationsToBranchMangementOpenConfirmed();
            await SendNoActionTakenNotificationsToBranchMangementOpenConfirmed();
            await SendNotificationToBranchMangerConfirmedWhenPickUpDate();
        }

        #region Uploading new File
        public async Task SavingUploadedFiles() =>
            await Task.Run(() =>
                    {
                        var NotParsedUploadsToSave = unitOfWork.UploadLogBL.Find(u => u.UploadStatus == (int)UploadStatusEnum.Uploaded);
                        for (int i = 0; i < NotParsedUploadsToSave.Count; i++)
                        {
                            var uploadLog = NotParsedUploadsToSave[i];
                            try
                            {
                                var webRoot = hostEnvironment.WebRootPath;
                                string filePath = System.IO.Path.Combine(webRoot, uploadLog.FilePath);
                                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                                {
                                    NotifyUserWithMessage(uploadLog.UserId.GetValueOrDefault(), "Attempting to Parse Uploaded File");
                                    uploadLog.ParsingStartTime = DateTime.Now;
                                    var fileName = Path.GetFileName(filePath);
                                    string ext = Path.GetExtension(filePath);
                                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        IWorkbook workbook;

                                        if (ext == ".xlsx")
                                        {
                                            workbook = new XSSFWorkbook(stream);
                                        }
                                        else
                                        {
                                            workbook = new HSSFWorkbook(stream);
                                        }
                                        var NumberOfSheets = workbook.NumberOfSheets;
                                        ISheet sheet = workbook.GetSheetAt(0); //  first work sheet only 
                                        IRow headerRow = sheet.GetRow(0);
                                        var hearderRowCount = headerRow.Cells.Count();
                                        var lastRowNum = sheet.LastRowNum;
                                        List<string> ErrorCodes = new List<string>();
                                        string errorMsg = string.Empty;
                                        int[] result = SaveUploadfileToDB(sheet, lastRowNum, uploadLog, ref ErrorCodes, ref errorMsg);
                                        string msg = string.Empty;
                                        if (result[0] == -1 && result[1] == -1)  // error occured 
                                        {
                                            uploadLog.SavedRecordNum = 0;
                                            uploadLog.FailedRecordNum = 0;
                                            uploadLog.ParsingCompleteTime = DateTime.Now;
                                            uploadLog.UploadStatus = (int)UploadStatusEnum.Failed;
                                            uploadLog.FailureMsg = errorMsg.Substring(0, 499);
                                        }
                                        else
                                        {
                                            msg = "File Parsed Successfully with " + result[0] + " records saved";
                                            if (result[1] > 0)
                                            {
                                                msg += " , and " + result[1] + " records failed";
                                            }
                                            uploadLog.SavedRecordNum = result[0];
                                            uploadLog.FailedRecordNum = result[1];
                                            uploadLog.ParsingCompleteTime = DateTime.Now;
                                            uploadLog.UploadStatus = (int)UploadStatusEnum.Parsed;
                                        }
                                        if (ErrorCodes?.Count > 0)
                                        {
                                            string webPath = System.IO.Path.Combine(@"\UploadErrorLog", uploadLog.Id + ".txt");
                                            string path = System.IO.Path.Combine(webRoot, "UploadErrorLog");
                                            Directory.CreateDirectory(path);
                                            string errorFilepath = Path.Combine(path, uploadLog.Id + ".txt");
                                            Helper.SaveStringToFile(JsonConvert.SerializeObject(ErrorCodes), errorFilepath);
                                            uploadLog.ErrorLogPath = webPath;
                                        }
                                        unitOfWork.UploadLogBL.Update(uploadLog);
                                        int updated = unitOfWork.Complete();

                                        NotifyUserWithMessage(uploadLog.UserId.GetValueOrDefault(), msg);
                                        var notify = new Notification
                                        {
                                            ToUser = uploadLog.UserId,
                                            IsSeen = false,
                                            NotificationText = msg,
                                            Status = false,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Uploads/UploadsIndex",
                                            NotificationType = (int)NotificationType.UploadLog
                                        };
                                        unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);

                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                uploadLog.UploadStatus = (int)UploadStatusEnum.Failed;
                                uploadLog.FailureMsg = e.Message.Substring(0, 499);
                                unitOfWork.UploadLogBL.Update(uploadLog);
                                int updated = unitOfWork.Complete();
                            }
                        }
                    });
        protected int[] SaveUploadfileToDB(ISheet sheet, int lastRowNum, UploadLog uploadLog, ref List<string> errorCodes, ref string errorMsg)
        {

            int rowIndex = 0;
            int SavedRecords = 0;
            int FailedRecords = 0;
            int[] Result = new int[2];
            try
            {
                int uploadLogId = uploadLog.Id;
                int userId = uploadLog.UserId.GetValueOrDefault();
                var rateSegments = unitOfWork.RateSegmentBL.GetAll();
                var rateSegmentCategories = unitOfWork.RateSegmentCategoryBL.GetAll();

                int SavedCount = 0;
                for (int i = 0; i <= lastRowNum; i++)
                {
                    var systemBranches = unitOfWork.BranchBL.GetAll();
                    IRow row = sheet.GetRow(i);
                    // skip header row
                    if (rowIndex++ == 0 || row == null) continue;
                    var rowCells = row.Cells;
                    try
                    {
                        var type = rowCells[0].CellType.ToString();
                        string reservationNo = "";
                        Reservation reservation = new Reservation();

                        if (!string.IsNullOrWhiteSpace(rowCells[0].ToString()))
                        {
                            reservation.ReservationNum = long.Parse(rowCells[0].ToString());
                            reservationNo = reservation.ReservationNum.ToString();
                        }
                        else continue;

                        if (!string.IsNullOrWhiteSpace(rowCells[1].ToString()))
                        {
                            try { reservation.NumberOfReservations = int.Parse(rowCells[1].ToString()); } catch (Exception e) { }
                        }
                        // else continue;

                        if (!string.IsNullOrWhiteSpace(rowCells[5].ToString()))
                        {
                            DateTime date = DateTime.ParseExact(rowCells[5].ToString().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            //  "MM.dd.yyyy"                          
                            reservation.BookingDate = date;
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[6].ToString())) // F Booking Date
                        {
                            try
                            {
                                string[] hours = rowCells[6].ToString().Split(' ');
                                reservation.ReservationHour = hours[1] == "AM" ? int.Parse(hours[0]) : int.Parse(hours[0]) + 12;
                            }
                            catch (Exception e) { }
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[9].ToString())) // J Pick Up Date
                        {
                            DateTime date = DateTime.ParseExact(rowCells[9].ToString().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            reservation.PickUpDate = date;
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                        if (!string.IsNullOrWhiteSpace(rowCells[10].ToString())) // H Pick Up Hour
                        {
                            try
                            {
                                string[] hours = rowCells[10].ToString().Split(' ');
                                reservation.PickUpHour = hours[1] == "AM" ? int.Parse(hours[0]) : int.Parse(hours[0]) + 12;
                            }
                            catch (Exception e) { }
                        }
                        if (!string.IsNullOrWhiteSpace(rowCells[11].ToString())) // L Pick Up Weekday
                        {
                            reservation.PickUpweekDay = rowCells[11].ToString();
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[22].ToString()))
                        {
                            try { reservation.RentalDays = int.Parse(rowCells[22].ToString()); } catch (Exception e) { }
                        }
                        // else continue;
                        if (!string.IsNullOrWhiteSpace(rowCells[26].ToString()))
                        {
                            try { reservation.CancelledAfterBookingInDays = int.Parse(rowCells[26].ToString()); } catch (Exception e) { }
                        }
                        // else continue;
                        if (!string.IsNullOrWhiteSpace(rowCells[27].ToString()))
                        {
                            try { reservation.CancelledBeforeBickUpInDays = int.Parse(rowCells[27].ToString()); } catch (Exception e) { }
                        }
                        // else continue;

                        if (!string.IsNullOrWhiteSpace(rowCells[34].ToString())) // AI Pick Up Branch Code
                        {
                            var branch = systemBranches?.FirstOrDefault(b => b.Code.ToLower() == rowCells[34].ToString().ToLower());
                            if (branch != null)
                            {
                                reservation.PickUpBranchId = branch.Id;
                            }
                            else
                            {
                                errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue;
                                //unitOfWork.BranchBL.Add(new Branch()
                                //{
                                //    Code = rowCells[34].ToString().Trim(),
                                //    Name = rowCells[35].ToString().Trim()
                                //});
                            }
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[40].ToString())) // AO Drop Off Date
                        {
                            DateTime date = DateTime.ParseExact(rowCells[40].ToString().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            reservation.DropOffDate = date;

                            var dayofweek = date.DayOfWeek;
                            var dayofwekno = (int)date.DayOfWeek;
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[41].ToString())) // AP Drop Off Hour
                        {
                            try
                            {
                                string[] hours = rowCells[41].ToString().Split(' ');
                                reservation.DropOffHour = hours[1] == "AM" ? int.Parse(hours[0]) : int.Parse(hours[0]) + 12;
                            }
                            catch (Exception e) { }
                        }

                        if (!string.IsNullOrWhiteSpace(rowCells[42].ToString())) // AQ Drop Off Weekday
                        {
                            reservation.DropOffweekDay = rowCells[42].ToString();
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[51].ToString())) // AZ Drop Off Branch Code
                        {
                            var branch = systemBranches?.FirstOrDefault(b => b.Code.ToLower() == rowCells[51].ToString().ToLower());
                            if (branch != null)
                            {
                                reservation.DropOffBranchId = branch.Id;
                            }
                            else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                            //else
                            //    unitOfWork.BranchBL.Add(new Branch()
                            //    {
                            //        Code = rowCells[51].ToString().Trim(),
                            //        Name = rowCells[52].ToString().Trim()
                            //    });

                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[58].ToString())) //BG Vehicle ACRISS
                        {
                            reservation.VehicleAcriss = rowCells[58].ToString();
                        }

                        if (!string.IsNullOrWhiteSpace(rowCells[65].ToString())) //BN Reservation Source Channel 1
                        {
                            reservation.ReservationSourceChannel1 = rowCells[65].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(rowCells[66].ToString())) //BO Reservation Source Channel 2
                        {
                            reservation.ReservationSourceChannel2 = rowCells[66].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(rowCells[67].ToString())) //BP Reservation Source Channel 3
                        {
                            reservation.ReservationSourceChannel3 = rowCells[67].ToString();
                        }


                        if (!string.IsNullOrWhiteSpace(rowCells[68].ToString())) // BQ Business Segment
                        {
                            //var segment = rateSegments?.FirstOrDefault(b => b.RateSegmentName.ToLower() == rowCells[68].ToString().ToLower());
                            //if (segment != null)
                            //{
                            //    reservation.BusinessSegmentId = segment.Id;
                            //}
                            //else { ErrorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                            reservation.BusinessSegmentText = rowCells[68].ToString();
                        }

                        if (!string.IsNullOrWhiteSpace(rowCells[71].ToString())) // BT Rate Segment Category
                        {
                            var segmentCategory = rateSegmentCategories?.FirstOrDefault(b => b.RateSegmentCategoryName.ToLower() == rowCells[71].ToString().ToLower());
                            if (segmentCategory != null)
                            {
                                reservation.RateSegmentCategory = segmentCategory.Id;
                            }
                            else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                        }


                        if (!string.IsNullOrWhiteSpace(rowCells[75].ToString())) //BX One Way Reservation
                        {
                            reservation.OneWayReservation = rowCells[75].ToString().ToLower() == "yes" ? true : false;
                        }
                        if (!string.IsNullOrWhiteSpace(rowCells[77].ToString())) //BZ On Request
                        {
                            reservation.OnRequest = rowCells[77].ToString().ToLower() == "yes" ? true : false;
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[78].ToString())) //CA   Request level
                        {
                            reservation.RequestLevel = rowCells[78].ToString();
                            int requestLevel = GetRequestLevel(rowCells[78].ToString());
                            if (requestLevel >= 0)
                                reservation.RequestLevelId = requestLevel;

                            else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[80].ToString())) //CC  reservation Status 
                        {
                            int status = unitOfWork.ReservationStatusBL.GetReservationStatusOffLine(rowCells[80].ToString());
                            if (status >= 0)
                                reservation.ReservationStatusId = status;
                            else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[82].ToString())) // CE Cancelled Date
                        {
                            if (rowCells[82].ToString().Trim() != "-")
                            {
                                DateTime date = DateTime.ParseExact(rowCells[82].ToString().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                                reservation.CancelledDate = date;
                            }

                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[84].ToString())) // CG no show Date
                        {
                            if (rowCells[84].ToString().Trim() != "-")
                            {
                                DateTime date = DateTime.ParseExact(rowCells[84].ToString().Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                                reservation.NoShowDate = date;
                            }

                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }


                        if (!string.IsNullOrWhiteSpace(rowCells[85].ToString())) // Ch Rental Agreement Number 
                        {
                            reservation.RentalAgreementNumber = long.Parse(rowCells[85].ToString().Trim());

                        }
                        // else { ErrorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[93].ToString())) // CP CD Number
                        {
                            reservation.Cdnumber = long.Parse(rowCells[93].ToString());
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[94].ToString())) // CQ Customer Name
                        {
                            if (rowCells[94].ToString() != "N/A")
                            {
                                reservation.DriverName = rowCells[94].ToString();
                            }
                        }
                        //else { ErrorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        // Add Driver Name as Customer Name  – Column CV
                        if (!string.IsNullOrWhiteSpace(rowCells[99].ToString())) // CV Driver Name
                        {
                            if (rowCells[99].ToString() != "N/A")
                            {
                                var customer = unitOfWork.CustomerBL.FindOne(c => c.Name.ToLower() == rowCells[99].ToString().ToLower());
                                if (customer != null)
                                {
                                    reservation.CustomerId = customer.Id;
                                }
                                else
                                {
                                    int customerId = unitOfWork.CustomerBL.SavingNewCustomer(new Customer() { Name = rowCells[99].ToString(), CreatedDate = DateTime.Now });
                                    reservation.CustomerId = customerId > 0 ? customerId : (int?)null;
                                } // SaveNewCustomer
                            }
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[100].ToString())) // CW DriverCountry
                        {
                            reservation.DriverCountry = rowCells[100].ToString();
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        if (!string.IsNullOrWhiteSpace(rowCells[104].ToString())) // CZ Customer Card Indicator 
                        {
                            reservation.CustomerCardIndicator = rowCells[104].ToString();
                        }
                        else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }

                        DateTime now = DateTime.Now;
                        // check exist and update  
                        // assume that there is only one record with the same  reseration number 
                        var existingRecord = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == reservation.ReservationNum);
                        var existingRecordHistory = unitOfWork.ReservationHistoryBL.FindOne(r => r.ReservationNum == reservation.ReservationNum);
                        if (existingRecord != null)
                        {
                            // add current existing one as history 
                            if (existingRecord.ReservationStatusId != reservation.ReservationStatusId || existingRecordHistory.ReservationStatusId != reservation.ReservationStatusId)
                            {
                                var newHistory = unitOfWork.ReservationHistoryBL.GetHistoryObject(existingRecord, userId);
                                newHistory.DateFrom = existingRecord.CreationDate;
                                newHistory.DateTo = now;
                                unitOfWork.ReservationHistoryBL.Add(newHistory);

                                // update exitsig reservation

                                int id = existingRecord.Id; DateTime? creationdate = existingRecord.CreationDate;
                                // reservation.CopyProperties<Reservation>(ref existingRecord);
                                unitOfWork.ReservationBL.CopyObject(ref existingRecord, reservation);
                                existingRecord.CreationDate = creationdate;

                                unitOfWork.ReservationBL.Update(existingRecord);
                                unitOfWork.NotificationBL.UpdateDeletedNotification(reservation.ReservationNum);
                                //StepHandler step = new StepHandler(unitOfWork);
                                //CancelledByCustomerNotification builder = (CancelledByCustomerNotification)step.InitiateSteperCByCustomer((int)existingRecord.ReservationNum, 1);
                                //builder.PerformAction(new NotificationVM { });

                                if (unitOfWork.Complete() > 0)
                                {
                                    PerformReservationAction(reservation);
                                    if (unitOfWork.Complete() > 0)
                                    {
                                        // log failed records here 
                                    }
                                    SavedRecords++;
                                }
                                else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                            }
                            //else do nothing 

                        }
                        else
                        {
                            reservation.CreationDate = now;
                            unitOfWork.ReservationBL.Add(reservation);

                            if (unitOfWork.Complete() > 0)
                            {
                                PerformReservationAction(reservation);
                                SavedRecords++;
                                if (unitOfWork.Complete() > 0)
                                {
                                    // log failed records here 
                                }
                            }
                            //else { errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue; }
                        }


                        //if (SavedCount == 100)
                        //{
                        //    unitOfWork.Complete();
                        //    SavedCount = 0;
                        //}
                        //else SavedCount++;

                    }
                    catch (Exception e)
                    {

                        errorCodes.Add(rowCells[0].ToString()); FailedRecords++; continue;
                    }

                }
                Result[0] = SavedRecords;
                Result[1] = FailedRecords;

            }
            catch (Exception e)
            {
                Result[0] = -1;
                Result[1] = -1;
                errorMsg = e.Message;

            }
            return Result;
        }

        protected int GetRequestLevel(string requestLevel)
        {
            switch (requestLevel.ToLower())
            {
                case "0": return 0;
                case "1 - onrequest - open": return 1;
                case "2 - onrequest - confirmed": return 2;
                case "3 - onrequest - declined": return 3;
                default: return -1;
            }


        }

        #endregion
        protected void PerformReservationAction(Reservation obj)
        {
            // not complted 
            switch (obj.ReservationStatusId)
            {
                case 1:  // CancelledByCustomer 
                    {
                        StepHandler step = new StepHandler(unitOfWork);
                        CancelledByCustomerNotification builder = (CancelledByCustomerNotification)step
                                                                      .InitiateSteperCByCustomer(obj.ReservationNum, (int)SixtCancellationStatusEnum.CancelledByCustomer);
                        if (builder != null)
                        {

                            //builder.CurrentStep = (int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification;
                            builder.ReservationNo = obj.ReservationNum;
                            builder.ReservationStatus = obj.ReservationStatusId.Value;
                            builder.CurrentStep = step.StepId;
                            builder.PerformAction(new NotificationVM { ReservationLogId = step.SavedLogId });
                            // NotifyUserWithMessage();
                        }

                    }
                    break;
                case 2:     //cancelledBySixt
                    {
                        StepHandler step = new StepHandler(unitOfWork);
                        CancelledBySixtNotification builder = (CancelledBySixtNotification)step
                                                                      .InitiateSteperCBySixt(obj.ReservationNum, (int)SixtCancellationStatusEnum.CancelledBySixt);
                        if (builder != null)
                        {

                            //builder.CurrentStep = (int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification;
                            builder.ReservationNo = obj.ReservationNum;
                            builder.ReservationStatus = obj.ReservationStatusId.Value;
                            builder.CurrentStep = step.StepId;
                            builder.PerformAction(new NotificationVM { ReservationLogId = step.SavedLogId });
                            // NotifyUserWithMessage();
                        }

                    }
                    break;

                case 3:   //NoShow
                    {
                        StepHandler step = new StepHandler(unitOfWork);
                        NoShowNotification builder = (NoShowNotification)step
                                                                      .InitiateSteperNoShow(obj.ReservationNum, (int)SixtCancellationStatusEnum.NoShow);
                        if (builder != null)
                        {

                            builder.ReservationNo = obj.ReservationNum;
                            builder.ReservationStatus = obj.ReservationStatusId.Value;
                            builder.CurrentStep = step.StepId;
                            builder.PerformAction(new NotificationVM { ReservationLogId = step.SavedLogId });
                            // NotifyUserWithMessage();
                        }

                    }
                    break;

                case 4: //Open
                    {
                        // if status is open - confirmed
                        if (obj.RequestLevelId == (int)RequestLevelEnum.OnRequest_confirmed || obj.RequestLevelId == (int)RequestLevelEnum.Zero)
                        {
                            StepHandler step = new StepHandler(unitOfWork);
                            OpenContactAgentNotification builder = (OpenContactAgentNotification)step
                                                                          .InitiateSteperOpenConfirm(obj.ReservationNum, (int)SixtCancellationStatusEnum.Open);
                            if (builder != null)
                            {

                                builder.ReservationNo = obj.ReservationNum;
                                builder.ReservationStatus = obj.ReservationStatusId.Value;
                                builder.CurrentStep = step.StepId;
                                builder.PerformAction(new CAgentOpenNotification { ReservationLogId = step.SavedLogId, ReservationId = obj.ReservationNum.ToString() });
                                // NotifyUserWithMessage();
                            }

                        }
                        // if status is open only
                        else if (obj.RequestLevelId == (int)RequestLevelEnum.OnRequest_open)
                        {
                            StepHandler step = new StepHandler(unitOfWork);
                            OpenBranchNotification builder = (OpenBranchNotification)step
                                                                          .InitiateSteperOpen(obj.ReservationNum, (int)SixtCancellationStatusEnum.Open);
                            if (builder != null)
                            {

                                builder.ReservationNo = obj.ReservationNum;
                                builder.ReservationStatus = obj.ReservationStatusId.Value;
                                builder.CurrentStep = step.StepId;
                                builder.PerformAction(new NotificationVM { ReservationLogId = step.SavedLogId });
                                // NotifyUserWithMessage();
                            }

                        }
                        //if open -decliend
                        else if (obj.RequestLevelId == (int)RequestLevelEnum.OnRequest_declined)
                        {
                            StepHandler step = new StepHandler(unitOfWork);
                            OpenFormActionDecliend builder = (OpenFormActionDecliend)step
                                                                          .InitiateSteperOpenDecliend(obj.ReservationNum, (int)SixtCancellationStatusEnum.Open);
                            if (builder != null)
                            {

                                builder.ReservationNo = obj.ReservationNum;
                                builder.ReservationStatus = obj.ReservationStatusId.Value;
                                builder.CurrentStep = step.StepId;
                                builder.PerformAction(null);
                                // NotifyUserWithMessage();
                            }
                        }

                    }
                    break;

                case 5: { } break;

                default:
                    break;
            }


        }

        private async void NotifyUserWithMessage(int uploadUserId, string message)
        {
            try
            {
                await notificationHub.SendNotification(uploadUserId, message);
            }
            catch (Exception e)
            {


            }


        }


        #region Push notification for not assigned - Cancelled by customer

        public async Task SendNotAssignNotifications() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledByCustomer;
            var Step1 = (int)CacelledByCustomerStepEnum.ContactCenterManagmentNotification;
            var Step2 = (int)CacelledByCustomerStepEnum.NoAssignmentNotification;
            var Step3 = (int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step1 &&
                                                                                                        r.IsDone == true &&
                                                                                                        r.DiffDateNotifyAssign >= 24
                                                                                                        ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                    // new ReservationStepsLog has been created on initilaize steper 
                    for (int j = 0; j < ToUsers.Count(); j++)
                    {
                        var falg = j;
                        if (JobTitles?.Count > 0)
                        {
                            for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                            {
                                if (falg == 0)
                                {

                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    var newLog = new ReservationStepsLog
                                    {
                                        ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                        StepId = Step2,
                                        CreationDate = DateTime.Now,
                                        IsDone = true,
                                        ReservationStatus = ReservationStatusId,
                                    };
                                    unitOfWork.ReservationStepLogBL.Add(newLog);
                                }
                                var notify = new Notification
                                {
                                    ToUser = ToUsers[j].Id,
                                    IsSeen = false,
                                    NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                    Status = false,
                                    ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledByCustomer,
                                    ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                    ReservationLogId = NotifyNotAssigned[i].LogId,
                                    CreateDate = DateTime.Now,
                                    UrlNotification = "/Reservation/CByCustomerSteps/" + NotifyNotAssigned[i].ReservationNo,
                                    NotificationType = (int)NotificationType.CancelledByCustomer,
                                    GroupId = (int)NotificationGroupType.NoAssignmentNotification,
                                    IsDeleted = false,
                                };
                                notifications.Add(notify);
                                //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                unitOfWork.NotificationBL.Add(notify);

                                if (unitOfWork.Complete() > 0)
                                {
                                    // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                }

                            }

                        }

                    }
                }



            }


        });

        #endregion

        #region Push notification for no action taken - Cancelled by customer
        public async Task SendNoActionTakenNotifications() => await Task.Run(() =>
             {
                 var notifications = new List<Notification>();
                 var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledByCustomer;
                 var Step3 = (int)CacelledByCustomerStepEnum.ContactCenterAgentAssignment;
                 var Step4 = (int)CacelledByCustomerStepEnum.NoFormsumbittedNotification;
                 var Step5 = (int)CacelledByCustomerStepEnum.AgentFormSumbitted;
                 // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
                 var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                                 r.ReservationStatus == ReservationStatusId &&
                                                                                                                 r.StepId == Step3
                                                                                                                 && r.IsDone == true
                                                                                                                 && r.DiffDateAssignSubmit >= 24
                                                                                                                 )
                                                                                                                // .GroupBy(r => r.ReservationId)
                                                                                                                // .Select(r => r.First()) 
                                                                                                                .ToList();
                 var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
                 if (AssignedReservations?.Count() > 0)
                 {
                     var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                                r.ReservationStatus == ReservationStatusId &&
                                                                                                                r.StepId == Step4 &&
                                                                                                                r.IsDone == true &&
                                                                                                                AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                                 .Select(r => r.ReservationNo)
                                                                                                                 .Distinct()
                                                                                                                 .ToList();



                     var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                                r.ReservationStatus == ReservationStatusId &&
                                                                                                                r.StepId == Step5 &&
                                                                                                                r.IsDone == false &&
                                                                                                                AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                               .Select(r => r.ReservationNo)
                                                                                                               .Distinct()
                                                                                                                .ToList();

                     var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                            NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                            !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                     FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                     if (FormNotSubmitted?.Count > 0)
                     {
                         //send notification to notification user's in setting
                         var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                                 u.ReservationStatusId == ReservationStatusId &&
                                                                                                                 u.ActionStep == Step4);
                         var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                         var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                         var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                         // new ReservationStepsLog has been created on initilaize steper 
                         for (int j = 0; j < ToUsers.Count(); j++)
                         {
                             var falg = j;

                             if (JobTitles?.Count > 0)
                             {

                                 for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                 {
                                     //set step 2 is done
                                     //create new stepLog 2 and set is done  
                                     if (falg == 0)
                                     {
                                         var newLog = new ReservationStepsLog
                                         {
                                             ReservationNo = FormNotSubmitted[i].ReservationNo,
                                             StepId = Step4,
                                             CreationDate = DateTime.Now,
                                             IsDone = true,
                                             ReservationStatus = ReservationStatusId,
                                         };
                                         unitOfWork.ReservationStepLogBL.Add(newLog);
                                     }
                                     var notify = new Notification
                                     {
                                         ToUser = ToUsers[j].Id,
                                         IsSeen = false,
                                         NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                         Status = false,
                                         ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledByCustomer,
                                         ReservationNo = FormNotSubmitted[i].ReservationNo,
                                         ReservationLogId = FormNotSubmitted[i].LogId,
                                         CreateDate = DateTime.Now,
                                         UrlNotification = "/Reservation/CByCustomerSteps/" + FormNotSubmitted[i].ReservationNo,
                                         NotificationType = (int)NotificationType.CancelledByCustomer,
                                         GroupId = (int)NotificationGroupType.NoFormSubmitNotification,
                                         IsDeleted = false,
                                     };
                                     notifications.Add(notify);
                                     //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                     unitOfWork.NotificationBL.Add(notify);

                                     if (unitOfWork.Complete() > 0)
                                     {
                                         // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                     }

                                 }

                             }

                         }

                     }


                 }


             });


        #endregion


        #region Push notification for not assigned - Cancelled by Sixt

        public async Task SendNotAssignNotificationsCBySixt() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var Step1 = (int)CacelledBySixtStepEnum.OperationReservationManagementNotification;
            var Step2 = (int)CacelledBySixtStepEnum.NoAssignmentNotification;
            var Step3 = (int)CacelledBySixtStepEnum.OperationReservationManagementAssignment;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step1 &&
                                                                                                        r.IsDone == true &&
                                                                                                        r.DiffDateNotifyAssign >= 24
                                                                                                        ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                    // new ReservationStepsLog has been created on initilaize steper 
                    for (int j = 0; j < ToUsers.Count(); j++)
                    {
                        var falg = j;
                        if (JobTitles?.Count > 0)
                        {
                            for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                            {
                                if (falg == 0)
                                {

                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    var newLog = new ReservationStepsLog
                                    {
                                        ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                        StepId = Step2,
                                        CreationDate = DateTime.Now,
                                        IsDone = true,
                                        ReservationStatus = ReservationStatusId,
                                    };
                                    unitOfWork.ReservationStepLogBL.Add(newLog);
                                }
                                var notify = new Notification
                                {
                                    ToUser = ToUsers[j].Id,
                                    IsSeen = false,
                                    NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                    Status = false,
                                    ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt,
                                    ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                    ReservationLogId = NotifyNotAssigned[i].LogId,
                                    CreateDate = DateTime.Now,
                                    UrlNotification = "/Reservation/CBySixtSteps/" + NotifyNotAssigned[i].ReservationNo,
                                    NotificationType = (int)NotificationType.CancelledBySixt,
                                    GroupId = (int)NotificationGroupType.NoAssignmentNotificationCBySixt,
                                    IsDeleted = false,
                                };
                                notifications.Add(notify);
                                //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                unitOfWork.NotificationBL.Add(notify);

                                if (unitOfWork.Complete() > 0)
                                {
                                    // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                }

                            }

                        }

                    }
                }



            }


        });

        #endregion

        #region Push notification for no action taken - Cancelled by Sixt
        public async Task SendNoActionTakenNotificationsCBySixt() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var Step3 = (int)CacelledBySixtStepEnum.OperationReservationManagementAssignment;
            var Step4 = (int)CacelledBySixtStepEnum.NoFormsumbittedNotification;
            var Step5 = (int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted;
            // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3
                                                                                                            && r.IsDone == true
                                                                                                            && r.DiffDateAssignSubmit >= 24
                                                                                                            )
                                                                                                           // .GroupBy(r => r.ReservationId)
                                                                                                           // .Select(r => r.First()) 
                                                                                                           .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step4 &&
                                                                                                           r.IsDone == true &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();



                var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step5 &&
                                                                                                           r.IsDone == false &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                          .Select(r => r.ReservationNo)
                                                                                                          .Distinct()
                                                                                                           .ToList();

                var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                       NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step4);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                    // new ReservationStepsLog has been created on initilaize steper 
                    for (int j = 0; j < ToUsers.Count(); j++)
                    {
                        var falg = j;

                        if (JobTitles?.Count > 0)
                        {

                            for (int i = 0; i < FormNotSubmitted.Count(); i++)
                            {
                                //set step 2 is done
                                //create new stepLog 2 and set is done  
                                if (falg == 0)
                                {
                                    var newLog = new ReservationStepsLog
                                    {
                                        ReservationNo = FormNotSubmitted[i].ReservationNo,
                                        StepId = Step4,
                                        CreationDate = DateTime.Now,
                                        IsDone = true,
                                        ReservationStatus = ReservationStatusId,
                                    };
                                    unitOfWork.ReservationStepLogBL.Add(newLog);
                                }
                                var notify = new Notification
                                {
                                    ToUser = ToUsers[j].Id,
                                    IsSeen = false,
                                    NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                    Status = false,
                                    ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt,
                                    ReservationNo = FormNotSubmitted[i].ReservationNo,
                                    ReservationLogId = FormNotSubmitted[i].LogId,
                                    CreateDate = DateTime.Now,
                                    UrlNotification = "/Reservation/CBySixtSteps/" + FormNotSubmitted[i].ReservationNo,
                                    NotificationType = (int)NotificationType.CancelledBySixt,
                                    GroupId = (int)NotificationGroupType.NoFormSubmitNotificationCBySixt,
                                    IsDeleted = false,
                                };
                                notifications.Add(notify);
                                //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                unitOfWork.NotificationBL.Add(notify);

                                if (unitOfWork.Complete() > 0)
                                {
                                    // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                }

                            }

                        }

                    }

                }


            }


        });


        #endregion


        #region Push notification for not assigned - No Show

        public async Task SendNotAssignNotificationsNoShow() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var Step1 = (int)NoShowStepEnum.ContactCenterManagmentNotification;
            var Step2 = (int)NoShowStepEnum.NoAssignmentNotification;
            var Step3 = (int)NoShowStepEnum.ContactCenterAgentAssignment;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step1 &&
                                                                                                        r.IsDone == true &&
                                                                                                        r.DiffDateNotifyAssign >= 24
                                                                                                        ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null)
                    {

                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;
                            if (JobTitles?.Count > 0)
                            {
                                for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                                {
                                    if (falg == 0)
                                    {

                                        //set step 2 is done
                                        //create new stepLog 2 and set is done  
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            StepId = Step2,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    var notify = new Notification
                                    {
                                        ToUser = ToUsers[j].Id,
                                        IsSeen = false,
                                        NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                        Status = false,
                                        ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow,
                                        ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                        ReservationLogId = NotifyNotAssigned[i].LogId,
                                        CreateDate = DateTime.Now,
                                        UrlNotification = "/Reservation/NoShowSteps/" + NotifyNotAssigned[i].ReservationNo,
                                        NotificationType = (int)NotificationType.NoShow,
                                        GroupId = (int)NotificationGroupType.NoAssignNotificationNoShow,
                                        IsDeleted = false,
                                    };
                                    notifications.Add(notify);
                                    //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                    unitOfWork.NotificationBL.Add(notify);

                                    if (unitOfWork.Complete() > 0)
                                    {
                                        // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                    }

                                }
                            }
                        }

                    }
                }



            }


        });

        #endregion

        #region Push notification for no action taken - No Show
        public async Task SendNoActionTakenNotificationsNoShow() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var Step3 = (int)NoShowStepEnum.ContactCenterAgentAssignment;
            var Step4 = (int)NoShowStepEnum.NoFormsumbittedNotification;
            var Step5 = (int)NoShowStepEnum.AgentFormSumbitted;
            // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3
                                                                                                            && r.IsDone == true
                                                                                                            && r.DiffDateAssignSubmit >= 24
                                                                                                            )
                                                                                                           // .GroupBy(r => r.ReservationId)
                                                                                                           // .Select(r => r.First()) 
                                                                                                           .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step4 &&
                                                                                                           r.IsDone == true &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();



                var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step5 &&
                                                                                                           r.IsDone == false &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                          .Select(r => r.ReservationNo)
                                                                                                          .Distinct()
                                                                                                           .ToList();

                var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                       NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step4);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {

                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;

                            if (JobTitles?.Count > 0)
                            {

                                for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                {
                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    if (falg == 0)
                                    {
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            StepId = Step4,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    var notify = new Notification
                                    {
                                        ToUser = ToUsers[j].Id,
                                        IsSeen = false,
                                        NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                        Status = false,
                                        ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow,
                                        ReservationNo = FormNotSubmitted[i].ReservationNo,
                                        ReservationLogId = FormNotSubmitted[i].LogId,
                                        CreateDate = DateTime.Now,
                                        UrlNotification = "/Reservation/NoShowSteps/" + FormNotSubmitted[i].ReservationNo,
                                        NotificationType = (int)NotificationType.NoShow,
                                        GroupId = (int)NotificationGroupType.NoFormSubmitNotificationNoShow,
                                        IsDeleted = false,
                                    };
                                    notifications.Add(notify);
                                    //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                    unitOfWork.NotificationBL.Add(notify);

                                    if (unitOfWork.Complete() > 0)
                                    {
                                        // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                    }

                                }
                            }
                        }

                    }

                }


            }


        });


        #endregion


        #region Push notification for not assigned - Open 

        public async Task SendNotAssignNotificationsOpen() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step1 = (int)OpenStepEnum.BranchManagmentNotification;
            var Step2 = (int)OpenStepEnum.NoAssignmentNotification;
            var Step3 = (int)OpenStepEnum.BranchAgentAssignment;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step1 &&
                                                                                                        r.IsDone == true &&
                                                                                                        r.DiffDateNotifyAssign >= 24
                                                                                                        ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {

                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;
                            if (JobTitles?.Count > 0)
                            {
                                for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                                {
                                    if (falg == 0)
                                    {

                                        //set step 2 is done
                                        //create new stepLog 2 and set is done  
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            StepId = Step2,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    //Disable action 
                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step2).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var disable = false;
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == NotifyNotAssigned[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {

                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {
                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                                ReservationLogId = NotifyNotAssigned[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.NoAssignNotificationOpen,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {

                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            ReservationLogId = NotifyNotAssigned[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.NoAssignNotificationOpen,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }

                                }
                            }
                        }

                    }
                }



            }


        });

        #endregion

        #region Push notification for no action taken - Open
        public async Task SendNoActionTakenNotificationsOpen() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step3 = (int)OpenStepEnum.BranchAgentAssignment;
            var Step4 = (int)OpenStepEnum.NoFormsumbittedNotification;
            var Step5 = (int)OpenStepEnum.AgentFormSumbitted;
            // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3
                                                                                                            && r.IsDone == true
                                                                                                            && r.DiffDateAssignSubmit >= 24
                                                                                                            )
                                                                                                           // .GroupBy(r => r.ReservationId)
                                                                                                           // .Select(r => r.First()) 
                                                                                                           .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step4 &&
                                                                                                           r.IsDone == true &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();



                var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step5 &&
                                                                                                           r.IsDone == false &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                          .Select(r => r.ReservationNo)
                                                                                                          .Distinct()
                                                                                                           .ToList();

                var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                       NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step4);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {
                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;

                            if (JobTitles?.Count > 0)
                            {

                                for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                {
                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    if (falg == 0)
                                    {
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            StepId = Step4,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    //Disable action 
                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step4).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var disable = false;
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == FormNotSubmitted[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {

                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {
                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = FormNotSubmitted[i].ReservationNo,
                                                ReservationLogId = FormNotSubmitted[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.NoFormSubmitNotificationOpen,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {
                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            ReservationLogId = FormNotSubmitted[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.NoFormSubmitNotificationOpen,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }

                                }
                            }
                        }

                    }

                }


            }


        });


        #endregion



        #region Push notification for not assigned - Open_Confirmed 

        public async Task SendNotAssignNotificationsOpenConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step1 = (int)OpenStepEnum.ContactManagmentNotification;
            var Step2 = (int)OpenStepEnum.ContactManagmentNoAssignmentNotification;
            var Step3 = (int)OpenStepEnum.ContactManagmentAssignment;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step1 &&
                                                                                                        r.IsDone == true &&
                                                                                                        r.DiffDateNotifyAssign >= 24
                                                                                                        ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {

                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;
                            if (JobTitles?.Count > 0)
                            {
                                for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                                {
                                    if (falg == 0)
                                    {

                                        //set step 2 is done
                                        //create new stepLog 2 and set is done  
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            StepId = Step2,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    //Disable action 
                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step2).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var disable = false;
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == NotifyNotAssigned[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {

                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {
                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                                ReservationLogId = NotifyNotAssigned[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.NoAssignNotificationOpenConfirmed,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {
                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            ReservationLogId = NotifyNotAssigned[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.NoAssignNotificationOpenConfirmed,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                }
                            }
                        }

                    }
                }



            }


        });

        #endregion

        #region Push notification for no action taken - Open_Confirmed
        public async Task SendNoActionTakenNotificationsOpenConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step3 = (int)OpenStepEnum.ContactManagmentAssignment;
            var Step4 = (int)OpenStepEnum.ContactManagmentNoFormsumbittedNotification;
            var Step5 = (int)OpenStepEnum.ContactManagmentFormSumbitted;
            // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3
                                                                                                            && r.IsDone == true
                                                                                                            && r.DiffDateAssignSubmit >= 24
                                                                                                            )
                                                                                                           // .GroupBy(r => r.ReservationId)
                                                                                                           // .Select(r => r.First()) 
                                                                                                           .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step4 &&
                                                                                                           r.IsDone == true &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();



                var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step5 &&
                                                                                                           r.IsDone == false &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                          .Select(r => r.ReservationNo)
                                                                                                          .Distinct()
                                                                                                           .ToList();

                var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                       NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                            u.ReservationStatusId == ReservationStatusId &&
                                                                                                            u.ActionStep == Step4);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {


                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;

                            if (JobTitles?.Count > 0)
                            {

                                for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                {
                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    if (falg == 0)
                                    {
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            StepId = Step4,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }


                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step4).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var disable = false;
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == FormNotSubmitted[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {

                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {


                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = FormNotSubmitted[i].ReservationNo,
                                                ReservationLogId = FormNotSubmitted[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.NoFormSubmitNotificationOpenConfirmed,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {

                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            ReservationLogId = FormNotSubmitted[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.NoFormSubmitNotificationOpenConfirmed,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                }
                            }
                        }

                    }

                }


            }


        });


        #endregion


        //Send automated confirmation email to customer within 48 hours before pick up date

        #region Send automated confirmation email to customer within 48 hours before pick up date
        public async Task SendEmailToCustomerOpenConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step = (int)OpenStepEnum.BranchAgentAssignmentConfirmed;
            var Step2 = (int)OpenStepEnum.SendAutomatedEmailToCustomer;
            // if (Send Email)Step is not done && diff hour between pickup date and current date >= 0 
            var AssignmentSteps = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                   r.ReservationStatus == ReservationStatusId &&
                                                                                                   r.StepId == Step &&
                                                                                                   r.IsDone == false &&
                                                                                                   r.DiffCurrPickUpDate > -24
                                                                                                   ).Distinct()
                                                                                                    .ToList();
            var NotifiedNumbers = AssignmentSteps?.Select(r => r.ReservationNo).ToList();
            if (NotifiedNumbers?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step2 &&
                                                                                                           r.IsDone == true &&
                                                                                                           NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();

                var FormNotSubmitted = AssignmentSteps.Where(r =>
                                                                     !NotificationNotSentNumbers.Contains(r.ReservationNo)
                                                                     ).ToList();

                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {

                    var ToCustomers = new Customer();

                    //get all Customers to this reservations
                    for (int i = 0; i < FormNotSubmitted.Count; i++)
                    {
                        var item = FormNotSubmitted[i];

                        var Reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == item.ReservationNo);
                        if (Reservation != null)
                        {
                            ToCustomers = unitOfWork.CustomerBL.FindOne(c => c.Id == Reservation.CustomerId);
                        }

                        var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step2).ToList();
                        if (actionsetting?.Count > 0)
                        {

                            var disable = false;
                            var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == FormNotSubmitted[i].ReservationNo);
                            for (int k = 0; k < actionsetting.Count; k++)
                            {

                                if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                {
                                    disable = true;
                                }
                            }

                            if (disable == false)
                            {



                                var newLog = new ReservationStepsLog
                                {
                                    ReservationNo = ToCustomers.ReservationNo,
                                    StepId = Step2,
                                    CreationDate = DateTime.Now,
                                    IsDone = true,
                                    ReservationStatus = ReservationStatusId,
                                };
                                unitOfWork.ReservationStepLogBL.Add(newLog);

                                // send Email To customer
                                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templete", "Email.html");
                                string Template = System.IO.File.ReadAllText(file);
                                if (Template != null)
                                {
                                    var reservation = unitOfWork.ReservationBL.GetReservationDetails(r => r.ReservationNum == ToCustomers.ReservationNo);
                                    var EmailText = unitOfWork.EmailSettingBL.FindOne(e => e.ReseravationStatus == reservation.ReservationStatusId)?.EmailText;

                                    Template = Template.Replace("#RESERVATIONNUM#", reservation.ReservationNum.ToString())
                                      .Replace("#DriverName#", reservation.Customer.Name)
                                      .Replace("#EmailText#", EmailText)
                                      .Replace("#VehicleACRISS#", reservation.VehicleAcriss)
                                      .Replace("#PickUpDate#", reservation.PickUpDate.ToString())
                                      .Replace("#PickUpHour#", reservation.PickUpHour.ToString())
                                      .Replace("#DropOffBranchName#", reservation.DropOffBranch?.Name.ToString() ?? "")
                                      .Replace("#DropOffDate#", reservation.DropOffDate.ToString())
                                      .Replace("#DropOffHour#", reservation.DropOffHour.ToString())
                                      .Replace("#BranchEmail#", reservation.DropOffBranch?.Email ?? "")
                                      .Replace("#Date#", DateTime.Now.ToString());


                                    var Email = new EmailHelper(Configuration);
                                    var sent = Email.SendEmail(ToCustomers.Email, Template, true);

                                }





                                if (unitOfWork.Complete() > 0)
                                {
                                    // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                }


                            }
                        }

                        else
                        {

                            var newLog = new ReservationStepsLog
                            {
                                ReservationNo = ToCustomers.ReservationNo,
                                StepId = Step2,
                                CreationDate = DateTime.Now,
                                IsDone = true,
                                ReservationStatus = ReservationStatusId,
                            };
                            unitOfWork.ReservationStepLogBL.Add(newLog);


                            // send Email To customer
                            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templete", "Email.html");
                            string Template = System.IO.File.ReadAllText(file);
                            if (Template != null)
                            {
                                var reservation = unitOfWork.ReservationBL.GetReservationDetails(r => r.ReservationNum == ToCustomers.ReservationNo);
                                var EmailText = unitOfWork.EmailSettingBL.FindOne(e => e.ReseravationStatus == reservation.ReservationStatusId)?.EmailText;

                                Template = Template.Replace("#RESERVATIONNUM#", reservation.ReservationNum.ToString())
                                  .Replace("#DriverName#", reservation.Customer.Name)
                                  .Replace("#EmailText#", EmailText)
                                  .Replace("#VehicleACRISS#", reservation.VehicleAcriss)
                                  .Replace("#PickUpDate#", reservation.PickUpDate.ToString())
                                  .Replace("#PickUpHour#", reservation.PickUpHour.ToString())
                                  .Replace("#DropOffBranchName#", reservation.DropOffBranch?.Name.ToString() ?? "")
                                  .Replace("#DropOffDate#", reservation.DropOffDate.ToString())
                                  .Replace("#DropOffHour#", reservation.DropOffHour.ToString())
                                  .Replace("#BranchEmail#", reservation.DropOffBranch?.Email ?? "")
                                  .Replace("#Date#", DateTime.Now.ToString());


                                var Email = new EmailHelper(Configuration);
                                var sent = Email.SendEmail(ToCustomers.Email, Template, true);

                            }

                            if (unitOfWork.Complete() > 0)
                            {
                                // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                            }


                        }

                    }
                }
            }

        });


        #endregion


        //Notification to branch manager within 24h 

        #region Notification to branch manager within 24h 
        public async Task SendNotificationToBranchMangerConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step = (int)OpenStepEnum.BranchAgentAssignmentConfirmed;
            var Step2 = (int)OpenStepEnum.BranchAgentNotification;

            // if (Send Notification To Branch Agent)Step is not done && diff hour between pickup date and current date >= 24 

            var AssignmentSteps = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                       r.ReservationStatus == ReservationStatusId &&
                                                                                                       r.StepId == Step &&
                                                                                                       r.IsDone == false &&
                                                                                                       r.DiffCurrPickUpDate >= 24
                                                                                                       ).Distinct()
                                                                                                        .ToList();
            var NotifiedNumbers = AssignmentSteps?.Select(r => r.ReservationNo).ToList();
            if (NotifiedNumbers?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step2 &&
                                                                                                           r.IsDone == true &&
                                                                                                           NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();

                var FormNotSubmitted = AssignmentSteps.Where(r =>
                                                                     !NotificationNotSentNumbers.Contains(r.ReservationNo)
                                                                     ).ToList();

                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {

                    // new ReservationStepsLog has been created on initilaize steper 
                    for (int i = 0; i < FormNotSubmitted.Count(); i++)
                    {

                        var newLog = new ReservationStepsLog
                        {
                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                            StepId = Step2,
                            CreationDate = DateTime.Now,
                            IsDone = true,
                            ReservationStatus = ReservationStatusId,
                        };
                        unitOfWork.ReservationStepLogBL.Add(newLog);

                        //send notification to notification user's in setting


                        var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                                u.ReservationStatusId == ReservationStatusId &&
                                                                                                                u.ActionStep == Step2);
                        var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                        var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                        if (JobTitles != null)
                        {
                            //disable action


                            var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step2).ToList();
                            if (actionsetting?.Count > 0)
                            {
                                var disable = false;
                                var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == FormNotSubmitted[i].ReservationNo);
                                for (int k = 0; k < actionsetting.Count; k++)
                                {

                                    if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                    {
                                        disable = true;
                                    }
                                }

                                if (disable == false)
                                {


                                    var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                                    // new ReservationStepsLog has been created on initilaize steper 
                                    for (int j = 0; j < ToUsers.Count(); j++)
                                    {

                                        if (JobTitles?.Count > 0)
                                        {

                                            //set step 2 is done
                                            //create new stepLog 2 and set is done  
                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " need assignment",    // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = FormNotSubmitted[i].ReservationNo,
                                                ReservationLogId = FormNotSubmitted[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.FinalNeedAssignmentNotificationOpen,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                    }
                                }

                                if (unitOfWork.Complete() > 0)
                                {
                                    // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                }



                            }
                            else
                            {
                                var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);
                                // new ReservationStepsLog has been created on initilaize steper 
                                for (int j = 0; j < ToUsers.Count(); j++)
                                {

                                    if (JobTitles?.Count > 0)
                                    {

                                        //set step 2 is done
                                        //create new stepLog 2 and set is done  
                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " need assignment",    // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            ReservationLogId = FormNotSubmitted[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.FinalNeedAssignmentNotificationOpen,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

        });


        #endregion


        #region Push notification for not assigned - To Branch Management open-Confirmed

        public async Task SendNotAssignNotificationsToBranchMangementOpenConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step1 = (int)OpenStepEnum.BranchAgentNotification;
            var Step2 = (int)OpenStepEnum.BranchAgentNoAssignmentNotification;
            var Step3 = (int)OpenStepEnum.BranchAgentAssignmentConfirmed;
            // if step1 (Notification) is done and step2 (not assigned) notdone  and step3 (assigned) not done and diff date between notification and notifed >=24
            var NotifiedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                    r.ReservationStatus == ReservationStatusId &&
                                                                                                    r.StepId == Step1 &&
                                                                                                    r.IsDone == true &&
                                                                                                    r.DiffDateNotifyAssign >= 24
                                                                                                    ).ToList();/*GroupBy(r => r.ReservationId).Select(r=>r.First()).ToList();*/
            var NotifiedNumbers = NotifiedReservations?.Select(r => r.ReservationNo).ToList();
            if (NotifiedReservations?.Count > 0)
            {
                var NoAssignNotificationNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step2 &&
                                                                                                            r.IsDone == true &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo).Distinct().ToList();
                var NotAssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                                                            r.StepId == Step3 &&
                                                                                                            r.IsDone == false &&
                                                                                                            NotifiedNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct().ToList();

                var NotifyNotAssigned = NotifiedReservations.Where(r =>
                                                                        NotAssignedReservations.Contains(r.ReservationNo) &&
                                                                        !NoAssignNotificationNumbers.Contains(r.ReservationNo)).ToList();

                NotifyNotAssigned = NotifyNotAssigned.DistinctBy(x => x.ReservationNo).ToList();

                if (NotifyNotAssigned?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                        u.ReservationStatusId == ReservationStatusId &&
                                                                                                        u.ActionStep == Step2);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles?.Count() > 0)
                    {
                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);




                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;
                            if (JobTitles?.Count > 0)
                            {
                                for (int i = 0; i < NotifyNotAssigned.Count(); i++)
                                {
                                    if (falg == 0)
                                    {

                                        //set step 2 is done
                                        //create new stepLog 2 and set is done  
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            StepId = Step2,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    //disable action
                                    var disable = false;
                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step2).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == NotifyNotAssigned[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {


                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {
                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                                ReservationLogId = NotifyNotAssigned[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.FinalNoAssignNotificationOpen,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {
                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + NotifyNotAssigned[i].ReservationNo + " notified but no assignment taken",   // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = NotifyNotAssigned[i].ReservationNo,
                                            ReservationLogId = NotifyNotAssigned[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + NotifyNotAssigned[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.FinalNoAssignNotificationOpen,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);

                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }


        });

        #endregion

        #region Push notification for no action taken - To Branch Management Open_Confirmed
        public async Task SendNoActionTakenNotificationsToBranchMangementOpenConfirmed() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step3 = (int)OpenStepEnum.BranchAgentAssignmentConfirmed;
            var Step4 = (int)OpenStepEnum.BranchAgentNoFormsumbittedNotification;
            var Step5 = (int)OpenStepEnum.BranchAgentFormSumbitted;
            // if step3 (Assigned) is done and step2 (not FormSubmitted) notdone  and step3 (FormSubmitted) not done and diff date between notification and notifed >=24
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                                                        r.StepId == Step3
                                                                                                        && r.IsDone == true
                                                                                                        && r.DiffDateAssignSubmit >= 24
                                                                                                        )
                                                                                                       // .GroupBy(r => r.ReservationId)
                                                                                                       // .Select(r => r.First()) 
                                                                                                       .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step4 &&
                                                                                                           r.IsDone == true &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();



                var NoFormSubmittedNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step5 &&
                                                                                                           r.IsDone == false &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                          .Select(r => r.ReservationNo)
                                                                                                          .Distinct()
                                                                                                           .ToList();

                var FormNotSubmitted = AssignedReservations.Where(r =>
                                                                       NoFormSubmittedNumbers.Contains(r.ReservationNo) &&
                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {
                    //send notification to notification user's in setting
                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                        u.ReservationStatusId == ReservationStatusId &&
                                                                                                        u.ActionStep == Step4);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null && JobTitles.Count() > 0)
                    {
                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;

                            if (JobTitles?.Count > 0)
                            {

                                for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                {
                                    //set step 2 is done
                                    //create new stepLog 2 and set is done  
                                    if (falg == 0)
                                    {
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            StepId = Step4,
                                            CreationDate = DateTime.Now,
                                            IsDone = true,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);
                                    }
                                    //disable action
                                    var disable = false;
                                    var actionsetting = unitOfWork.ActionSettingBL.Find(a => a.ReservationStatusId == ReservationStatusId && a.ActionStepId == Step4).ToList();
                                    if (actionsetting?.Count > 0)
                                    {
                                        var reservation1 = unitOfWork.ReservationBL.FindOne(a => a.ReservationNum == FormNotSubmitted[i].ReservationNo);
                                        for (int k = 0; k < actionsetting.Count; k++)
                                        {

                                            if (ReservationStatusId == actionsetting[k].ReservationStatusId && actionsetting[k].BranchId == reservation1.PickUpBranchId && actionsetting[k].RateSegmentCategoryId == reservation1.RateSegmentCategory && actionsetting[k].WeekDayId == GetWeekDay(reservation1.PickUpweekDay) && actionsetting[k].IsEnable == false)
                                            {
                                                disable = true;
                                            }
                                        }

                                        if (disable == false)
                                        {

                                            var notify = new Notification
                                            {
                                                ToUser = ToUsers[j].Id,
                                                IsSeen = false,
                                                NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                                Status = false,
                                                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                                ReservationNo = FormNotSubmitted[i].ReservationNo,
                                                ReservationLogId = FormNotSubmitted[i].LogId,
                                                CreateDate = DateTime.Now,
                                                UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                                NotificationType = (int)NotificationType.Open,
                                                GroupId = (int)NotificationGroupType.FinalNoFormSubmitNotificationOpen,
                                                IsDeleted = false,
                                            };
                                            notifications.Add(notify);
                                            //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                            unitOfWork.NotificationBL.Add(notify);
                                        }
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }
                                    }
                                    else
                                    {
                                        var notify = new Notification
                                        {
                                            ToUser = ToUsers[j].Id,
                                            IsSeen = false,
                                            NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " assigned but no action taken",    // need to be modified  later 
                                            Status = false,
                                            ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            ReservationLogId = FormNotSubmitted[i].LogId,
                                            CreateDate = DateTime.Now,
                                            UrlNotification = "/Reservation/OpenSteps/" + FormNotSubmitted[i].ReservationNo,
                                            NotificationType = (int)NotificationType.Open,
                                            GroupId = (int)NotificationGroupType.FinalNoFormSubmitNotificationOpen,
                                            IsDeleted = false,
                                        };
                                        notifications.Add(notify);
                                        //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                        unitOfWork.NotificationBL.Add(notify);
                                        if (unitOfWork.Complete() > 0)
                                        {
                                            // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                        }

                                    }
                                }
                            }

                        }

                    }

                }


            }


        });


        #endregion


        //Notification to branch manager within 24h 

        #region Notification to branch manager when pickup date passed 
        public async Task SendNotificationToBranchMangerConfirmedWhenPickUpDate() => await Task.Run(() =>
        {
            var notifications = new List<Notification>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var Step = (int)OpenStepEnum.ChangeReservationStatus;

            // Get all reservation open and pick up date is passed 24h
            var AssignedReservations = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                      r.ReservationStatus == ReservationStatusId
                                                                                                      && r.DiffCurrPickUpDate <= -24
                                                                                                      )
                                                                                                     .ToList();
            var AssignemntDoneNumbers = AssignedReservations.Select(r => r.ReservationNo).Distinct().ToList();
            if (AssignedReservations?.Count() > 0)
            {
                var NotificationNotSentNumbers = unitOfWork.VReservationLogBL.GetAllNotAssignedReservations(r =>
                                                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                                                           r.StepId == Step &&
                                                                                                           AssignemntDoneNumbers.Contains(r.ReservationNo))
                                                                                                            .Select(r => r.ReservationNo)
                                                                                                            .Distinct()
                                                                                                            .ToList();





                var FormNotSubmitted = AssignedReservations.Where(r =>

                                                                       !NotificationNotSentNumbers.Contains(r.ReservationNo)).ToList();


                FormNotSubmitted = FormNotSubmitted.DistinctBy(x => x.ReservationNo).ToList();

                if (FormNotSubmitted?.Count > 0)
                {



                    var notificationSetting = unitOfWork.NotificationSettingBL.GetNotificationWithDetails(u =>
                                                                                                        u.ReservationStatusId == ReservationStatusId &&
                                                                                                        u.ActionStep == Step && u.IsDisabled == false);
                    var LnkJobTitles = notificationSetting?.LnkNotificationJobTitle.ToList();
                    var JobTitles = LnkJobTitles?.Select(s => s.JobTitleId).ToList();
                    if (JobTitles != null)
                    {
                        var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true);

                        // new ReservationStepsLog has been created on initilaize steper 
                        for (int j = 0; j < ToUsers.Count(); j++)
                        {
                            var falg = j;

                            if (JobTitles?.Count > 0)
                            {

                                for (int i = 0; i < FormNotSubmitted.Count(); i++)
                                {

                                    if (falg == 0)
                                    {
                                        var newLog = new ReservationStepsLog
                                        {
                                            ReservationNo = FormNotSubmitted[i].ReservationNo,
                                            StepId = Step,
                                            CreationDate = DateTime.Now,
                                            IsDone = false,
                                            ReservationStatus = ReservationStatusId,
                                        };
                                        unitOfWork.ReservationStepLogBL.Add(newLog);

                                        //Modify Last step notification Is deleted
                                        var LastNotifications = unitOfWork.NotificationBL.Find(n => n.ReservationNo == FormNotSubmitted[i].ReservationNo)
                                                                                         .ToList();
                                        for (int k = 0; k < LastNotifications.Count(); k++)
                                        {
                                            var item = LastNotifications[k];
                                            item.IsDeleted = true;
                                            unitOfWork.NotificationBL.Update(item);
                                        }
                                    };

                                    var notify = new Notification
                                    {
                                        ToUser = ToUsers[j].Id,
                                        IsSeen = false,
                                        NotificationText = "Reservation " + FormNotSubmitted[i].ReservationNo + " need to change status",    // need to be modified  later 
                                        Status = false,
                                        ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                                        ReservationNo = FormNotSubmitted[i].ReservationNo,
                                        ReservationLogId = FormNotSubmitted[i].LogId,
                                        CreateDate = DateTime.Now,
                                        UrlNotification = "/Reservation/ChangeStatus/" + FormNotSubmitted[i].ReservationNo,
                                        NotificationType = (int)NotificationType.Open,
                                        GroupId = (int)NotificationGroupType.NeedToChangeReservationStatus,
                                        IsDeleted = false,
                                    };
                                    notifications.Add(notify);
                                    //   unitOfWork.NotificationBL.SaveNewNotificationToDB(notify);
                                    unitOfWork.NotificationBL.Add(notify);

                                    if (unitOfWork.Complete() > 0)
                                    {
                                        // _ = notificationHub.SendNotification(ToUsers[j].Id, "Check Notification");

                                    }

                                }

                            }
                        }
                    }

                }
            }

        });


        #endregion



        #region  push all notiication withc was unsent 
        public async void PushUnSentNotifications() => await Task.Run(() =>
             {
                 //  var NotAssignedReservations = unitOfWork.VReservationLogBL.GetReservationWithDetails(u => u.UploadStatus == (int)UploadStatusEnum.Uploaded);


             });


        #endregion

        protected int GetWeekDay(string WeekDay)
        {
            switch (WeekDay.ToLower())
            {
                case "sun": return 1;
                case "mon": return 2;
                case "tue": return 3;
                case "wed": return 4;
                case "thu": return 5;
                case "fri": return 6;
                case "sat": return 7;

                default: return -1;
            }


        }

    }
}
