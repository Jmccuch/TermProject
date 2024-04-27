using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class DashboardController : Controller
    {
        API api = new API();

        //global list of pontential matches
        List<User> potentialMatches = new List<User>();
        List<User> potentialMatches2 = new List<User>();


        public IActionResult Index()
        {

            List<int> scores = api.GetAll2048();
            foreach (int score in scores) {

                System.Diagnostics.Debug.WriteLine(score);

            }





            // Check if the cookie exists to avoid bypass
            if (!Request.Cookies.ContainsKey("Bypass"))
            {
                // Cookie doesn't exist or has been expired
                return RedirectToAction("Index", "Login");

            }


            // check if user has new date requests or matches on load

            // new date requests
            if (CheckIfUserHasNewDateRequests() == true)
            {
                ViewBag.DateRequest = "You have a new date request!";

            }


            return View(scores);
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult RedirectMain()
        {
            return View("~/Views/Main/Index.cshtml");
        }



        // check if user has new date requests
        public Boolean CheckIfUserHasNewDateRequests()
        {

            string loggedInUserName = HttpContext.Session.GetString("Username");
            System.Diagnostics.Debug.WriteLine("LOLOLOL " + loggedInUserName);

            // retrieve all  users date reuested accounts
            List<DateRequest> userDateRequestsDS = api.GetDateRequests(loggedInUserName);

            Boolean newDateRequest = false;

            foreach (DateRequest request in userDateRequestsDS)
            {

                if (request.viewed == "No")
                {

                    newDateRequest = true;

                    System.Diagnostics.Debug.WriteLine("TOTOTOT " + loggedInUserName);


                    // update that all of users date requests have viewed
                    api.UpdateUserDateRequestView(loggedInUserName);

                    break;
                }

            }

            return newDateRequest;
        }


        private bool CheckIfUserHasNewMatches()
        {
        


            string loggedInUserName = HttpContext.Session.GetString("Username");


            System.Diagnostics.Debug.WriteLine("FIREEEEE " + loggedInUserName);

            List<Match> matches = new List<Match>();


            //build matches

            int index = 0;

            List<string> username2list = api.GetMatchUsername2();

            foreach (string username in api.GetMatchUsername1())
            {
                System.Diagnostics.Debug.WriteLine("ice " + username);

                Match match = new Match();

                match.userName1 = username;


                match.userName2 = username2list[index];

                index++;

                matches.Add(match);

            }



            foreach (Match match in matches)
            {

                System.Diagnostics.Debug.WriteLine("grass  " + loggedInUserName);

                System.Diagnostics.Debug.WriteLine(match.userName1 + " == " + loggedInUserName);

                if (match.userName1 == loggedInUserName)
                {


                    System.Diagnostics.Debug.WriteLine("water  " + loggedInUserName);

                    User user = api.GetUserInfo(match.userName2);

                    potentialMatches2.Add(user);

                }

            }

            System.Diagnostics.Debug.WriteLine("GGGGGGGGGGGGGGGGGGGy " + matches.Count);


            Boolean newMatches = false;

            // check if any have not been viewed 
            foreach (Match match in matches)
            {
                System.Diagnostics.Debug.WriteLine("GOGOGOGOG " + loggedInUserName);
                System.Diagnostics.Debug.WriteLine("GOGO3434GOGOG " +  match.viewed);

               

                    if (match.viewed == "No")
                {

                    System.Diagnostics.Debug.WriteLine("GOGOGOGOG " + loggedInUserName);
                    newMatches = true;

                    // update that all of users matches have viewed
                    api.UpdateUserMatchesView(loggedInUserName);

                    break;
                }

            }

            return newMatches;




        }





    }
}
