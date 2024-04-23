using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class LoginController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("INDEX:" + Request.Cookies.ContainsKey("Username"));

            // check if cookie is null
            if (Request.Cookies.ContainsKey("Username"))
            {
                string? UserName = Request.Cookies["Username"];


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


        public IActionResult Login()
        {


            return RedirectToAction("Index", "Dashboard");
        }
    }
}
