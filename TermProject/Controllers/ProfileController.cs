using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class ProfileController : Controller
    {

        API api = new API();

        public IActionResult Index(string username)
        {
            

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

            System.Diagnostics.Debug.WriteLine(name);

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
                return View("~/Views/Matches/Index.cshtml");
            }

            // came from DR
            if (HttpContext.Session.GetString("ViewProfileRedirectedFrom") == "DateRequest")
            {
                return View("~/Views/DateRequest/Index.cshtml");
            }

            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
