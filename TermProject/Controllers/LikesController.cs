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
            System.Diagnostics.Debug.WriteLine("main un: " + username);

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

     
    }
}
