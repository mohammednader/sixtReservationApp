using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Models.Reason;
using SIXTReservationApp.ViewModels;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Models.Domain;

namespace SIXTReservationApp.Controllers
{
    public class ReasonManagementController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public ReasonManagementController(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }
        public IActionResult ReasonIndex()
        {
            return View();
        }

        public PartialViewResult _ReasonList(ReasonSC search)
        {
            ReasonVM[] Reasons = null;
            try
            {
                Reasons = UnitOfWork.ReasonBL.SearchReason(search).Select(b => new ReasonVM(b)).ToArray();
            }
            catch (Exception e)
            {
                Reasons = null;
            }

            return PartialView(Reasons);

        }

        public PartialViewResult CreateReason()
        {
            ViewBag.Title = "Create Reason";
            return PartialView("_CreateReason",new ReasonVM());
        }

        [HttpPost]
        public JsonResult CreateReason(ReasonVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var ReasonExist = UnitOfWork.ReasonBL.CheckExist(b => b.ReasonText.ToLower() == model.Reason.ToLower() && b.ReservationStatusId == model.ReservationStatusId);
                    if (ReasonExist)
                    {
                        return Json(new { success = false, Message = "Reason with same name already exists" });
                    }
                    var Reason = new Reason
                    {
                        ReasonText = model.Reason,
                        ReservationStatusId = model.ReservationStatusId,
                        IsAnswer = model.Status??null,
                        IsActive = true,
                    };
                    UnitOfWork.ReasonBL.Add(Reason);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Reason added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add reason" });

                    }
                }
            }
            catch(Exception)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateReason(int id)
        {
            ViewBag.Title = "Update Reason";
            var model = UnitOfWork.ReasonBL.GetByID(id);
            return PartialView("_CreateReason", new ReasonVM(model));
        }


        [HttpPost]
        public JsonResult UpdateReason(ReasonVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var ReasonExist = UnitOfWork.ReasonBL.CheckExist(b => b.ReasonText.ToLower() == model.Reason.ToLower() && b.ReservationStatusId == model.ReservationStatusId);
                    if (ReasonExist)
                    {
                        return Json(new { success = false, Message = "Reason with same name already exists" });
                    }
                    var Reason = UnitOfWork.ReasonBL.GetByID(model.Id);
                    if (Reason != null)
                    {
                        Reason.ReasonText = model.Reason;
                        Reason.ReservationStatusId = model.ReservationStatusId;
                        Reason.IsAnswer = model.Status;

                        UnitOfWork.ReasonBL.Update(Reason);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Reason updated successfuly" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update reason" });

                        }
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Reason not found" });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }

        [HttpPut]
        public JsonResult DeactivateReason(int id)
        {
            try
            {
                var reason = UnitOfWork.ReasonBL.GetByID(id);
                if (reason != null)
                {
                    reason.IsActive = false;

                    UnitOfWork.ReasonBL.Update(reason);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Reason deactivate successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to deactivate reason",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "reason not found",
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        [HttpPut]
        public JsonResult ActivateReason(int id)
        {
            try
            {
                var reason = UnitOfWork.ReasonBL.GetByID(id);
                if (reason != null)
                {
                    reason.IsActive = true;
                    UnitOfWork.ReasonBL.Update(reason);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Reason activate successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to activate reason",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "reason not found",
                    });
                }
            }

            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }

        [HttpPost]
        public JsonResult DeleteReason(int id) 
        {
            try
            {
                var reason = UnitOfWork.ReasonBL.GetByID(id);
                if(reason != null)
                {
                    UnitOfWork.ReasonBL.Remove(reason);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Reason deleted successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to delete reason",
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "reason not found",
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