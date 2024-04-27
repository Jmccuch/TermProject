using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {

         
                System.Diagnostics.Debug.WriteLine("BYPASS STILL HERE");
                Response.Cookies.Delete("Bypass");


            return View("~/Views/Home/Index.cshtml");
        }
    }
}
