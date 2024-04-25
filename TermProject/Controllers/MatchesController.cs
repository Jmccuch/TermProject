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
           

            string userName = HttpContext.Session.GetString("Username");

            List<Match> matches  = new List<Match>();


            //build matches

            int index = 0;

            List<string> username2list = api.GetMatchUsername2();

            foreach (string username in api.GetMatchUsername1()) {

                Match match = new Match();

                match.userName1 = username;

           
                match.userName2 = username2list[index] ;

                index++;

                matches.Add(match);
            
            }


                
            foreach (Match match in matches) {

                if (match.userName1 == userName) {

                    System.Diagnostics.Debug.WriteLine("GET PROFILE :" + match.userName2);

                    User user = api.GetUserInfo(match.userName2);

                    potentialMatches.Add(user);

                }

            }


           

            return View(potentialMatches);
        }



        public IActionResult ViewProfile(string username)
        {
            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom", "Matches");

            string name = HttpContext.Session.GetString("ViewProfileRedirectedFrom");

            return RedirectToAction("Index", "Profile", new { username = username });
        }


        public IActionResult DeleteMatch(string username)
        {

            string loggedInUsername = HttpContext.Session.GetString("Username");


            api.RemoveMatches(loggedInUsername, username);

            return RedirectToAction("Index", "Matches");
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
