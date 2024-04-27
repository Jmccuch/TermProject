using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class My2048Controller : Controller
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

            return View();
        }

        public IActionResult FinalScore(string score)
        {
            System.Diagnostics.Debug.WriteLine("SCOOOOOOOORE: " + score);

            string username = HttpContext.Session.GetString("Username");

            User user = api.GetUserInfo(username);

            user.finalScore = int.Parse(score);

            api.UpdateUserInfo(user, username);

            System.Diagnostics.Debug.WriteLine("UPDATING Score: " + score);

            return RedirectToAction("Index","Dashboard");
        }
    }
}
