using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProject.Models;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class SignUpController : Controller
    {
        API api = new API();

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateAccount(AccountModel account)
        {


            string submitType = Request.Form["submitType"];

            // create account was pressed
            if (submitType == "create")
            {


                // if model state is valid
                if (ModelState.IsValid)
                {

                    // if username is not taken create account
                    if (IsUsernameAvailable(account) == false)

                    {


                        UserAccount accountToAdd = new UserAccount(account.UserName, account.Password, account.FirstName, 
                            account.LastName, account.Email, account.Answer1, account.Answer2, account.Answer3);


                        api.AddNewUserAccount(accountToAdd);


                        // Check if save to cookie checkbox is checked
                        bool addToCookie = Request.Form["addToCookie"] == "on";

                        // if add to cookie is checked off add username to a cookie
                        if (addToCookie == true)
                        {
                            System.Diagnostics.Debug.WriteLine("COOKIE!");



                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(7);

                            if (account.UserName != null)
                            {

                                Response.Cookies.Append("Username", account.UserName, options);


                            }



                        }

                        ViewBag.AccountAdded = "Your Account has been created!";

                        return View("~/Views/SignUp/Index.cshtml");
                    }

                    // user name not avaiable 
                    else
                    {
                        // add error message to view bag
                        ViewBag.UsernameErrorMessage = "Your selected username is not available. Please select another one.";
                        System.Diagnostics.Debug.WriteLine("HERE: not added");
                        return View("~/Views/SignUp/Index.cshtml");

                    }

                }

                // model state not valid
                else
                {


                    System.Diagnostics.Debug.WriteLine("not added");


                    return View("~/Views/SignUp/Index.cshtml");
                }

            }


            // back was selected
            else
            {
                return View("~/Views/Login/Index.cshtml");

            }


        }



        public bool IsUsernameAvailable(AccountModel account)
        {


            bool isUsernameAvailable = false;


            // check data base if username is available 
            List<UserAccount> accounts = api.GetUserAccount();



            foreach (UserAccount userAccount in accounts)
            {

               
                // not available
                if (account.UserName == userAccount.userName)
                {

                    return true;
                }



            }

            // available
            return isUsernameAvailable;


        }
        
    }
}
