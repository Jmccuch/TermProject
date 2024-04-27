using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

      

        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if the cookie exists to avoid bypass
            if (!Request.Cookies.ContainsKey("Bypass"))
            {
                // Cookie doesn't exist or has been expired
                return RedirectToAction("Index", "Login");

            }

            {
                System.Diagnostics.Debug.WriteLine("BYPASS STILL HERE");
                Response.Cookies.Delete("Bypass");
            }

            System.Diagnostics.Debug.WriteLine("home index");
            return View();
        }

        public IActionResult RedirectLogin()
        {
            return RedirectToAction("Index", "Login");
        }


        public IActionResult RedirectSignUp()
        {
            return View("~/Views/SignUp/Index.cshtml");
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }





    }
}
