using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TermProject.Models;
using TermProjectAPI;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net;


namespace TermProject.Controllers
{
    public class LoginController : Controller
    {
        API api = new API();

        string cookieUserNname;


        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };

        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

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


                // check if remember me is selected

                bool addToCookie = Request.Form["rememberMe"] == "on";
                if (addToCookie == true)
                {

                    // add cookie
                    System.Diagnostics.Debug.WriteLine("COOKIE!");

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);

                    if (username != null)
                    {

                        Response.Cookies.Append("Username", username, options);


                    }

                }

                else
                {
                    // Remove cookie
                    if (Request.Cookies["Username"] == username)
                    {
                        // Delete the "Username" cookie
                        Response.Cookies.Delete("Username");
                    }
                }

                // save username before redirecting
                HttpContext.Session.SetString("Username", username);

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

                string decryptP = DecryptPassword(account.password);

                System.Diagnostics.Debug.WriteLine("comparing account info:" + account.userName + "with" + username);
                System.Diagnostics.Debug.WriteLine("comparing account info:" + decryptP + "with" + password);

                if (account.userName == username && decryptP == password)
                {

                    System.Diagnostics.Debug.WriteLine("found account match");

                    accountValid = true;

                }

            }

            return accountValid;


        }

        public IActionResult RedirectForgotPassword()
        {
            return RedirectToAction("Index", "ForgotPassword");
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
