using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIXTReservationApp.Auth
{
    public class LoginRequired : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var claimsIdentity = (ClaimsIdentity)filterContext.HttpContext.User.Identity;

            var userIdString = claimsIdentity.FindFirst("UserId")?.Value;
            var userId = Convert.ToInt32(userIdString ?? "0");


            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || userId == 0)
            {
                var returnUrl = filterContext.HttpContext.Request.Path;

                var result = new RedirectResult($"/Account/Logout?returnUrl={returnUrl}");
                filterContext.Result = result;
                return;
            }

        }
    }
}
