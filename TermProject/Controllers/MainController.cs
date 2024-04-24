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

        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
