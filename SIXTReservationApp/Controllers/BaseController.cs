using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SIXTReservationApp.Controllers
{
    public class BaseController : Controller
    {
        protected int LoggedUserId
        {
            get
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;
                    return Convert.ToInt32(identity.FindFirst("UserId")?.Value ?? "1");
                }
                catch
                {
                    return 1;
                }
            }
        }
        protected string LoggedUserEmail
        {
            get
            {
                try
                {
                    return ((ClaimsIdentity)User.Identity)?.FindFirst("Email")?.Value;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

    }
}