using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class LoginController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {

            return View();
        }


        public IActionResult Login()
        {

            string username = "Jmccuch";


            List<User> profiles = api.GetUserPotentialMatches(username);

            return RedirectToAction("Index", "Main");
        }
    }
}
