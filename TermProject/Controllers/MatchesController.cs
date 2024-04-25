using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class MatchesController : Controller
    {


        API api = new API();

        //global list of pontential matches
        List<User> potentialMatches = new List<User>();

        public IActionResult Index()
        {
            UserAccount account = new UserAccount();

            account.userName = HttpContext.Session.GetString("Username");

            List<Match> matches  = api.GetUserMatches(account);
                
            foreach (Match match in matches) {


                User user = api.GetUserInfo(match.userName2);

                potentialMatches.Add(user);

            }


           

            return View(potentialMatches);
        }

        public IActionResult ViewProfile(string username)
        {
            System.Diagnostics.Debug.WriteLine("main un: " + username);

            return RedirectToAction("Index", "Profile", new { username = username });
        }

        public IActionResult DateRequest(string username)
        {
            System.Diagnostics.Debug.WriteLine("main un: " + username);

            return RedirectToAction("Index", "DateRequest", new { username = username });
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
