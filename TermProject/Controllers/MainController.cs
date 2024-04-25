using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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

            if (HttpContext.Session.GetString("LikedUser") != null)
            {
                System.Diagnostics.Debug.WriteLine("PICKLE");

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


        public IActionResult ViewProfile(string username)
        {
            System.Diagnostics.Debug.WriteLine("main un: " + username);

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
            // CheckForMatches(liker, likedUser);



            return RedirectToAction("Index", "Main");


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
