using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Data;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class MainController : Controller
    {




        API api = new API();

        //global list of pontential matches
       List<User> potentialMatches = new List<User>();

        // filtered users from global data set (potential maches after filters are applied)
        List<User> filteredUsers = new List<User>();

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

                ViewBag.YouLiked = "You Liked " + name + "!";

                HttpContext.Session.Remove("LikedUser");

            }


            UserSearchFilterModel filterModel = new UserSearchFilterModel();

            string username = HttpContext.Session.GetString("Username");

            potentialMatches = api.GetUserPotentialMatches(username);

            filterModel.users = potentialMatches;

            return View(filterModel);
        }

        // VIEW PROFILE
        public IActionResult ViewProfile(string username)
        {
            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom", "Main");

            return RedirectToAction("Index", "Profile", new { username = username });
        }


        [HttpPost]
        public IActionResult ApplyFilters(UserSearchFilterModel userSearchFilterModel)
        {



            string username = HttpContext.Session.GetString("Username");

            // rest all potential matches intially 
            List<User> profiles = api.GetUserPotentialMatches(username);
            potentialMatches = profiles;



            System.Diagnostics.Debug.WriteLine("Filters:");
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.State);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.CommitmentType);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.LowAge);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.HighAge);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.CatOrDog);

            // go through filters

            StateFilter(userSearchFilterModel);

            RelationshipFilter(userSearchFilterModel);

            AgeRangeFilter(userSearchFilterModel);

            CatDogFilter(userSearchFilterModel);


            /// remove filtered list users from global potential list
            foreach (User user in filteredUsers)
            {
                if (potentialMatches.Contains(user)) { 
                    potentialMatches.Remove(user);
                }

            }

            UserSearchFilterModel filterModel = new UserSearchFilterModel();

            filterModel.users = potentialMatches;


            return View("~/Views/Main/Index.cshtml", filterModel);
        }


        private void StateFilter(UserSearchFilterModel userSearchFilterModel)
        {

            

            // If a state is selected
            if (userSearchFilterModel.State != "No Selection")
            {
                string selection = userSearchFilterModel.State;

                foreach (User user in potentialMatches)
                {


                    if (user.state != selection)
                    {
                        // Check if already in list
                        if (!filteredUsers.Contains(user))
                        {
                            filteredUsers.Add(user);
                        }
                    }
                }
            }
        }



        private void RelationshipFilter(UserSearchFilterModel userSearchFilterModel)
        {
            //Freinds, Marriage, relationship, or not sure
            if (userSearchFilterModel.CommitmentType != "No Selection")
            {

                string selection = userSearchFilterModel.CommitmentType;

                foreach (User user in potentialMatches)
                {

                    if (user.commitmentType != selection)
                    {

                        // check if already in list
                        if (filteredUsers.Contains(user) == false)
                        {
                            filteredUsers.Add(user);
                        }
                    }
                }
            }
        }



        private void AgeRangeFilter(UserSearchFilterModel userSearchFilterModel)
        {
            // Age
            if ((userSearchFilterModel.HighAge != 0 && userSearchFilterModel.LowAge != 0) || (userSearchFilterModel.LowAge == 0 && userSearchFilterModel.HighAge != 0) )
            {
                System.Diagnostics.Debug.WriteLine("not 0s");


                foreach (User user in potentialMatches)
                {
                  
                    if (!((userSearchFilterModel.LowAge <= user.age) && (user.age <= userSearchFilterModel.HighAge)))
                    {
                        System.Diagnostics.Debug.WriteLine("age doesnt match");

                        // check if already in list to remove
                        if (!filteredUsers.Contains(user))
                        {


                            // add
                            filteredUsers.Add(user);
                        }
                    }
                }
            }
        }
        


        private void CatDogFilter(UserSearchFilterModel userSearchFilterModel)
        {
        
            //cat, dog, or neither
            if (userSearchFilterModel.CatOrDog != "No Selection")
            {
                string selection = userSearchFilterModel.CatOrDog;

                foreach (User user in potentialMatches)
                {
                    if (user.catOrDog != selection)
                    {
                     
                        // check if already in list
                        if (!filteredUsers.Contains(user))
                        {
                            filteredUsers.Add(user);
                        }
                    }
                }


            }
        }

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
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



            return RedirectToAction("Index", "Main");


        }




        // after a like check if a new match is made
        private void CheckForMatches(string liker, string likedUser)
        {

            System.Diagnostics.Debug.WriteLine("start check match");


            // Retrieve list containing all likes

            List<Like> allLikes = new List<Like>();

            List<int> likeIDs = api.GetLikeIDs(); ;

            int index = 0;

            foreach (int likedID in likeIDs) { 
            
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
                System.Diagnostics.Debug.WriteLine(like.UserName + " == "  + likedUser + " && " + like.LikedUserName + " == " + liker);

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

            foreach (int id in allLikeIDs) {
                
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

                        System.Diagnostics.Debug.WriteLine(id  + ">" + likeID);

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
