using Microsoft.AspNetCore.Mvc;
using System.Data;
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

            if (HttpContext.Session.GetString("DateRequestedUser") != null)
            {

                string name = HttpContext.Session.GetString("DateRequestedUser");

                ViewBag.DateRequestedUser = "You sent a date request to " + name + "!";

                HttpContext.Session.Remove("DateRequestedUser");

            }



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


                    User user = api.GetUserInfo(match.userName2);

                    potentialMatches.Add(user);

                }

            }



            System.Diagnostics.Debug.WriteLine("MATCHY COUNT " + potentialMatches.Count);


            return View(potentialMatches);
        }



        public IActionResult ViewProfile(string username)
        {


            System.Diagnostics.Debug.WriteLine("MC");

            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom","Matches");

            return RedirectToAction("Index", "Profile", new { username = username });
        }


        public IActionResult DeleteMatch(string username)
        {

            string loggedInUsername = HttpContext.Session.GetString("Username");


            api.RemoveMatches(loggedInUsername, username);

            return RedirectToAction("Index", "Matches");
        }



        public IActionResult DateRequest(string username, string name)
        {

            /*

            // set profile being sent Date request
            string requestee = username;

            string loggedInUsername = HttpContext.Session.GetString("Username");


            // get next avaible request Id
            int requestID = GetRequestID();

            api.AddNewDateRequest(requestID, loggedInUsername, requestee);

            HttpContext.Session.SetString("DateRequestedUser", name);


            return RedirectToAction("Index", "Matches");
            */



            // save info
            HttpContext.Session.SetString("DRusername", username);
            HttpContext.Session.SetString("DRname", name);


            // save where dr form is being redirected from
            HttpContext.Session.SetString("DateRequestRedirectedFrom", "Matches");

            return RedirectToAction("Index", "DateRequest");

        }


        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }



    }
}
