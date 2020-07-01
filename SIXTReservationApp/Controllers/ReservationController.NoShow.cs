using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models;
using SIXTReservationApp.Models.CByCustomerReservation;
using SIXTReservationBL.Hendlers;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Controllers
{
    public partial class ReservationController : BaseController
    {
        #region No Show

        #region Search And List
        [PermissionNotRequired]
        public ViewResult NoShowIndex()
        {

            return View();
        }

        public PartialViewResult _NoShowList(ReservationSC reservationSC, int? page = 1, int? pageSize = 10)
        {
            var Model = new PaginationResult<ReservationVM>();
            if (page.GetValueOrDefault() == 0)
            {
                page = 1;
            }
            if (pageSize.GetValueOrDefault() == 0)
            {
                pageSize = 20;
            }
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            if (reservationSC.ActionStepId == 17) // Assign To Me
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            try
            {
                var result = unitOfWork.VReservationListBL.GetAllReservationsNoShow(reservationSC, page, pageSize);

                Model.Page = result.Page;
                Model.PageSize = result.PageSize;
                Model.Total = result.Total;
                Model.Items = result.Items.Select(r => new ReservationVM(r)).ToList();


                return PartialView(Model);
            }
            catch (Exception e)
            {

                return PartialView(null);
            }

        }

        private List<VReservationListModel> SearchNoShow(ReservationSC reservationSC)
        {
            if (reservationSC.ActionStepId == 17)
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var model = unitOfWork.VReservationListBL.GetReservationsNoShow(reservationSC);


            // var model = unitOfWork.ReservationBL.GetReservationWithDetails(reservationSC).Select(r => new ReservationVM(r)).ToArray();
            return model;

        }
        #endregion

        #region Reservation Details
        public IActionResult NoShowDetails(int id)
        {
            ReservationSC reservationSC = new ReservationSC()
            {
                ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow,
                ReservationId = id,
            };
          
            var model = unitOfWork.VReservationListBL.GetReservationsNoShow(reservationSC).Select(r => new VReservationListVM(r)).LastOrDefault();

            return View(model);
        }
        #endregion

        #region Reservation Steps Page
        #region Initialize step page and get current step

        public ViewResult NoShowSteps(string id)
        {
            ViewBag.ReservationId = id;
            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var StepId = (int)NoShowStepEnum.ContactCenterManagmentNotification;
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
                    ViewBag.CurrentStep = 1 /*(int)NoShowStepEnum.ContactCenterAgentAssignment*/;
                    var Notifiedusers = unitOfWork.NotificationBL.GetNotifiedUser(ToUserNotification, ReservationId, StepId).Select(n => new UserNotifications(n)).ToList();

                    //check last step is Done 
                    var SecondStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                              r.ReservationStatus == ReservationStatusId &&
                                                                              r.StepId == (int)NoShowStepEnum.ContactCenterAgentAssignment
                                                                              ).LastOrDefault();

                    if (SecondStep?.IsDone == true)
                    {
                        
                        ViewBag.CurrentStep =2/*(int)NoShowStepEnum.NoAssignmentNotification*/;
                    }

                    return View();
                }

                else
                {
                    return View();

                }
            }

        }
        public PartialViewResult _NoShowNotify(string id)
        {

            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var StepId = (int)NoShowStepEnum.ContactCenterManagmentNotification;
            //check if this step is done or not by reservationId
            var ReservationId = long.Parse(id);

            // get notification for this reservation
            var ToUserNotification = unitOfWork.NotificationBL.Find(n => n.ReservationNo == ReservationId && n.ReservationStatusId == ReservationStatusId)
                                                              .Select(n => n.ToUser).ToList();


            var Notifiedusers = unitOfWork.NotificationBL.GetNotifiedUser(ToUserNotification, ReservationId, StepId).Select(n => new UserNotifications(n)).ToList();

            if (Notifiedusers != null)
            {
                return PartialView(Notifiedusers);
            }

            return PartialView();


        }
        #endregion

        #region Assign Agent step2
        public PartialViewResult _NoShowAssignment(string id)
        {
            var emptyAssignment = new List<UserAssignments>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)NoShowStepEnum.ContactCenterAgentAssignment &&
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
        public JsonResult GetNoShowAssignUsers()
        {
            // get users with job title CAgent & CManger
            var JobTitles = unitOfWork.JobTitleBL.Find(j => j.JobTitleId == (int)JobTitleEnum.ContactCenterAgent || j.JobTitleId == (int)JobTitleEnum.ContactCenterManagement)
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
        public JsonResult NoShowAssignAgent_Multiple(AgentAssignmentVM assignObj)
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

                            NoShowAgentAssignment builder = (NoShowAgentAssignment)step
                                                        .InitiateSteperNoShow(reservationNumbers[i], (int)SixtCancellationStatusEnum.NoShow);

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
        public PartialViewResult _NoShowFormSubmit(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.NoShow;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)NoShowStepEnum.AgentFormSumbitted &&
                                                                            r.IsDone == true
                                                                            ).LastOrDefault();
            if (lastStep != null)
            {
                var FormSubmit = unitOfWork.FormSubmittedBL.GetFormSubmitDetails(ReservationId).Select(n => new UserFormSubmit(n)).ToList();
                return PartialView("_CByCustomerFormSubmitDone", FormSubmit);
            }

            return PartialView();
        }

        [PermissionNotRequired]
        public JsonResult GetNoShowFormSubmit(bool isAnswer)
        {
            if (isAnswer != null)
            {
                var result = unitOfWork.ReasonBL.Find(r => r.IsAnswer == isAnswer &&
                                                      r.IsActive == true &&
                                                      r.ReservationStatusId == (int)SixtCancellationStatusEnum.NoShow);
                var reasons = result.Select(r => new { id = r.Id, name = r.ReasonText }).ToArray();
                return Json(reasons);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult NoShowFormSubmitted(FormActionVM form)
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

                    NoShowFormAction builder = (NoShowFormAction)step
                                                       .InitiateSteperNoShow(reservationId, (int)SixtCancellationStatusEnum.NoShow);

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
        public FileResult NoShowExportReservation(ReservationSC reservationSC)
        {
            var stream = new MemoryStream();
            try
            {
                // call search function
                var sheet = SearchNoShow(reservationSC).Select(r => new ReservationExport(r)).ToArray();

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
    }
}
