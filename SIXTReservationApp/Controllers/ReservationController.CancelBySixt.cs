using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Hubs;
using SIXTReservationApp.Models;
using SIXTReservationApp.Models.CByCustomerReservation;
using SIXTReservationBL.Hendlers;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Repositories;

namespace SIXTReservationApp.Controllers
{
    public partial class ReservationController : BaseController
    {
        #region Cancel by sixt

        #region Search And List
        [PermissionNotRequired]
        public ViewResult CBySixtIndex()
        {
            return View();
        }

        public PartialViewResult _CancelledBySixtList(ReservationSC reservationSC, int? page = 1, int? pageSize = 10)
        {
            var Model = new PaginationResult<ReservationVM>();
            if (page.GetValueOrDefault() == 0)
            {
                page = 1;
            }
            if (pageSize.GetValueOrDefault() == 0)
            {
                pageSize = 10;
            }
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            if (reservationSC.ActionStepId == 11) //Assign To Me
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            try
            {
                var model = unitOfWork.VReservationListBL.GetReservationsCancelBySixt(reservationSC, page, pageSize);
                Model.Page = model.Page;
                Model.PageSize = model.PageSize;
                Model.Total = model.Total;
                Model.Items = model.Items.Select(r => new ReservationVM(r)).ToList();

                return PartialView(Model);
            }
            catch (Exception e)
            {

                return PartialView(null);
            }

        }

        private List<VReservationListModel> SearchCBySixt(ReservationSC reservationSC)
        {
            if (reservationSC.ActionStepId == 11)
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var model = unitOfWork.VReservationListBL.GetAllReservationsCancelBySixt(reservationSC);

            // var model = unitOfWork.ReservationBL.GetReservationWithDetails(reservationSC).Select(r => new ReservationVM(r)).ToArray();
            return model;

        }
        #endregion

        #region Reservation Details
        public IActionResult CBySixtDetails(int id)
        {
            ReservationSC reservationSC = new ReservationSC()
            {
                ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt,
                ReservationId = id,
            };
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var model = unitOfWork.VReservationListBL.GetAllReservationsCancelBySixt(reservationSC).Select(r => new VReservationListVM(r)).LastOrDefault();

            return View(model);
        }
        #endregion

        #region Reservation Steps Page
        #region Initialize step page and get current step
        [PermissionNotRequired]
        public ViewResult CBySixtSteps(string id)
        {
            ViewBag.ReservationId = id;
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var StepId = (int)CacelledBySixtStepEnum.OperationReservationManagementNotification;
            //check if this step is done or not by reservationId
            var ReservationId = long.Parse(id);
            var firstStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                               r.ReservationStatus == ReservationStatusId &&
                                                                               r.StepId == StepId &&
                                                                               r.IsDone == true
                                                                               ).FirstOrDefault();
            if (firstStep == null)
            {
                ViewBag.CurrentStep = 0;
                return View();
            }
            else
            {
                // get notification for this reservation
                var ToUserNotification = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationId && n.ReservationStatusId == ReservationStatusId)
                                                                  .Select(n => n.ToUser).ToList();

                if (ToUserNotification.Count() > 0)
                {
                    ViewBag.CurrentStep = 1;

                    //check last step is Done 
                    var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                              r.ReservationStatus == ReservationStatusId &&
                                                                              r.StepId == (int)CacelledBySixtStepEnum.OperationReservationManagementAssignment
                                                                              ).LastOrDefault();

                    if (lastStep?.IsDone == true)
                    {
                        ViewBag.CurrentStep = 2;
                    }

                    return View();
                }

                else
                {
                    return View();

                }
            }

        }

        public PartialViewResult _CBySixtNotify(string id)
        {

            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var StepId = (int)CacelledBySixtStepEnum.OperationReservationManagementNotification;
            //check if this step is done or not by reservationId
            var ReservationId = long.Parse(id);

            // get notification for this reservation
            var ToUserNotification = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationId && n.ReservationStatusId == ReservationStatusId)
                                                              .Select(n => n.ToUser).ToList();

            if (ToUserNotification.Count() > 0)
            {
                ViewBag.CurrentStep = 1;
                var Notifiedusers = unitOfWork.NotificationBL.GetNotifiedUser(ToUserNotification, ReservationId, StepId).Select(n => new UserNotifications(n)).ToList();

                if (Notifiedusers != null)
                {
                    return PartialView(Notifiedusers);
                }
                return PartialView();
            }

            else
            {
                return PartialView();

            }
        }

        #endregion

        #region Assign Agent step2
        public PartialViewResult _CBySixtAssignment(string id)
        {
            var emptyAssignment = new List<UserAssignments>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)CacelledBySixtStepEnum.OperationReservationManagementAssignment &&
                                                                            r.IsDone == true
                                                                            ).LastOrDefault();
            if (lastStep != null)
            {
                var AssignUser = unitOfWork.ReservationAssignmentBL.GetUserAssignment(ReservationId).Select(n => new UserAssignments(n)).ToList();
                return PartialView(AssignUser);
            }

            return PartialView(emptyAssignment);
        }

        [PermissionNotRequired]
        public JsonResult GetCBySixtAssignUsers()
        {
            // get users with job title Operation & reservation Management
            var JobTitles = unitOfWork.JobTitleBL.Find(j => j.JobTitleId == (int)JobTitleEnum.OperationsManager || j.JobTitleId == (int)JobTitleEnum.ReservationManager)
                                               .Select(s => s.JobTitleId).ToList();
            if (JobTitles?.Count > 0)
            {
                var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true)
                                                .Select(u => new { id = u.Id, name = u.FullName }).ToList();
                return Json(ToUsers);
            }
            return Json(null);

        }


        [HttpPost]
        public JsonResult CBySixtAssign_Multiple(AgentAssignmentVM assignObj)
        {
            StepHandler step = new StepHandler(unitOfWork);
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {

                    var date = DateTime.Now;
                    var reservationNumbers = assignObj.ReservationNo;
                    if (reservationNumbers?.Length > 0)
                    {
                        for (int i = 0; i < reservationNumbers.Length; i++)
                        {

                            CancelledBySixtAssignment builder = (CancelledBySixtAssignment)step
                                                         .InitiateSteperCBySixt(reservationNumbers[i], (int)SixtCancellationStatusEnum.CancelledBySixt);

                            if (builder == null)
                            {
                                return Json(new { success = false, Message = "Failed , No steps found " });
                            }
                            builder.PerformAction(new AgentAssignmentVM()
                            {
                                FromUser = LoggedUserId,
                                userId = assignObj.AssigneeId
                            });

                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Assignment(s) added successfuly" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to add assignment(s)" });

                        }


                    }
                    else
                    {
                        return Json(new { success = false, Message = "No items selected" });
                    }

                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }
        #endregion

        #region Form Submit Step 3
        public PartialViewResult _CBySixtFormSubmit(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.CancelledBySixt;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)CacelledBySixtStepEnum.OperationReservationManagementFormSumbitted &&
                                                                            r.IsDone == true
                                                                            ).LastOrDefault();
            if (lastStep != null)
            {
                var FormSubmit = unitOfWork.FormSubmittedBL.GetFormSubmitDetails(ReservationId).Select(n => new UserFormSubmit(n)).ToList();
                return PartialView("_CBySixtFormSubmitDone", FormSubmit);
            }

            return PartialView();
        }

        [PermissionNotRequired]
        public JsonResult GetReasonForCBySixt()
        {
            try
            {

                var result = unitOfWork.ReasonBL.Find(r =>
                                                      r.IsActive == true &&
                                                      r.ReservationStatusId == (int)SixtCancellationStatusEnum.CancelledBySixt);
                var reasons = result.Select(r => new { id = r.Id, name = r.ReasonText }).ToArray();
                return Json(reasons);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult CBySixtFormSubmitted(FormActionVM form)
        {

            StepHandler step = new StepHandler(unitOfWork);
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var reservationId = long.Parse(form.ReservationId);
                    form.LoggedUser = LoggedUserId;

                    CancelledBySixtFormAction builder = (CancelledBySixtFormAction)step
                                                       .InitiateSteperCBySixt(reservationId, (int)SixtCancellationStatusEnum.CancelledBySixt);
                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed , No steps found " });
                    }
                    builder.PerformAction(form);


                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfuly" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to submit form" });

                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }
        #endregion
        #endregion
        #region Export Reservation 
        public FileResult CBySixtExportReservation(ReservationSC reservationSC)
        {
            var stream = new MemoryStream();
            try
            {
                // call search function
                var sheet = SearchCBySixt(reservationSC).Select(r => new ReservationExport(r)).ToArray();

                var ReservationDT = Helper.GenerateDataTable(sheet.ToArray());

                using (var wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ReservationDT, "Reservation");

                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reservation.xlsx");

                }
            }
            catch (Exception e)
            {
                return File(stream.ToArray(), e.ToString());

            }

        }
        #endregion

        #endregion

        #region OPen >> CBySixt/ CByCustomer /NoShow
        [PermissionNotRequired]
        public ViewResult ReservationHistorySteps(string id,int status)
        {
            ViewBag.status=status;
            ViewBag.ReservationId = id;
            var StepsDone = unitOfWork.VReservationHistoryBL
                                                            .Find(r => r.ReservationNo == long.Parse(id))
                                                            .Select(r => new ReservationHistoryVM(r)).ToList();
           
            return View(StepsDone);

        }
        #endregion



    }
}
