using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using TermProjectAPI;

namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/Login")]
    public class LoginController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        /*
        [HttpGet("GetUserInfo/{username}")]
        public User GetUserInfo(string username)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@userName", username);

            DataSet userAccount = objDB.GetDataSetUsingCmdObj(objCommand);

           User profile = new User();
            /
            foreach (DataRow record in userAccount.Tables[0].Rows)
            {
                User profile = new User();
                UserAccount account = new UserAccount();

                account.firstName = record["FirstName"].ToString();
               
                account.firstName = record["FirstName"].ToString();
                profile.userAccount = account;
                profile.age = int.Parse(record["Age"].ToString());
                profile.age = int.Parse(record["Height"].ToString());
                profile.age = int.Parse(record["Weight"].ToString());




                profile.profileImage = record["ProfileImage"].ToString();
                profile.city = record["City"].ToString();
                profile.state = record["State"].ToString();
                profile.description = record["Description"].ToString();
                profile.commitmentType = record["CommitmentType"].ToString();
                profile.occupation = record["Occupation"].ToString();
                profile.catOrDog = record["CatOrDog"].ToString();


                profiles.Add(profile);
            }

            return profiles;
        }*/
    }
}
