using Microsoft.AspNetCore.Mvc;
using System.Data;
using TermProject.Models;
using TermProjectAPI;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Numerics;

namespace TermProject.Controllers
{
    public class SignUpController : Controller
    {
        API api = new API();

        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };

        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

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

                        string encryptP = EncryptPassword(account.Password);
                         accountToAdd.password = encryptP;

                        string encryptA1 = EncryptPassword(account.Answer1);
                        accountToAdd.answer1 = encryptA1;
                        string encryptA2 = EncryptPassword(account.Answer2);
                        accountToAdd.answer2 = encryptA2;
                        string encryptA3 = EncryptPassword(account.Answer3);
                        accountToAdd.answer3 = encryptA3;


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

        private string EncryptPassword(string password)

        {

            String encryptedPassword;

            UTF8Encoding encoder = new UTF8Encoding();

            Byte[] textBytes;

            textBytes = encoder.GetBytes(password);

            RijndaelManaged rmEncryption = new RijndaelManaged();
            MemoryStream myMemoryStream = new MemoryStream();

            CryptoStream myEncryptionStream = new CryptoStream(myMemoryStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

            myEncryptionStream.Write(textBytes, 0, textBytes.Length);

            myEncryptionStream.FlushFinalBlock();

            myMemoryStream.Position = 0;

            Byte[] encryptedBytes = new Byte[myMemoryStream.Length];

            myMemoryStream.Read(encryptedBytes, 0, encryptedBytes.Length);

            myEncryptionStream.Close();

            myMemoryStream.Close();

            encryptedPassword = Convert.ToBase64String(encryptedBytes);

            return encryptedPassword;


        }

        private string DecryptPassword(string password)

        {

            String encryptedPassword = password;

            Byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);

            Byte[] textBytes;

            String plainTextPassword;

            UTF8Encoding encoder = new UTF8Encoding();


            RijndaelManaged rmEncryption = new RijndaelManaged();

            MemoryStream myMemoryStream = new MemoryStream();

            CryptoStream myDecryptionStream = new CryptoStream(myMemoryStream, rmEncryption.CreateDecryptor(key, vector), CryptoStreamMode.Write);


            myDecryptionStream.Write(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);

            myDecryptionStream.FlushFinalBlock();


            myMemoryStream.Position = 0;

            textBytes = new Byte[myMemoryStream.Length];

            myMemoryStream.Read(textBytes, 0, textBytes.Length);

            myDecryptionStream.Close();

            myMemoryStream.Close();


            plainTextPassword = encoder.GetString(textBytes);

            return plainTextPassword;


        }



    }

}
