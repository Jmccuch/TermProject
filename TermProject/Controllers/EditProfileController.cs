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
            string username = HttpContext.Session.GetString("Username");

           

            api.UpdateUserInfo(user, username);


            return View("~/Views/EditProfile/Index.cshtml");
        }

    }
}
