using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class LikesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
