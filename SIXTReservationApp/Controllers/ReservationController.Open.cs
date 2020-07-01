using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        #region Open Reservation

        #region Search And List
        [PermissionNotRequired]
        public ViewResult OpenIndex()
        {

            return View();
        }

        public PartialViewResult _OpenList(ReservationSC reservationSC, int? page = 1, int? pageSize = 10)
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
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            if (reservationSC.ActionStepId == (int)OpenStepEnum.AgentFormSumbitted || reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentFormSumbitted || reservationSC.ActionStepId == (int)OpenStepEnum.BranchAgentFormSumbitted) // Assign To Me
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            try
            {
                var result = unitOfWork.VReservationListBL.GetAllReservationsOpen(reservationSC, page, pageSize);

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

        [PermissionNotRequired]
        private List<VReservationListModel> SearchOpen(ReservationSC reservationSC)
        {
            if (reservationSC.ActionStepId == (int)OpenStepEnum.AgentFormSumbitted || reservationSC.ActionStepId == (int)OpenStepEnum.ContactManagmentFormSumbitted) // Assign To Me
            {
                reservationSC.AssignedToId = LoggedUserId;
            }
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var model = unitOfWork.VReservationListBL.GetReservationsOpen(reservationSC);


            // var model = unitOfWork.ReservationBL.GetReservationWithDetails(reservationSC).Select(r => new ReservationVM(r)).ToArray();
            return model;

        }
        #endregion

        #region Reservation Details
        public ViewResult OpenDetails(int id)
        {
            ReservationSC reservationSC = new ReservationSC()
            {
                ReservationStatusId = (int)SixtCancellationStatusEnum.Open,
                ReservationId = id,
            };

            var model = unitOfWork.VReservationListBL.GetReservationsOpen(reservationSC).Select(r => new VReservationListVM(r)).LastOrDefault();

            return View(model);
        }
        #endregion

        #region Reservation Steps Page
        #region Initialize step page and get current step

        public ViewResult OpenSteps(string id)
        {
            ViewBag.ReservationId = id;
            ViewBag.ActiveBeforeSteps = 1;//show last steps
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var StepId = (int)OpenStepEnum.BranchManagmentNotification;
            //check if this step is done or not by reservationId
            var ReservationId = long.Parse(id);
            var firstStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                               r.ReservationStatus == ReservationStatusId &&
                                                                               r.StepId == StepId &&
                                                                               r.IsDone == true
                                                                               ).FirstOrDefault();
            if (firstStep == null)
            {

                //For Open - Decliend from excel sheet
                ViewBag.CurrentStep = 0;
                var secondStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                              r.ReservationStatus == ReservationStatusId &&
                                                                              r.StepId == (int)OpenStepEnum.AgentFormSumbitted
                                                                              ).FirstOrDefault();
                if (secondStep != null)
                {
                    ViewBag.CurrentStep = 2;
                    ViewBag.ActiveBeforeSteps = 0;//don't show last steps
                }
                //For Open - Confirmed from excel sheet

                var ThirdStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.ContactManagmentNotification
                                                                            );
                if (ThirdStep?.IsDone == true)
                {
                    ViewBag.CurrentStep = 3;
                    ViewBag.ActiveBeforeSteps = 0; //don't show last steps


                    var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.IsDone == false
                                                                            )?.StepId;

                    if (lastStepNotDone == (int)OpenStepEnum.ContactManagmentAssignment)
                    {
                        ViewBag.CurrentStep = 3;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.ContactManagmentFormSumbitted)
                    {
                        ViewBag.CurrentStep = 4;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.FillCustomerForm)
                    {
                        ViewBag.CurrentStep = 5;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.BranchAgentAssignmentConfirmed)
                    {
                        ViewBag.CurrentStep = 7;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.BranchAgentFormSumbitted)
                    {
                        ViewBag.CurrentStep = 8;
                    }

                    else if (lastStepNotDone == null)
                    {
                        var FourStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                                r.ReservationStatus == ReservationStatusId &&
                                                                                r.StepId == (int)OpenStepEnum.ContactManagmentAssignment
                                                                                );
                        if (FourStep?.IsDone == true)
                        {
                            ViewBag.ActiveBeforeSteps = 0; //don't show last steps
                            ViewBag.CurrentStep = 4;
                        }
                        var FiveStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                              r.ReservationStatus == ReservationStatusId &&
                                                                              r.StepId == (int)OpenStepEnum.ContactManagmentFormSumbitted
                                                                              );

                        if (FiveStep?.IsDone == false)
                        {
                            ViewBag.ActiveBeforeSteps = 0; //don't show last steps
                            ViewBag.CurrentStep = 4;
                        }
                        var SixStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                          r.ReservationStatus == ReservationStatusId &&
                                                                          r.StepId == (int)OpenStepEnum.FillCustomerForm
                                                                          );
                        if (SixStep == null)
                        {
                            ViewBag.CurrentStep = 4;
                        }
                        else if (SixStep != null)
                        {
                            ViewBag.CurrentStep = 5;
                        }
                        else if (SixStep?.IsDone == true)
                        {
                            ViewBag.CurrentStep = 6;
                        }

                        var SevenStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                 r.ReservationStatus == ReservationStatusId &&
                                                                 r.StepId == (int)OpenStepEnum.BranchAgentAssignmentConfirmed
                                                                 );
                        if (SevenStep?.IsDone == false)
                        {
                            ViewBag.CurrentStep = 7;

                        }
                        else if (SevenStep?.IsDone == true)
                        {
                            ViewBag.CurrentStep = 8;

                        }
                        var EightStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                        r.StepId == (int)OpenStepEnum.BranchAgentFormSumbitted
                                                                        );
                        if (EightStep != null)
                        {
                            ViewBag.CurrentStep = 8;
                        }


                    }
                }


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
                    var Notifiedusers = unitOfWork.NotificationBL.GetNotifiedUser(ToUserNotification, ReservationId, StepId).Select(n => new UserNotifications(n)).ToList();

                    //check last step is Done 
                    var lastStepNotDone = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                             r.ReservationStatus == ReservationStatusId &&
                                                                             r.IsDone == false
                                                                             )?.StepId;

                    if (lastStepNotDone == (int)OpenStepEnum.BranchAgentAssignment)
                    {
                        ViewBag.CurrentStep = 1;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.AgentFormSumbitted)
                    {
                        ViewBag.CurrentStep = 2;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.ContactManagmentAssignment)
                    {
                        ViewBag.CurrentStep = 3;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.ContactManagmentFormSumbitted)
                    {
                        ViewBag.CurrentStep = 4;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.FillCustomerForm)
                    {
                        ViewBag.CurrentStep = 5;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.BranchAgentAssignmentConfirmed)
                    {
                        ViewBag.CurrentStep = 7;
                    }
                    else if (lastStepNotDone == (int)OpenStepEnum.BranchAgentFormSumbitted)
                    {
                        ViewBag.CurrentStep = 8;
                    }
                    else if (lastStepNotDone == null) //for End reservation
                    {

                        var SecondStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                                  r.ReservationStatus == ReservationStatusId &&
                                                                                  r.StepId == (int)OpenStepEnum.BranchAgentAssignment
                                                                                  ).LastOrDefault();

                        if (SecondStep?.IsDone == true)
                        {

                            ViewBag.CurrentStep = 2;
                            var thirdStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                                  r.ReservationStatus == ReservationStatusId &&
                                                                                  r.StepId == (int)OpenStepEnum.AgentFormSumbitted
                                                                                  );
                            var thirdStepformSubmit = unitOfWork.FormSubmittedBL.FindOne(f => f.ReservationLogId == thirdStep.Id);
                            if (thirdStep?.IsDone == true && thirdStepformSubmit.IsOpenConfirm == true)
                            {
                                ViewBag.CurrentStep = 3;

                                var FourStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                             r.ReservationStatus == ReservationStatusId &&
                                                                             r.StepId == (int)OpenStepEnum.ContactManagmentAssignment
                                                                             );
                                if (FourStep?.IsDone == true)
                                {
                                    ViewBag.CurrentStep = 4;
                                }
                                var FiveStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                                      r.ReservationStatus == ReservationStatusId &&
                                                                                      r.StepId == (int)OpenStepEnum.ContactManagmentFormSumbitted
                                                                                      );

                                if (FiveStep?.IsDone == false)
                                {
                                    ViewBag.CurrentStep = 4;
                                }
                                var SixStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                                  r.ReservationStatus == ReservationStatusId &&
                                                                                  r.StepId == (int)OpenStepEnum.FillCustomerForm
                                                                                  );
                                if (SixStep != null)
                                {
                                    ViewBag.CurrentStep = 5;
                                }

                                var SevenStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                         r.ReservationStatus == ReservationStatusId &&
                                                                         r.StepId == (int)OpenStepEnum.BranchAgentAssignmentConfirmed
                                                                         );
                                if (SevenStep?.IsDone == false)
                                {
                                    ViewBag.CurrentStep = 7;

                                }
                                else if (SevenStep?.IsDone == true)
                                {
                                    ViewBag.CurrentStep = 8;

                                }
                                var EightStep = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                        r.StepId == (int)OpenStepEnum.BranchAgentFormSumbitted
                                                                        );
                                if(EightStep != null)
                                {
                                    ViewBag.CurrentStep = 8;
                                }


                            }
                        }
                    }


                    else
                    {
                        return View();

                    }
                }
                return View();
            }
        }
        public PartialViewResult _OpenNotify(string id)
        {

            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var StepId = (int)OpenStepEnum.BranchManagmentNotification;
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
        public PartialViewResult _OpenAssignment(string id)
        {
            var emptyAssignment = new List<UserAssignments>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.BranchAgentAssignment
                                                                            ).LastOrDefault();
            if (lastStep?.IsDone == true)
            {
                var AssignUser = unitOfWork.ReservationAssignmentBL.GetUserAssignment(ReservationId).Select(n => new UserAssignments(n)).ToList();
                return PartialView(AssignUser);
            }
            else if (lastStep?.IsDone == false)
            {
                return PartialView(emptyAssignment);
            }
            else
            {
                return null;
            }

        }

        [PermissionNotRequired]
        public JsonResult GetOpenAssignUsers()
        {
            // get users with job title BAgent & BManger
            var JobTitles = unitOfWork.JobTitleBL.Find(j => j.JobTitleId == (int)JobTitleEnum.BranchAgent || j.JobTitleId == (int)JobTitleEnum.BranchManager)
                                               .Select(s => s.JobTitleId).ToList();
            if (JobTitles?.Count > 0)
            {
                var ToUsers = unitOfWork.UserBL.Find(u => JobTitles.Contains(u.JobTitleId.GetValueOrDefault()) && u.IsActive == true)
                                                .Select(u => new { id = u.Id, name = u.FullName }).ToList();
                return Json(ToUsers);
            }
            return Json(null);

        }

        [PermissionNotRequired]
        public JsonResult GetOpenAssignAnyUsers()
        {
            // get users with job title BAgent & BManger
            var JobTitles = unitOfWork.JobTitleBL.Find(j => j.JobTitleId == (int)JobTitleEnum.BranchAgent || j.JobTitleId == (int)JobTitleEnum.BranchManager || j.JobTitleId == (int)JobTitleEnum.ContactCenterAgent || j.JobTitleId == (int)JobTitleEnum.ContactCenterManagement)
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
        public JsonResult OpenAssignAgent_Multiple(AgentAssignmentVM assignObj)
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

                            OpenBranchAssignment builder = (OpenBranchAssignment)step
                                                        .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);

                            if (builder == null)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                            var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                            {
                                FromUser = LoggedUserId,
                                userId = assignObj.AssigneeId
                            });
                            if (PerformAction == false)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Assignment(s) added successfully" });
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

        [HttpPost]
        public JsonResult OpenAssignAnyUser_Multiple(AgentAssignmentVM assignObj)
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

                            var currentStep = unitOfWork.VReservationLogBL.Find(r => r.ReservationNo == reservationNumbers[i] && r.IsDone == false).LastOrDefault();
                            if (currentStep.StepId == 21)
                            {
                                OpenBranchAssignment builder = (OpenBranchAssignment)step
                                                            .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);
                                var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                                {
                                    FromUser = LoggedUserId,
                                    userId = assignObj.AssigneeId
                                });
                                if (PerformAction == false)
                                {
                                    return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                                }
                            }
                            else if (currentStep.StepId == 26)
                            {
                                OpenContactCenterAssignment builder = (OpenContactCenterAssignment)step
                                                            .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);
                                var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                                {
                                    FromUser = LoggedUserId,
                                    userId = assignObj.AssigneeId
                                });

                                if (PerformAction == false)
                                {
                                    return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                                }
                            }
                            else if (currentStep.StepId == 33)
                            {
                                OpenBranchAssignment builder = (OpenBranchAssignment)step
                                                            .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);
                                var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                                {
                                    FromUser = LoggedUserId,
                                    userId = assignObj.AssigneeId
                                });
                                if (PerformAction == false)
                                {
                                    return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                                }
                            }
                            else
                            {

                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });

                            }


                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Assignment(s) added successfully" });
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
        public PartialViewResult _OpenFormSubmit(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.AgentFormSumbitted
                                                                            ).LastOrDefault();
            if (lastStep?.IsDone == true)
            {
                var FormSubmit = unitOfWork.FormSubmittedBL.GetFormSubmitDetails(ReservationId).Select(n => new UserFormSubmit(n)).ToList();
                return PartialView("_OpenFormSubmitDone", FormSubmit);
            }
            else if (lastStep?.IsDone == false)
            {
                return PartialView();
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public JsonResult OpenFormSubmitted_Deny(FormActionVM form)
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
                    form.IsDeny = true;
                    form.CancelBy = (int)SixtCancellationStatusEnum.CancelledBySixt;//Reservation will changed to
                    form.ReasonStatus = null;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
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

        [HttpPost]
        public JsonResult OpenFormSubmitted_Confirm(CAgentOpenNotification form)
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


                    OpenFormAction builder = (OpenFormAction)step
                                                     .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }
                    else
                    {
                        try
                        {

                            var formAction = new FormActionVM
                            {
                                ReasonStatus=null,
                                ReservationId = form.ReservationId,
                                LoggedUser = LoggedUserId,
                                IsOpenConfirm = true,
                                IsDeny = false,
                                IsAnswer = true
                            };
                            var PerformAction = builder.PerformAction(formAction);
                            if (PerformAction == false)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                            if (unitOfWork.Complete() > 0)
                            {

                                OpenContactAgentNotification builder2 = (OpenContactAgentNotification)step
                                                            .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                                builder2.PerformAction(form);
                                if (unitOfWork.Complete() > 0)
                                {
                                    var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                                    _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                                    return Json(new { success = true, Message = "Reservation confirmed successfully" });
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Failed to confirm Reservation" });

                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Failed to confirm Reservation" });

                            }
                        }
                        catch (Exception e)
                        {

                            throw;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }


        public PartialViewResult _OpenCAgentManagementAssignment(string id)
        {
            var emptyAssignment = new List<UserAssignments>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.ContactManagmentAssignment
                                                                            ).LastOrDefault();

            if (lastStep?.IsDone == true)
            {
                var AssignUser = unitOfWork.ReservationAssignmentBL.GetUserAssignment(ReservationId).Select(n => new UserAssignments(n)).ToList();
                return PartialView(AssignUser);
            }
            else if (lastStep?.IsDone == false)
            {
                return PartialView(emptyAssignment);
            }
            else
            {
                return null;
            }
        }

        [PermissionNotRequired]
        public JsonResult GetOpenAssignCAgentUsers()
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
        public JsonResult OpenAssignContactCenter_Multiple(AgentAssignmentVM assignObj)
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

                            OpenContactCenterAssignment builder = (OpenContactCenterAssignment)step
                                                        .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);

                            if (builder == null)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                            var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                            {
                                FromUser = LoggedUserId,
                                userId = assignObj.AssigneeId,
                                
                            });
                            if (PerformAction == false)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Assignment(s) added successfully" });
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

        public PartialViewResult _OpenContactCenterFormSubmit(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.ContactManagmentFormSumbitted
                                                                            ).LastOrDefault();
            if (lastStep?.IsDone == true)
            {
                var FormSubmit = unitOfWork.FormSubmittedBL.GetFormSubmitDetails(ReservationId).Select(n => new UserFormSubmit(n)).ToList();
                return PartialView("_OpenFormSubmitDone", FormSubmit);
            }

            else if (lastStep?.IsDone == false)
            {
                return PartialView();
            }
            else
            {
                return null;
            }


        }


        [HttpPost]
        public JsonResult OpenFormSubmitted_NoAnswer(FormActionVM form)
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
                    //flag to if i want to go to next step or not
                    form.IsDeny = true;
                    form.ReasonStatus = false;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
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

        [HttpPost]
        public JsonResult OpenFormSubmitted_Cancel(FormActionVM form)
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
                    form.IsDeny = true;
                    form.ReasonStatus = true;
                    form.CancelBy = (int)SixtCancellationStatusEnum.CancelledByCustomer;//Reservation will cahnged to

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }
                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
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


        [HttpPost]
        public JsonResult OpenFormSubmitted_ConfirmedConfirm(FormActionVM form)
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
                    form.IsDeny = false;
                    form.IsOpenConfirm = true;
                    form.ReasonStatus = true;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }
                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Reservation confirmed successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to confirm reservation" });

                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }


        public PartialViewResult _OpenFillCustomerForm(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);
            var StepSix = unitOfWork.ReservationStepLogBL.FindOne(r => r.ReservationNo == ReservationId &&
                                                                        r.ReservationStatus == ReservationStatusId &&
                                                                        r.StepId == (int)OpenStepEnum.FillCustomerForm
                                                                        );

            var Reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationId);

            if (Reservation != null)
            {

                var Customer = unitOfWork.CustomerBL.FindOne(c => c.Id == Reservation.CustomerId);
                var CustomerModel = new Models.CByCustomerReservation.CustomerModel()
                {
                    Email = Customer.Email,
                    Phone = Customer.Phone,
                    Comment = Customer.Comment,
                };

                if (StepSix?.IsDone == true)
                {
                    CustomerModel = new Models.CByCustomerReservation.CustomerModel()
                    {
                        Name=Customer.Name,
                        Email = Customer.Email,
                        Phone = Customer.Phone,
                        Comment = Customer.Comment,
                    };

                    return PartialView("_OpenConfirmedSendEmailDone2", CustomerModel);
                }
                else if (StepSix?.IsDone == false)
                {
                    return PartialView("_OpenFillCustomerForm", CustomerModel);
                }
                else
                {
                    return null;
                }
            }

            return PartialView();

        }


        [HttpPost]
        public JsonResult OpenFormSubmitted_CustomerForm(SIXTReservationBL.Models.ViewModels.CustomerModel form)
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

                    OpenFillCustomerFormAction builder = (OpenFillCustomerFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }
                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {

                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to Submit Form" });

                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }


        public PartialViewResult _OpenConfirmedSendEmailDone(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var Reservation = unitOfWork.ReservationBL.FindOne(r => r.ReservationNum == ReservationId);

            if (Reservation != null)
            {

                var Customer = unitOfWork.CustomerBL.FindOne(c => c.Id == Reservation.CustomerId);
                var CustomerModel = new Models.CByCustomerReservation.CustomerModel()
                {
                    Name = Customer.Name,
                    Email = Customer.Email,
                    Phone = Customer.Phone,
                    Comment = Customer.Comment,
                };
                var EmailNotifyStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                           r.ReservationStatus == ReservationStatusId &&
                                                                           r.StepId == (int)OpenStepEnum.SendAutomatedEmailToCustomer
                                                                           ).LastOrDefault();
                if (EmailNotifyStep?.IsDone == true)
                {

                    return PartialView("_OpenConfirmedSendEmailDone", CustomerModel);
                }
                else if (EmailNotifyStep?.IsDone == false)
                {
                    return PartialView("_OpenConfirmedSendEmailDone");
                }
            }


            return PartialView();

        }


        #region Assign Agent step8
        public PartialViewResult _OpenBranchAssignment(string id)
        {
            var emptyAssignment = new List<UserAssignments>();
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.BranchAgentAssignmentConfirmed
                                                                            ).LastOrDefault();
            if (lastStep?.IsDone == true)
            {
                var AssignUser = unitOfWork.ReservationAssignmentBL.GetUserAssignment(ReservationId).Select(n => new UserAssignments(n)).ToList();
                return PartialView(AssignUser);
            }
            else if (lastStep?.IsDone == false)
            {

                return PartialView(emptyAssignment);
            }
            else
            {
                return null;
            }

        }


        [PermissionNotRequired]
        public JsonResult GetOpenAssignBranchUsers()
        {
            // get users with job title BAgent & BManger
            var JobTitles = unitOfWork.JobTitleBL.Find(j => j.JobTitleId == (int)JobTitleEnum.BranchAgent || j.JobTitleId == (int)JobTitleEnum.BranchManager)
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
        public JsonResult OpenAssignBranchAgent_Multiple(AgentAssignmentVM assignObj)
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

                            OpenBranchAssignment builder = (OpenBranchAssignment)step
                                                        .InitiateSteperOpen(reservationNumbers[i], (int)SixtCancellationStatusEnum.Open);

                            if (builder == null)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                            var PerformAction = builder.PerformAction(new AgentAssignmentVM()
                            {
                                FromUser = LoggedUserId,
                                userId = assignObj.AssigneeId,
                                IsFinalAssignment = true
                            });
                            if (PerformAction == false)
                            {
                                return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                            }

                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Assignment(s) added successfully" });
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

        #region Branch Agent form submit
        public PartialViewResult _OpenBranchAgentFormSubmit(string id)
        {
            var ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            var ReservationId = long.Parse(id);

            var lastStep = unitOfWork.ReservationStepLogBL.Find(r => r.ReservationNo == ReservationId &&
                                                                            r.ReservationStatus == ReservationStatusId &&
                                                                            r.StepId == (int)OpenStepEnum.BranchAgentFormSumbitted
                                                                            ).LastOrDefault();
            if (lastStep?.IsDone == true)
            {
                var FormSubmit = unitOfWork.FormSubmittedBL.GetFormSubmitDetails(ReservationId).Select(n => new UserFormSubmit(n)).ToList();
                return PartialView("_OpenFormSubmitDone", FormSubmit);
            }
            else if (lastStep?.IsDone == false)
            {

                return PartialView();
            }
            else
            {
                return null;
            }


        }


        [HttpPost]
        public JsonResult OpenBranchAgentFormSubmitted_NoResponse(FormActionVM form)
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
                    //flag go to next step or not
                    form.IsDeny = true;
                    form.ReasonStatus = false;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
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
        [HttpPost]
        public JsonResult OpenBranchAgentFormSubmitted_Cancelled(FormActionVM form)
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
                    //flag go to next step or not
                    form.IsDeny = true;
                    form.CancelBy = form.CancelBy;
                    form.ReasonStatus = true;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Form submitted successfully" });
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

        [HttpPost]
        public JsonResult OpenBranchAgentFormSubmitted_Confirm(FormActionVM form)
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
                    form.IsDeny = false;
                    form.IsOpenConfirm = true;
                    form.ReasonStatus = true;

                    OpenFormAction builder = (OpenFormAction)step
                                                       .InitiateSteperOpen(reservationId, (int)SixtCancellationStatusEnum.Open);

                    if (builder == null)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    var PerformAction = builder.PerformAction(form);
                    if (PerformAction == false)
                    {
                        return Json(new { success = false, Message = "Failed ,Pickup date for this reservation is Passed more than 24 h" });
                    }

                    if (unitOfWork.Complete() > 0)
                    {
                        var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                        _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                        return Json(new { success = true, Message = "Reservation confirmed successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to confirm reservation" });

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
        public FileResult OpenExportReservation(ReservationSC reservationSC)
        {
            var stream = new MemoryStream();
            try
            {
                // call search function
                var sheet = SearchOpen(reservationSC).Select(r => new ReservationExport(r)).ToArray();

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

        [PermissionNotRequired]
        public JsonResult GetOpenReasonsFormSubmit()
        {
            var result = unitOfWork.ReasonBL.Find(r => r.IsAnswer == false &&
                                                     r.IsActive == true &&
                                                     r.ReservationStatusId == (int)SixtCancellationStatusEnum.Open);
            var reasons = result.Select(r => new { id = r.Id, name = r.ReasonText }).ToArray();
            if (reasons.Length > 0)
            {
                return Json(reasons);
            }
            else
            {
                return Json(null);
            }
        }
        [PermissionNotRequired]
        public JsonResult GetALLReservationStatus()
        {
            var result = unitOfWork.ReservationStatusBL.Find(r => r.Id == 1 || r.Id == 2 || r.Id == 3).ToList();

            var Status = result.Select(r => new { id = r.Id, name = r.Status }).ToArray();
            if (Status.Length > 0)
            {
                return Json(Status);
            }
            else
            {
                return Json(null);
            }
        }

        public ViewResult ChangeStatus()
        {
            return View();
        }
        public PartialViewResult _ChangeStatusList(ReservationSC reservationSC, int? page = 1, int? pageSize = 10)
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
            reservationSC.ReservationStatusId = (int)SixtCancellationStatusEnum.Open;
            try
            {
                var result = unitOfWork.VReservationListBL.GetAllReservationsNeedToAssign(reservationSC, page, pageSize);

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



        [HttpPost]
        public JsonResult ChangeReservationStatus(ChangeStatusVM assignObj)
        {
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
                    var StatusId = assignObj.StatusId;

                    if (reservationNumbers?.Length > 0)
                    {
                        for (int i = 0; i < reservationNumbers.Length; i++)
                        {
                            var item = reservationNumbers[i];
                            OpenChangeStatus obj = new OpenChangeStatus((int)OpenStepEnum.ChangeReservationStatus, item, (int)SixtCancellationStatusEnum.Open, 0, unitOfWork);
                            ChangeStatusVM changestatusobj = new ChangeStatusVM { StatusId = assignObj.StatusId, reservationId = item, LoggedUser = LoggedUserId };
                            var PerformAction = obj.PerformAction(changestatusobj);
                            if (PerformAction == false)
                            {
                                return Json(new { success = false, Message = "Failed to change reservation status" });
                            }

                        }
                        if (unitOfWork.Complete() > 0)
                        {
                            var Count = unitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
                            // var users = unitOfWork.UserBL.Find(u => u.IsActive == true).Select(u => u.Id).ToList();
                            _ = Notify.UpdateUnseenCount(LoggedUserId, Count);
                            return Json(new { success = true, Message = "Reservation(s) status is changed successfully " });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to change reservation status" });

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
    }
}
