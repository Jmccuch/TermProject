using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class MainController : Controller
    {
        string username = "Jmccuch";

        API api = new API();

        //global list of pontential matches
       List<User> potentialMatches = new List<User>();

        // filtered users from global data set (potential maches after filters are applied)
        List<User> filteredUsers = new List<User>();

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


        [HttpPost]
        public IActionResult ApplyFilters(UserSearchFilterModel userSearchFilterModel)
        {

            // rest all potential matches intially 
            List<User> profiles = api.GetUserPotentialMatches(username);
            potentialMatches = profiles;



            System.Diagnostics.Debug.WriteLine("Filters:");
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.State);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.CommitmentType);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.Occupation);
            System.Diagnostics.Debug.WriteLine(userSearchFilterModel.CatOrDog);

            // go through filters

            StateFilter(userSearchFilterModel);

            RelationshipFilter(userSearchFilterModel);

            SameOccupationFilter(userSearchFilterModel);

            CatDogFilter(userSearchFilterModel);


            /// remove filtered list users from global potential list
            foreach (User user in filteredUsers)
            {
                if (potentialMatches.Contains(user)) { 
                    potentialMatches.Remove(user);
                
                }

            }
          
            return View("~/Views/Main/Index.cshtml", potentialMatches);
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



        private void SameOccupationFilter(UserSearchFilterModel userSearchFilterModel)
        {
            // same occupation is checked
            if (userSearchFilterModel.Occupation == true)
            {

                // retrieve account info
                


                // first (and only row)

                // set occupation
                string occupation = "Super Hero";


                // remove rows with differnt occupations
                foreach (User user in potentialMatches)
                {
                    System.Diagnostics.Debug.WriteLine("if" + user.occupation + "!=" + occupation);
                    if (!user.occupation.Equals(occupation))
                    {
                        // check if already in list
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
            System.Diagnostics.Debug.WriteLine("enter");
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


    }
}
