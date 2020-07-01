using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SIXTReservationBL;
using SIXTReservationBL.CoreBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIXTReservationApp.Auth
{

    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

     
            if (SkipAuthorization(filterContext)) return;

            var claimsIdentity = (ClaimsIdentity)filterContext.HttpContext.User.Identity;

            var userIdString = claimsIdentity.FindFirst("UserId")?.Value;
            var userId = Convert.ToInt32(userIdString ?? "0");
            var isSystemAdmin = claimsIdentity.FindFirst("IsSysAdmin");

            if (isSystemAdmin?.Value == "true")
            {
                return;
            }
            else
            {
                string action = filterContext.ActionDescriptor.DisplayName.Split(' ')[0].Split('.').Last();
                string controller = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
                string path = string.Join('-', controller, action).ToLower();

                if (!filterContext.HttpContext.User.Identity.IsAuthenticated || userId == 0)
                {
                    var returnUrl = filterContext.HttpContext.Request.Path;

                    var result = new RedirectResult($"/Account/Logout?returnUrl={returnUrl}");
                    filterContext.Result = result;
                    return;
                }
                if (IsLoginOnly(filterContext))
                {
                    return;
                }
                
                var unitOfWork = (UnitOfWork)filterContext.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

                //if (unitOfWork.UserBL.CheckExist(u => u.Id == userId && u.IsChangedPassword != true))
                //{
                //    var returnUrl = filterContext.HttpContext.Request.Path;
                //    var result = new RedirectResult($"/UserManagement/ChangePassword?returnUrl={returnUrl}");
                //    filterContext.Result = result;
                //    return;
                //}

                if (!unitOfWork.PermissionBL.UserHasPermission(userId, path))
                {
                    var returnType = ((ControllerActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType;
                    IActionResult result;
                    if (returnType == typeof(JsonResult) || returnType == typeof(Task<JsonResult>))
                    {
                        result = new JsonResult(new { Success = false, Ok = false, Message = "You don't have permission" });
                    }
                    else if (returnType == typeof(PartialViewResult) || returnType == typeof(Task<PartialViewResult>))
                    {
                        result = new PartialViewResult
                        {
                            ViewName = "_Unauthorized",
                        };
                        //result = new JsonResult(new { PermissionError = true, Message = "You don't have permission" });
                    }
                    else if (returnType == typeof(FileResult) || returnType == typeof(Task<FileResult>))
                    {
                        result = new EmptyResult();
                    }
                    else
                    {
                        result = new RedirectResult("/Unauthorized");
                    }
                    filterContext.Result = result;
                }
            }

        }

        private static bool SkipAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true)?.Count() > 0;
            }
            return false;
        }

        private static bool IsLoginOnly(AuthorizationFilterContext filterContext)
        {
            if (filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(PermissionNotRequiredAttribute), true)?.Count() > 0;
            }
            return false;
        }
    }

}
