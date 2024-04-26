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
