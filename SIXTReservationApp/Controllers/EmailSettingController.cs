using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
   [AppAuthorize]
    public class EmailSettingController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public EmailSettingController(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }
        public IActionResult EmailSettingIndex()
        {
            return View();
        }
        public PartialViewResult _EmailSettingList()
        {
            EmailSettingVM[] EmailSettings = null;
            try
            {
                EmailSettings = UnitOfWork.EmailSettingBL.GetAll().Select(e => new EmailSettingVM(e)).ToArray();
            }
            catch (Exception e)
            {
                EmailSettings = null;
            }

            return PartialView(EmailSettings);
        }

        public PartialViewResult CreateEmailSetting()
        {
            ViewBag.Title = "Create Email Setting";
            return PartialView("_CreateEmailSetting", new EmailSettingVM(null));
        }

        [HttpPost]
        public JsonResult CreateEmailSetting(EmailSettingVM model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var sameEmailSettingExist = UnitOfWork.EmailSettingBL.CheckExist(b => b.ReseravationStatus==model.ReservationStatus);
                    if (sameEmailSettingExist)
                    {
                        return Json(new { success = false, Message = "Email setting already exists" });
                    }
                    var EmailSetting = new EmailSetting
                    {
                        ReseravationStatus = model.ReservationStatus,
                        EmailText=model.EmailText
                    };
                    UnitOfWork.EmailSettingBL.Add(EmailSetting);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Email setting added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add email setting" });

                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateEmailSetting(int id)
        {
            ViewBag.Title = "Update Email Setting";
            var model = UnitOfWork.EmailSettingBL.GetByID(id);
            return PartialView("_CreateEmailSetting", new EmailSettingVM(model));
        }

        [HttpPost]
        public JsonResult UpdateEmailSetting(EmailSettingVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                   

                    var EmailSetting = UnitOfWork.EmailSettingBL.GetByID(model.Id);
                    if (EmailSetting != null)
                    {
                        EmailSetting.ReseravationStatus = model.ReservationStatus;
                        EmailSetting.EmailText  = model.EmailText;
                       

                        UnitOfWork.EmailSettingBL.Update(EmailSetting);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Email Setting updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update email Setting" });

                        }
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Banch not found" });
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