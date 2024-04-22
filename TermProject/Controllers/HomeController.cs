using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TermProject.Models;

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

    }
}
