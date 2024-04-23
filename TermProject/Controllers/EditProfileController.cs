using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class EditProfileController : Controller
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
