using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;


namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/Login")]
    public class LoginController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        
        [HttpGet("GetUserInfo/{username}")]
        public User GetUserInfo(string username)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@userName", username);

            DataSet userAccount = objDB.GetDataSetUsingCmdObj(objCommand);

            User info = new User();

            

            foreach (DataRow record in userAccount.Tables[0].Rows)
            {

                User profile = new User();
                UserAccount account = new UserAccount();


                account.firstName = record["FirstName"].ToString();
                account.lastName = record["LastName"].ToString();
                account.email = record["Email"].ToString();
                account.userName = record["UserName"].ToString();

                profile.userAccount = account;
                profile.age = int.Parse(record["Age"].ToString());
                profile.height = float.Parse(record["Height"].ToString());
                profile.weight = float.Parse(record["Weight"].ToString());


                profile.restaurant = record["Restaurant"].ToString();
                profile.movie = record["Movie"].ToString();
                profile.quote = record["Quote"].ToString();
                profile.book = record["Book"].ToString();

                List<string> likes = new List<string>();
                likes.Add(record["Like1"].ToString());
                likes.Add(record["Like2"].ToString());
                likes.Add(record["Like3"].ToString());
                likes.Add(record["Like4"].ToString());
                likes.Add(record["Like5"].ToString());
                profile.likes = likes;

                List<string> dislikes = new List<string>();
                dislikes.Add(record["Dislike1"].ToString());
                dislikes.Add(record["Dislike2"].ToString());
                dislikes.Add(record["Dislike3"].ToString());
                dislikes.Add(record["Dislike4"].ToString());
                dislikes.Add(record["Dislike5"].ToString());
                profile.dislikes = dislikes;
                


                profile.profileImage = record["ProfileImage"].ToString();
                profile.city = record["City"].ToString();
                profile.state = record["State"].ToString();
                profile.description = record["Description"].ToString();
                profile.commitmentType = record["CommitmentType"].ToString();
                profile.occupation = record["Occupation"].ToString();
                profile.catOrDog = record["CatOrDog"].ToString();


                info = profile;
            }

            System.Diagnostics.Debug.WriteLine(info.userAccount.firstName);

            return info;
        }
    }
}
