using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class LikesController : Controller
    {
        string username = "Jmccuch";

        API api = new API();

        //global list of pontential matches
        List<User> potentialMatches = new List<User>();

      

        public IActionResult Index()
        {
            potentialMatches = api.GetUserPotentialMatches(username);

            return View(potentialMatches);
        }

        public IActionResult ViewProfile(string username)
        {
            System.Diagnostics.Debug.WriteLine("main un: " + username);

            return RedirectToAction("Index", "Profile", new { username = username });
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
