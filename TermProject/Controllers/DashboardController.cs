using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult RedirectMain()
        {
            return View("~/Views/Main/Index.cshtml");
        }
    }
}
