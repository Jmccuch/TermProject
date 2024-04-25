using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        API api = new API();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string username, string answer1, string answer2, string answer3, string password)
        {
            bool goodAccount = CheckUserAccount(username, answer1, answer2, answer3);

            System.Diagnostics.Debug.WriteLine("B4 UpdatePassword");

            if (goodAccount)
            {
                api.UpdateUserPassword(username, password);

                System.Diagnostics.Debug.WriteLine("AFTER UpdatePassword");

                return RedirectToAction("Index", "Login");
            }
            System.Diagnostics.Debug.WriteLine("username: " + username);
            System.Diagnostics.Debug.WriteLine("password: " + password);
            System.Diagnostics.Debug.WriteLine("a1: " + answer1);

            ViewBag.Invalid = "Incorrect Information. Try Again.";
            return View("Index");
        }

        private Boolean CheckUserAccount(string username, string a1, string a2, string a3)
        {
            List<UserAccount> accounts = api.GetUserAccount();

            bool accountValid = false;

            foreach (UserAccount account in accounts)
            {

                System.Diagnostics.Debug.WriteLine("comparing account info:" + account.userName + "with" + username);

                if (account.userName == username && account.answer1 == a1 && account.answer2 == a2 && account.answer3 == a3)
                {

                    System.Diagnostics.Debug.WriteLine("found account match");

                    accountValid = true;

                }

            }

            return accountValid;


        }
    }
}
