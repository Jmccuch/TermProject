using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using TermProjectAPI; 

namespace TermProjectAPI.Controllers
{
    public class MainController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        [HttpGet("GetUserPotentialMatches/{username}")]
        public List<User> GetUserPotentialMatches(string username)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserPotentialMatches";

            System.Diagnostics.Debug.WriteLine("USERNAME TO REMOVE:" + username);

            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@userName", username);
            objCommand.Parameters.AddWithValue("@accountVisible", "Yes");

            DataSet userAccounts = objDB.GetDataSetUsingCmdObj(objCommand);

            List<User> profiles = new List<User>();

            foreach (DataRow record in userAccounts.Tables[0].Rows)
            {
                User profile = new User();
                UserAccount account = new UserAccount();

                account.firstName = record["FirstName"].ToString();
                account.userName = record["UserName"].ToString();
                profile.userAccount = account;

                try
                {
                    if (record["Age"] != null)
                    {
                        profile.age = int.Parse(record["Age"].ToString());
                    }
                }
                catch (FormatException)
                {
                    // null
                    profile.age = 0;
                }

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
        }
    }
}
