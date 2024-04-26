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

            string username = HttpContext.Session.GetString("Username");

            // retrieve all dates of logged in user
           List<Date> userDates = api.GetUserDates(username);


            //DS to hold user of datws
            List<User> userAccountsList = new List<User>();

            
            foreach (Date date in userDates)
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
    }
}
