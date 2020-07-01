using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models.ActionSetting;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class ActionSettingManagementController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public ActionSettingManagementController(IUnitOfWork  _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult ActionIndex()
        {
            return View();
        }

        public PartialViewResult _ActionSettingList(ActionSettingSC search)
        {
            ActionSettingVM[] ActionSetting;
            try
            {
                var Actions = UnitOfWork.ActionSettingBL.SearchActionSetting(search)
                                                      .Select(a => new ActionSettingVM(a)).ToArray();
                ActionSetting = Actions;

            }
            catch (Exception)
            {

                throw;
            }
            return PartialView(ActionSetting);
        }

        public PartialViewResult CreateActionSetting()
        {
            ViewBag.Title = "Create Action Setting";
            return PartialView("_CreateActionSetting", new ActionSettingVM());
        }

        [HttpPost]
        public JsonResult CreateActionSetting(ActionSettingVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {

                    var ActionSettingExist = UnitOfWork.ActionSettingBL.CheckExist(b => b.ReservationStatusId == model.ReservationStatusId && b.ActionStepId == model.ActionStepId && b.BranchId == model.BranchId && b.RateSegmentCategoryId == model.RateSegmentCategoryId && b.WeekDayId == model.WeekDayId);
                    if (ActionSettingExist)
                    {
                        return Json(new { success = false, Message = "Action setting already exists" });
                    }


                    var ActionSetting = new ActionSetting
                    {
                        ReservationStatusId = model.ReservationStatusId,
                        ActionStepId = model.ActionStepId,
                        BranchId=model.BranchId,
                        RateSegmentCategoryId=model.RateSegmentCategoryId,
                        WeekDayId=model.WeekDayId,
                    };
                    if (model.IsEnable == null || model.IsEnable== true)
                    {
                        ActionSetting.IsEnable = true;
                    }
                    else
                    {
                        ActionSetting.IsEnable = false;
                    }

                    UnitOfWork.ActionSettingBL.Add(ActionSetting);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Action setting added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add Action setting" });

                    }
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateActionSetting(int id)
        {
            ViewBag.Title = "Update Action Setting";
            var model = UnitOfWork.ActionSettingBL.GetByID(id);
            return PartialView("_CreateActionSetting", new ActionSettingVM(model));
        }


        [HttpPost]
        public JsonResult UpdateActionSetting(ActionSettingVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {

                    var ActionSetting = UnitOfWork.ActionSettingBL.GetByID(model.Id);
                    if (ActionSetting != null)
                    {
                        ActionSetting.ReservationStatusId = model.ReservationStatusId;
                        ActionSetting.ActionStepId = model.ActionStepId;
                        ActionSetting.BranchId = model.BranchId;
                        ActionSetting.RateSegmentCategoryId = model.RateSegmentCategoryId;
                        ActionSetting.WeekDayId = model.WeekDayId;

                        if (model.IsEnable == null || model.IsEnable == true)
                        {
                            ActionSetting.IsEnable = true;
                        }
                        else
                        {
                            ActionSetting.IsEnable = false;
                        }

                        UnitOfWork.ActionSettingBL.Update(ActionSetting);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Action setting updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update Action setting" });

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
        public JsonResult DeleteActionSetting(int id)
        {
            try
            {
                var ActionSetting = UnitOfWork.ActionSettingBL.GetByID(id);
                if (ActionSetting != null)
                {
                    UnitOfWork.ActionSettingBL.Remove(ActionSetting);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Action setting deleted successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to delete Action setting",
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Action setting not found",
                    });
                }

            }
            catch (Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }


        }

    }
}