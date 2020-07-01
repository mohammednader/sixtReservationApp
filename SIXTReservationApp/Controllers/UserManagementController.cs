using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Auth;
using SIXTReservationApp.Models.UserManagement;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Helper;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class UserManagementController : BaseController
    {
        private readonly IUnitOfWork UnitOfWork;
        public UserManagementController(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
        }
        [PermissionNotRequired]
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult _UserList(UserSC search)
        {
            var users = UnitOfWork.UserBL.SearchUsers(search);
            var model = users.Select(u =>
            {
                var roles = UnitOfWork.RoleBL.GetUserRoles(u.Id);
                var Branches = UnitOfWork.BranchBL.GetUserBranches(u.Id);
                return new UserVM(u)
                {
                    Branch= string.Join(", ",Branches.Select(r=>r.Name).ToArray()),
                    Roles = string.Join(", ", roles.Select(r => r.Name).ToArray()),
                };
            }).ToList();
            return PartialView(model);
        }
        public ViewResult CreateUser()
        {
            ViewBag.Title = "Create User";
            return View(new AddUserDto());
        }



        [HttpPost]
        public JsonResult CreateUser(AddUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            else
            {
                var user = new AppUser
                {
                    PasswordHash = PEncryption.Encrypt(model.Password),
                    FullName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    JobTitleId = model.JobTitle,
                    IsActive = model.IsActive,
                };

                if (UnitOfWork.UserBL.ValidateUser(user, out string errorMessage))
                {
                    user.LnkUserRole = model.Roles.Where(r => r.HasValue).Select(role => new LnkUserRole
                    {
                        RoleId = role.GetValueOrDefault(),
                    }).ToList();

                    user.LnkUserBranch = model.Branch.Where(r => r.HasValue).Select(Branch => new LnkUserBranch
                    {
                        BranchId = Branch.GetValueOrDefault(),
                    }).ToList();

                    user.CreationDate = DateTime.Now;

                    UnitOfWork.UserBL.Add(user);

                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "User added successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to add user",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = errorMessage,
                    });
                }

            }
        }

        public ViewResult UpdateUser(int id)
        {
            ViewBag.Title = "Update User";
            var model = UnitOfWork.UserBL.GetUserWithDetails(id);
            return View("CreateUser", new AddUserDto(model));
        }

        [HttpPost]
        public JsonResult UpdateUser(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            else
            {

                var user = UnitOfWork.UserBL.GetUserWithDetails(model.Id);

                user.FullName = model.Name;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.JobTitleId = model.JobTitle;
                user.IsActive = model.IsActive;
                if (UnitOfWork.UserBL.ValidateUser(user, out string errorMessage))
                {
                    user.LnkUserRole = model.Roles.Where(r => r.HasValue).Select(r => new LnkUserRole
                    {
                        RoleId = r.Value
                    }).ToHashSet();

                    user.LnkUserBranch = model.Branch.Where(r => r.HasValue).Select(r => new LnkUserBranch
                    {
                        BranchId = r.Value
                    }).ToHashSet();
                    user.LastModificationDate = DateTime.Now;

                    UnitOfWork.UserBL.Update(user);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "User updated successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to update user",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = errorMessage,
                    });
                }
            }
        }

        [HttpPost]
        public JsonResult DeactivateUser(int id)
        {
            try
            {
                var user = UnitOfWork.UserBL.GetByID(id);
                if (user != null)
                {
                    user.IsActive = false;
                    user.LastModificationDate = DateTime.Now;

                    UnitOfWork.UserBL.Update(user);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "User  is deactivated successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to deactivate user",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User not found",
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    Success = false,
                    Message = "Failed to deactivate user",
                });
            }
        }

        [HttpPost]
        public JsonResult ActivateUser(int id)
        {
            try
            {
                var user = UnitOfWork.UserBL.GetByID(id);
                if (user != null)
                {
                    user.IsActive = true;
                    user.LastModificationDate = DateTime.Now;

                    UnitOfWork.UserBL.Update(user);
                    if (UnitOfWork.Complete() > 0)
                    {
                        return Json(new
                        {
                            Success = true,
                            Message = "User is activated successfully",
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false,
                            Message = "Failed to activate user",
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User not found",
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    Success = false,
                    Message = "Failed to activate user",
                });
            }
        }

        [PermissionNotRequired]
        public PartialViewResult ResetPassword(int id)
        {
            ResetPasswordDto model = new ResetPasswordDto { Id = id };
            return PartialView("_ResetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            if (UnitOfWork.UserBL.ResetPassword(model.Id, model.Password, out string errorMessage))
            {
                if (UnitOfWork.Complete() > 0)
                {
                    return Json(new
                    {
                        Ok = true,
                        Message = "Password reset successully",
                    });
                }
                else
                {
                    return Json(new
                    {
                        Ok = false,
                        Message = "Failed to reset password",
                    });

                }
            }
            else
            {
                return Json(new
                {
                    Ok = false,
                    Message = errorMessage,
                });
            }
        }

        [PermissionNotRequired]
        public IActionResult ChangePassword(string returnUrl)
        {
            string email = LoggedUserEmail;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.returnUrl = returnUrl;
            ChangePasswordDto model = new ChangePasswordDto { Email = email };
            return View(model);
        }

        [HttpPost]
        [PermissionNotRequired]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordDto model, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            if (UnitOfWork.UserBL.ChangePassword(model.Email, model.Password, model.NewPassword, out string errorMessage))
            {
                if (UnitOfWork.Complete() > 0)
                {
                    var redirect = returnUrl ?? "/";
                    return Json(new
                    {
                        Ok = true,
                        Message = "Password changed successully",
                        Redirect = redirect,
                    });
                }
                else
                {
                    return Json(new
                    {
                        Ok = false,
                        Message = "Failed to change password",
                    });

                }
            }
            else
            {
                return Json(new
                {
                    Ok = false,
                    Message = errorMessage,
                });
            }

        }



    }
}