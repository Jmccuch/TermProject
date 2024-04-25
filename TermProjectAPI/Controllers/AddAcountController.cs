using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;
using System.Security.Principal;
using Utilities;
using System.Data.SqlClient;

namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/AddAccount")]
    public class AddAcountController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        [HttpPut("AddNewUserAccount")]
        public void AddNewUserAccount([FromBody] UserAccount userAccount)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewUserAccount";

            // add param to obj command
            objCommand.Parameters.Clear();

            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@userName", userAccount.userName);
            objCommand.Parameters.AddWithValue("@password", userAccount.password);
            objCommand.Parameters.AddWithValue("@firstName", userAccount.firstName);
            objCommand.Parameters.AddWithValue("@lastName", userAccount.lastName);
            objCommand.Parameters.AddWithValue("@email", userAccount.email);

            objCommand.Parameters.AddWithValue("@securityAnswer1", userAccount.answer1);
            objCommand.Parameters.AddWithValue("@securityAnswer2", userAccount.answer2);
            objCommand.Parameters.AddWithValue("@securityAnswer3", userAccount.answer3);

            // isnert into DB
            objDB.DoUpdate(objCommand);



            // make a gallery for new account
            MakeNewGallery(userAccount.userName);




        }

        private void MakeNewGallery(string userName)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddImageGallery";

            // add params to obj command
            objCommand.Parameters.Clear();


            System.Diagnostics.Debug.WriteLine("GALLERY " + userName);

            objCommand.Parameters.AddWithValue("@Username", userName);


            objDB.DoUpdateUsingCmdObj(objCommand);
        }



        [HttpGet("GetUserAccount")]
        public List<UserAccount> GetUserAccount()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserAccount";
            objCommand.Parameters.Clear();
  

            DataSet userAccounts = objDB.GetDataSetUsingCmdObj(objCommand);

            List<UserAccount> accounts = new List<UserAccount>();

            foreach (DataRow record in userAccounts.Tables[0].Rows)
            {
                UserAccount account = new UserAccount();

                account.userName = record["UserName"].ToString();

                account.password = record["Password"].ToString();


                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetSecurityAnswersViaUsername";
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@Username", account.userName);

                DataSet questions = objDB.GetDataSetUsingCmdObj(objCommand);

                foreach (DataRow row in questions.Tables[0].Rows)
                {

                    account.answer1 = row["SecurityAnswer1"].ToString();

                    account.answer2 = row["SecurityAnswer2"].ToString();

                    account.answer3 = row["SecurityAnswer3"].ToString();
                }

                accounts.Add(account);
            }

            return accounts;
        }

        public class PasswordUpdateDto
        {
            public string username { get; set; }
            public string newPassword { get; set; }
        }

        [HttpPut("UpdateUserPassword")]
        public void UpdateUserPassword([FromBody] PasswordUpdateDto info)
        {
            System.Diagnostics.Debug.WriteLine("USERNAME" + info.username);
            System.Diagnostics.Debug.WriteLine("PASSWORD" + info.newPassword);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserPassword";

            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@Username", info.username);
            objCommand.Parameters.AddWithValue("@Password", info.newPassword);

            objDB.DoUpdate(objCommand);

        }
    }
}
