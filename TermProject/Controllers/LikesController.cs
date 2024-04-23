using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class LikesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
