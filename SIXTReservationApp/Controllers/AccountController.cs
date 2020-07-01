using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SIXTReservationApp.Models;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace SIXTReservationApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork UnitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = "/home/index")
        {
            try
            {
                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = "/home/index";
                ViewBag.returnUrl = returnUrl;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var appUser = UnitOfWork.UserBL.Login(model.Email, model.Password);

                if (appUser == null)
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    ViewBag.returnUrl = returnUrl;
                    return View(model);
                }
                else if (appUser.IsSysAdmin != true)
                {
                    if (appUser.IsActive != true)
                    {
                        ModelState.AddModelError("", "Your account is deactivated, please contact your administrator");
                        return View(model);
                    }
                    //else if (appUser.Branch.IsActive != true)
                    //{
                    //    ModelState.AddModelError("", "Your department is deactivated, please contact your administrator");
                    //    return View(model);
                    //}
                }

                var permissions = UnitOfWork.PermissionBL.GetPermissions(appUser.Id);


                await SaveCookies(appUser, permissions, model.RememberMe);

                if (appUser.IsChangedPassword != true)
                {
                    return Redirect($"/UserManagement/ChangePassword?returnUrl={returnUrl}");
                }

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error occured , please try again later");
                return View(model);
            }

        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction(nameof(Login), new { returnUrl });
            }
            return RedirectToAction(nameof(Login));
        }

        private async Task SaveCookies(AppUser user, List<Permission> permissions, bool IsPersistent)
        {
            var roles = UnitOfWork.RoleBL.GetUserRoles(user.Id)
                                         .Select(r => r.Id);
            var rolesIds = string.Join(',', roles);

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Name", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("JobTitle", user.JobTitle?.Name ?? ""),
                    new Claim("IsSysAdmin", user.IsSysAdmin == true ? "true" : "false"),
                    new Claim("Roles", rolesIds),
                    new Claim("IsPersistent", IsPersistent ? "true" : "false"),
                   new Claim("JobTitleId", user.JobTitleId.ToString() ?? ""),
                },
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            if (permissions?.Count > 0)
            {
                 identity.AddClaim(new Claim("PermissionTree", GenerateMenuTree(permissions)));
            }

            var properties = new AuthenticationProperties
            {
                IsPersistent = IsPersistent,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                properties);
        }



        public string GenerateMenuTree(List<Permission> permissions)
        {
            var tree = permissions.Where(p => p.PermissionType == 1).Select(p => new PermissionNodeVM(p));

            var parents = tree.Where(t => t.ParentId == 0).ToList();

            var children = tree.Where(t => t.ParentId != 0).ToList();

            for (var i = 0; i < parents.Count; i++)
            {
                var parent = parents[i];
                AddChildren(ref parent, children);
                parents[i] = parent;
            }

            return JsonConvert.SerializeObject(parents);
        }

        public void AddChildren(ref PermissionNodeVM parent, List<PermissionNodeVM> others)
        {
            if (others?.Count == 0)
            {
                return;
            }
            var parentId = parent.Id;
            var children = others.Where(p => p.ParentId == parentId).ToList();
            var rest = others.Where(p => p.ParentId != parentId).ToList();
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[i];
                parent.Children.Add(child);
                AddChildren(ref child, rest);
            }
        }


        public JsonResult GetNotification()
        {
            var notificationCount = UnitOfWork.NotificationBL.GetCountUnSeenNotification(LoggedUserId);
            return Json(new { success = true, data = notificationCount });
        }
        public JsonResult GetLastNotification()
        {
            //var notification = UnitOfWork.NotificationBL.Find(n => n.ToUser == LoggedUserId)
            //                                            .Take(50)
            //                                             .OrderByDescending(n => n.CreateDate)
            //                                            .Select(n =>
            //                                            new
            //                                            {
            //                                                notification = n.NotificationText,
            //                                                Date = n.CreateDate.Value.ToString(),
            //                                                Url = n.UrlNotification,
            //                                                Unseen = n.IsSeen ?? false,
            //                                                NotificationId = n.Id,
            //                                                NotificationNo = n.ReservationNo.HasValue ? n.ReservationNo.ToString() : string.Empty
            //                                            })
            //                                            .ToArray();

            var notification = UnitOfWork.NotificationBL.GetNotDeletedNotification(LoggedUserId);

            return Json(notification);
        }
        public JsonResult UpdateCountOfUnseenNotification(int id)
        {
            var notification = UnitOfWork.NotificationBL.UpdateCountUnSeenNotification(LoggedUserId, id);
            UnitOfWork.NotificationBL.Update(notification);
            if (UnitOfWork.Complete() > 0)
            {
                return Json(new { success = true, data = notification });
            }
            else
            {
                return Json(new { success = false });

            }
        }


    }
}