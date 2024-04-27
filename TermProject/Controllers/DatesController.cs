using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProjectAPI;

namespace TermProject.Controllers
{

    public class DatesController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {
            // Check if the cookie exists to avoid bypass
            if (!Request.Cookies.ContainsKey("Bypass"))
            {
                // Cookie doesn't exist or has been expired
                return RedirectToAction("Index", "Login");

            }



            string dateUpdated = HttpContext.Session.GetString("DateUpdated");

            // new
            if (dateUpdated == "Yes")
            {
                System.Diagnostics.Debug.WriteLine("date updated adding name");

                ViewBag.dateUpdated = "Your date has been updated!";

                HttpContext.Session.Remove("DateUpdated");

            }






            string username = HttpContext.Session.GetString("Username");

            // retrieve all dates of logged in user
           List<DateInfo> userDates = api.GetUserDates(username);


            //DS to hold user of datws
            List<User> userAccountsList = new List<User>();

            
            foreach (DateInfo date in userDates)
            {
                
                // get correct user name of date from ds
                string dateUserName = "";

                if (username != date.userName1)
                {

                    dateUserName = date.userName1;
                }

                else
                {

                    dateUserName = date.userName2;

                }

                // date requester profile info
                User user = api.GetUserInfo(dateUserName); ;

                // add requester to list
               userAccountsList.Add(user);


            }


            return View(userAccountsList);
        }


        public IActionResult DateDetails(string username)
        {

            // save username before redirecting
            HttpContext.Session.SetString("DateWithUsername", username);

            HttpContext.Session.SetString("DateRequestRedirectedFrom", "Dates");

            return RedirectToAction("Index", "DateRequest");
        }

        public IActionResult ViewProfile(string username)
        {
            // save where view profile is being redirected from
            HttpContext.Session.SetString("ViewProfileRedirectedFrom", "Dates");

            return RedirectToAction("Index", "Profile", new { username = username });
        }




    }
}
