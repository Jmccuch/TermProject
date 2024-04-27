using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class LikesController : Controller
    {

        API api = new API();

        //global list of users who liked logged in user
        List<User> likers = new List<User>();


        public IActionResult Index()
        {
            // Check if the cookie exists to avoid bypass
            if (!Request.Cookies.ContainsKey("Bypass"))
            {
                // Cookie doesn't exist or has been expired
                return RedirectToAction("Index", "Login");

            }

            if (HttpContext.Session.GetString("LikedUser") != null)
            {

                string name = HttpContext.Session.GetString("LikedUser");

                System.Diagnostics.Debug.WriteLine("NAMEEEEEEEE" + name);

                ViewBag.LikedUser = "You liked " + name + "!";

                HttpContext.Session.Remove("LikedUser");

            }



            string username = HttpContext.Session.GetString("Username");

            List<string> likerUsernames = api.GetLikers(username);



            foreach (string likerUserName in likerUsernames) {


                System.Diagnostics.Debug.WriteLine("UN TO GET INFO" + likerUserName);

                likers.Add(api.GetUserInfo(likerUserName));

            }



            return View(likers);
        }

        public IActionResult ViewProfile(string username)
        {
            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom", "Likes");

            string name = HttpContext.Session.GetString("ViewProfileRedirectedFrom");

            System.Diagnostics.Debug.WriteLine("LIKE" + name);

            return RedirectToAction("Index", "Profile", new { username = username });
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }


        public IActionResult RemoveLike(string username)
        {
            string loggedInUsername = HttpContext.Session.GetString("Username");

            api.RemoveLike(loggedInUsername, username);


            return RedirectToAction("Index", "Likes");
        }


        public IActionResult AddLike(string username, string name)
        {


            // set profile being liked
            string likedUser = username;

            // set logged in user
            string liker = HttpContext.Session.GetString("Username");

            // set like ID
            int likeID = GetNextLikeID();

            System.Diagnostics.Debug.WriteLine("api getting id :" + likeID);

            // add like to db 
            api.AddNewLike(likeID, liker, likedUser);


            HttpContext.Session.SetString("LikedUser", name);



            // check if like created a match
            CheckForMatches(liker, likedUser);



            return RedirectToAction("Index", "Likes");


        }




        // after a like check if a new match is made
        private void CheckForMatches(string liker, string likedUser)
        {

            System.Diagnostics.Debug.WriteLine("start check match");


            // Retrieve list containing all likes

            List<Like> allLikes = new List<Like>();

            List<int> likeIDs = api.GetLikeIDs(); ;

            int index = 0;

            foreach (int likedID in likeIDs)
            {

                // build like

                Like like = new Like();

                like.LikeNumber = likedID;



                List<string> likerUsernames = api.GetLikerUsernames();

                like.UserName = likerUsernames[index];



                List<string> likedUsernames = api.GetLikedUsernames();

                like.LikedUserName = likedUsernames[index];


                allLikes.Add(like);

                System.Diagnostics.Debug.WriteLine("ADDING : " + likedID + " " + like.UserName + " " + like.LikedUserName);

                index++;
            }






            System.Diagnostics.Debug.WriteLine("LIKE COUNT: " + allLikes.Count);
            // go through each like an see if liked profile already liked the current liker

            foreach (Like like in allLikes)
            {
                System.Diagnostics.Debug.WriteLine(like.UserName + " == " + likedUser + " && " + like.LikedUserName + " == " + liker);

                // if liked already liked the liker
                if (like.UserName == likedUser && like.LikedUserName == liker)
                {
                    System.Diagnostics.Debug.WriteLine("ADDING NEW MATCH");

                    // add new match

                    AddNewMatch(liker, likedUser);

                    break;


                }



            }



        }

        private void AddNewMatch(string liker, string likedUser)
        {
            // get next match id
            int matchID = GetMatchID();

            System.Diagnostics.Debug.WriteLine("AMATCH ID: " + matchID);

            // add new match
            api.AddNewMatch(matchID, liker, likedUser);


        }

        private int GetMatchID()
        {
            // keep track of MatchID
            int matchID = 0;

            // get next highest match id

            // Retrieve dataset containing all match ids from the ds
            List<Match> matches = api.GetMatches();

            // Check if list contains any entries
            if (matches != null && matches.Count > 0)
            {
                // go through each match in match list
                foreach (Match match in matches)
                {

                    // Compare current id with the existing max id
                    if (match.matchID > matchID)
                    {
                        // Update the max id
                        matchID = match.matchID;
                    }


                }

                // increase the max match ID 
                matchID++;
            }
            else
            {
                //if no matches are found return 1 
                return 1;
            }

            // Return the next match ID
            return matchID;



        }



        private int GetNextLikeID()
        {
            // keep track of likeID
            int likeID = 0;

            // get next highest like id

            List<int> allLikeIDs = api.GetLikeIDs();

            foreach (int id in allLikeIDs)
            {

            }

            // Check if list contains any entries
            if (allLikeIDs != null && allLikeIDs.Count > 0)
            {
                // go through each row in like list
                foreach (int id in allLikeIDs)
                {
                    System.Diagnostics.Debug.WriteLine("LIKE ID: " + id);

                    if (id > likeID)
                    {

                        System.Diagnostics.Debug.WriteLine(id + ">" + likeID);

                        // Update the maximum LikeID
                        likeID = id;
                    }

                }

                // increase the max LikeID 
                likeID++;
            }
            else
            {
                //if no likes are found return 1 
                return 1;
            }

            // Return the next LikeID

            System.Diagnostics.Debug.WriteLine("returning: " + likeID);
            return likeID;
        }




    }
}
