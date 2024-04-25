using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class EditProfileController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {
            // get logged in username
            string username = HttpContext.Session.GetString("Username");

            User user = api.GetUserInfo(username);

            return View(user);
        }
        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Update(User user)
        {
        

             if ((user.weight != 0 && user.height != 0))
             {
            string username = HttpContext.Session.GetString("Username");

                api.UpdateUserInfo(user, username);

                System.Diagnostics.Debug.WriteLine("UPDATING");

                ViewBag.AccountUpdated = "Your Account Has Been Updated!";

                return View("~/Views/EditProfile/Index.cshtml", user);
            }
            
            else {
                System.Diagnostics.Debug.WriteLine("not UPDATING");

                ViewBag.InvalidNumber = "You must enter a valid number for Height and Weight!";

                return View("~/Views/EditProfile/Index.cshtml", user);


            }
        
        }



        public IActionResult RedirectDashboard()
        {

            return RedirectToAction("Index", "Dashboard");
        }

  

    }
}
