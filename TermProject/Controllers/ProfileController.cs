using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class ProfileController : Controller
    {

        API api = new API();

        public IActionResult Index(string username)
        {
            // came from Dates
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "Dates")
            {
                ViewBag.ShowSensitiveInfo = true;

            }



            System.Diagnostics.Debug.WriteLine("CLICKED ON PROFILE: " + username);

            User user = api.GetUserInfo(username);

            System.Diagnostics.Debug.WriteLine("PICTURE URL " + user.picture1);

            return View(user);
        }
        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }


        public IActionResult Back()
        {
            string name = HttpContext.Session.GetString("ViewProfileRedirectedFrom");

            // came from main
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "Main") {
                return RedirectToAction("Index","Main");
            }


            // came from likes
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "Likes")
            {
                return RedirectToAction("Index", "Likes");
            }


            // came from matches
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "Matches")
            {

                return RedirectToAction("Index", "Matches");
            }

            // came from Dates
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "Dates")
            {


                return RedirectToAction("Index", "Dates");
            }

            // came from DR
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "DateRequest")
            {

                System.Diagnostics.Debug.WriteLine("YEAH!!!!");

                return RedirectToAction("Index", "UserDateRequests");
            }


            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
