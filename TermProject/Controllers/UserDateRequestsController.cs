using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class UserDateRequestsController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {
           

            string dateUpdated = HttpContext.Session.GetString("DateUpdated");

            // new
            if (dateUpdated == "Yes")
            {
         

                ViewBag.dateUpdated = "Your date has been updated!";

                HttpContext.Session.Remove("DateUpdated");

            }


            

            // new
            if (HttpContext.Session.GetString("RequestAccepted")!= null)
            {
                ViewBag.requestAccepted = HttpContext.Session.GetString("RequestAccepted");

                HttpContext.Session.Remove("RequestAccepted");

            }






            string username = HttpContext.Session.GetString("Username");



            // retrieve all dates of logged in user
            List<DateRequest> dateRequests = api.GetDateRequests(username);


            //DS to hold user of datws
            List<User> userAccountsList = new List<User>();


            foreach (DateRequest dateRequest in dateRequests)
            {



                // date requester profile info
                User user = api.GetUserInfo(dateRequest.sender); ;

                // add requester to list
                userAccountsList.Add(user);


            }


            return View(userAccountsList);
        }


        public IActionResult AcceptDate(string username, string name)
        {

            int dateID = GetNextDateID();


            string accepterUserName = HttpContext.Session.GetString("Username");


            string requesteeUserName = username;


            // add
            api.AddNewDate(dateID, accepterUserName, requesteeUserName);



            // remove date request
             api.RemoveDateRequest(accepterUserName, requesteeUserName);


            HttpContext.Session.SetString("RequestAccepted", "You Accepted " + name + "'s Date Request!");


            return RedirectToAction("Index");

        }



        public IActionResult RemoveDate(string username, string name)
        {


            string accepterUserName = HttpContext.Session.GetString("Username");


            string requesteeUserName = username;

            // remove date request
            api.RemoveDateRequest(accepterUserName, requesteeUserName);

            return RedirectToAction("Index");

        }

            private int GetNextDateID()
        {
            // keep track of date ID
            int dateID = 0;

            // get next highest date id

            List<DateInfo> allDateIDs = api.GetDates();

            // Check if ds contains any rows
            if (allDateIDs != null && allDateIDs.Count > 0)
            {
                // go through each row
                foreach (DateInfo date in allDateIDs)
                {
                  
                        // Compare current date id with the existing max date id
                        if (date.dateID > dateID)
                        {
                            // Update the maximum dateid
                            dateID = date.dateID;
                        }
                    
                }

                // increase the max date id 
                dateID++;
            }
            else
            {
                //if no dates are found return 1 
                return 1;
            }

            // Return the next date id
            return dateID;
        }


        public IActionResult ViewProfile(string username)
        {
            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom", "DateRequest");

            string name = HttpContext.Session.GetString("ViewProfileRedirectedFrom");


            return RedirectToAction("Index", "Profile", new { username = username });
        }



    }
}
