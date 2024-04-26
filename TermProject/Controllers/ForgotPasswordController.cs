using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        API api = new API();


        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };

        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };
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
                string encryptP = EncryptPassword(password);
                api.UpdateUserPassword(username, encryptP);

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

                string decryptA1 = DecryptPassword(account.answer1);
                string decryptA2 = DecryptPassword(account.answer2);
                string decryptA3 = DecryptPassword(account.answer3);

                System.Diagnostics.Debug.WriteLine("comparing account info:" + account.userName + "with" + username);

                if (account.userName == username && decryptA1 == a1 && decryptA2 == a2 && decryptA3 == a3)
                {

                    System.Diagnostics.Debug.WriteLine("found account match");

                    accountValid = true;

                }

            }

            return accountValid;


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
