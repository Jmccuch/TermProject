using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class LoginController : Controller
    {
        API api = new API();

        string cookieUserNname;

        public IActionResult Index()
        {
           
              
            

            // check if cookie is null
            if (Request.Cookies.ContainsKey("Username"))
            {
                string? UserName = Request.Cookies["Username"];

                cookieUserNname = UserName;


                // get users password with cookie username
               // DataSet passwordDS = account.GetUserPassword(UserName);

               // string? password = "";

               // foreach (DataRow row in passwordDS.Tables[0].Rows)
               // {


                 //   System.Diagnostics.Debug.WriteLine(row["Password"].ToString());
                  //  password = row["Password"].ToString();



              //  }



                LoginModel login = new LoginModel();
               // login.Password = password;
                login.UserName = UserName;


                // return view with login model
                return View("~/Views/Login/Index.cshtml", login);
            }


            else
            {
             
     
                return View("~/Views/Login/Index.cshtml");
            }

        }



        public IActionResult Login(string username, string password)
        {
            bool goodAccount = CheckLoginInfoMAtches(username, password);

            if (goodAccount)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Invalid = "The entered Username and Password do not match!";
                return View("Index"); 
            }
        }


        private Boolean CheckLoginInfoMAtches(string username, string password)
        {
            List<UserAccount> accounts = api.GetUserAccount();


            bool accountValid = false;

            foreach (UserAccount account in accounts)
            {

                System.Diagnostics.Debug.WriteLine("comparing account info:" + account.userName + "with" + username);
                System.Diagnostics.Debug.WriteLine("comparing account info:" + account.password + "with" + password);

                if (account.userName == username && account.password == password) {

                    System.Diagnostics.Debug.WriteLine("found account match");

                    accountValid = true;
                
                }

            }

            return accountValid;

 
        }

    }
}
