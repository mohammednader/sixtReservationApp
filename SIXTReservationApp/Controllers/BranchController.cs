using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIXTReservationApp.Auth;
using SIXTReservationApp.ViewModels;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class BranchController : Controller
    {
        private readonly IUnitOfWork UnitOfWork;
        public BranchController(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult BranchIndex()
        {
            return View();
        }

        public PartialViewResult _BranchList(BranchSC search)
        {
            BranchVM[] branches = null;
            try
            {
                branches = UnitOfWork.BranchBL.SearchBranch(search).Select(b => new BranchVM(b)).ToArray();
            }
            catch (Exception e)
            {
                branches = null;
            }

            return PartialView(branches);
        }

        public PartialViewResult CreateBranch()
        {
            ViewBag.Title = "Create Branch";
            return PartialView("_CreateBranch", new BranchVM());
        }

        [HttpPost]
        public JsonResult CreateBranch(BranchVM model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    var sameNameExist = UnitOfWork.BranchBL.CheckExist(b => b.Name.ToLower() == model.Name.ToLower());
                    var sameCodeExist = UnitOfWork.BranchBL.CheckExist(b => b.Code.ToLower() == model.Code.ToLower());
                    if (sameNameExist)
                    {
                        return Json(new { success = false, Message = "Branch with same name already exists" });
                    }
                    if (sameCodeExist)
                    {
                        return Json(new { success = false, Message = "Branch with same code already exists" });
                    }

                    var branch = new Branch
                    {
                        Name = model.Name,
                        Code = model.Code,
                        Email = model.Email,
                        IsActive = true
                    };
                    UnitOfWork.BranchBL.Add(branch);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new { success = true, Message = "Branch added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Failed to add branch" });

                    }
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        public PartialViewResult UpdateBranch(int id)
        {
            ViewBag.Title = "Update Branch";
            var model = UnitOfWork.BranchBL.GetByID(id);
            return PartialView("_CreateBranch", new BranchVM(model));
        }

        [HttpPost]
        public JsonResult UpdateBranch(BranchVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(null);
                }
                else
                {
                    //var sameNameExist = UnitOfWork.BranchBL.CheckExist(b => b.Name.ToLower() == model.Name.ToLower() && b.Id != model.Id);
                    //var sameCodeExist = UnitOfWork.BranchBL.CheckExist(b => b.Code.ToLower() == model.Code.ToLower() && b.Id != model.Id);
                    //if (sameNameExist)
                    //{
                    //    return Json(new { success = false, Message = "Branch with same name already exists" });
                    //}
                    //if (sameCodeExist)
                    //{
                    //    return Json(new { success = false, Message = "Branch with same code already exists" });
                    //}

                    var Branch = UnitOfWork.BranchBL.GetByID(model.Id);
                    if (Branch != null)
                    {
                        Branch.Name = model.Name;
                        Branch.Code = model.Code;
                        Branch.Email = model.Email;

                        UnitOfWork.BranchBL.Update(Branch);
                        if (UnitOfWork.Complete() > 0)
                        {
                            return Json(new { success = true, Message = "Branch updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Failed to update branch" });

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

        [HttpPost]
        public JsonResult DeactivateBranch(int id)
        {
            try
            {
                var branch = UnitOfWork.BranchBL.GetByID(id);
                if (branch != null)
                {
                    branch.IsActive = false;

                    UnitOfWork.BranchBL.Update(branch);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Branch is deactivated successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to deactivate Branch",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Branch not found",
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }

        }

        [HttpPost]
        public JsonResult ActivateBranch(int id)
        {
            try
            {
                var branch = UnitOfWork.BranchBL.GetByID(id);
                if (branch != null)
                {
                    branch.IsActive = true;
                    UnitOfWork.BranchBL.Update(branch);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "Branch is activated successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to activate Branch",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Branch not found",
                    });
                }
            }

            catch (Exception e)
            {
                return Json(new { success = false, Message = "An error occured , please try again later" });
            }
        }


    }
}