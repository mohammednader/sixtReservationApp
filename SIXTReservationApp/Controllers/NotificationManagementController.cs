using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models.Notification;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class NotificationManagementController : BaseController
    {
        private readonly IUnitOfWork UnitOfWork;
        public NotificationManagementController(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult NotificationIndex()
        {
            return View();
        }

        public PartialViewResult _NotificationList(NotificationSC search)
        {
            NotificationVM[] Notification = null;
            try
            {
                var notification = UnitOfWork.NotificationSettingBL.SearchNotification(search);
                Notification = notification.Select(n =>
               {
                   var jobTitle = UnitOfWork.NotificationSettingBL.GetNotificationJobTitle(n.Id);
                   return new NotificationVM(n)
                   {
                       JobTitleText = string.Join(", ", jobTitle.Select(r => r.Name).ToArray()),
                   };
               }).ToArray();
            }
            catch (Exception e)
            {
                Notification = null;
            }

            return PartialView(Notification);

        }

        public PartialViewResult CreateNotification()
        {
            ViewBag.Title = "Create Notification";
            return PartialView("_CreateNotification", new NotificationVM());
        }

        [HttpPost]
        public JsonResult CreateNotification(NotificationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {

                    var NotificationExist = UnitOfWork.NotificationSettingBL.CheckExist(b => b.ReservationStatusId == model.ReservationStatusId && b.ActionStep == model.ActionStep);
                    if (NotificationExist)
                    {
                        return Json(new { success = false, Message = "Notification setting already exists" });
                    }


                    var Notification = new NotificationSetting
                    {
                        ReservationStatusId = model.ReservationStatusId,
                        ActionStep = model.ActionStep,

                    };
                    if (model.IsDisable == null)
                    {
                        Notification.IsDisabled = true;
                    }
                    else
                    {
                        Notification.IsDisabled = model.IsDisable;
                    }


                    Notification.LnkNotificationJobTitle = model.JobTitleId.Where(r => r.HasValue).Select(r => new LnkNotificationJobTitle
                    {
                        JobTitleId = r.GetValueOrDefault()
                    }).ToList();

                    UnitOfWork.NotificationSettingBL.Add(Notification);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Notification setting added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add notification setting" });

                    }
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateNotification(int id)
        {
            ViewBag.Title = "Update Notification";
            var model = UnitOfWork.NotificationSettingBL.GetNotificationWithDetails(f => f.Id == id);
            return PartialView("_CreateNotification", new NotificationVM(model));
        }


        [HttpPost]
        public JsonResult UpdateNotification(NotificationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    //var NotificationExist = UnitOfWork.NotificationSettingBL.CheckExist(b => b.ReservationStatusId == model.ReservationStatusId && b.ActionStep == model.ActionStep&&b. );
                    //if (NotificationExist)
                    //{
                    //    return Json(new { success = false, Message = "Notification setting already exists" });
                    //}
                    var Notification = UnitOfWork.NotificationSettingBL.GetNotificationWithDetails(f => f.Id == model.Id);
                    if (Notification != null)
                    {
                        Notification.ReservationStatusId = model.ReservationStatusId;
                        Notification.ActionStep = model.ActionStep;
                        if (model.IsDisable == null)
                        {
                            Notification.IsDisabled = true;
                        }
                        else
                        {
                            Notification.IsDisabled = model.IsDisable;
                        }
                        Notification.LnkNotificationJobTitle = model.JobTitleId.Where(r => r.HasValue).Select(r => new LnkNotificationJobTitle
                        {
                            JobTitleId = r.Value
                        }).ToHashSet();

                        UnitOfWork.NotificationSettingBL.Update(Notification);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Notification setting updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update Notification setting" });

                        }
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Notification setting not found" });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }

        [HttpPost]
        public JsonResult DeleteNotification(int id)
        {
            try
            {
                var notification = UnitOfWork.NotificationSettingBL.GetNotificationWithDetails(f => f.Id == id);
                if (notification != null)
                {
                    List<int> title = notification.LnkNotificationJobTitle.Select(t => t.JobTitleId).ToList();
                    UnitOfWork.NotificationSettingBL.DetachNotificationJobTitle(notification.Id, title);
                    UnitOfWork.NotificationSettingBL.Remove(notification);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Notification setting deleted successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to delete notification setting",
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Notification not found",
                    });
                }

            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }


        }

        public IActionResult NotificationList()
        {
            return View();
        }
        [PermissionNotRequired]
        public PartialViewResult AllNotification(NotificationDateRangeSC reservation)
        {
            reservation.ToUser = LoggedUserId;
            var notifications = UnitOfWork.NotificationBL.GetAllNotification(reservation).Select(n => new NotificationDto(n)).OrderByDescending(c => c.Date).ToList();
            return PartialView(notifications);
        }


    }
}