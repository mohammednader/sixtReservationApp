using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIXTReservationApp.Auth;
using SIXTReservationApp.ViewModels;

namespace SIXTReservationApp.Controllers
{
   
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [LoginRequired]
        public IActionResult Index()
        {
            return View();

        }

        [Route("Unauthorized")]
        [AllowAnonymous]
        public IActionResult AppUnauthorized()
        {
            return View("Unauthorized");
        }
        public IActionResult steps()
        {
            return View();

        }
        public IActionResult branches()
        {
            return View();
        }
       
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
