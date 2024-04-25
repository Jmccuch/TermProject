using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;


namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/Profile")]
    public class ProfileController : Controller
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

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetImageGallery";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Username", username);

            DataSet imageGallery = objDB.GetDataSetUsingCmdObj(objCommand);

            foreach (DataRow record in userAccount.Tables[0].Rows)
            {

                User profile = new User();
                UserAccount account = new UserAccount();


                profile.phoneNumber = record["PhoneNumber"].ToString();
                profile.address = record["Address"].ToString();

                account.firstName = record["FirstName"].ToString();
                account.lastName = record["LastName"].ToString();
                account.email = record["Email"].ToString();
                account.userName = record["UserName"].ToString();

                profile.userAccount = account;

                System.Diagnostics.Debug.WriteLine("ACCOUNT FN: " + profile.userAccount.firstName);    

                if (record["Age"].ToString() != "") {

                    profile.age = int.Parse(record["Age"].ToString());

                }


                if (record["Height"].ToString() != "")
                {

                    profile.height = float.Parse(record["Height"].ToString());

                }

                if (record["Weight"].ToString() != "")
                {
                    profile.weight = float.Parse(record["Weight"].ToString());
                }

                profile.restaurant = record["Restaurant"].ToString();
                profile.movie = record["Movie"].ToString();
                profile.quote = record["Quote"].ToString();
                profile.book = record["Book"].ToString();

                List<string> likes =
                [
                    record["Like1"].ToString(),
                    record["Like2"].ToString(),
                    record["Like3"].ToString(),
                    record["Like4"].ToString(),
                    record["Like5"].ToString(),
                ];
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

                profile.accountVisible = record["AccountVisible"].ToString();

                foreach (DataRow row in imageGallery.Tables[0].Rows)
                {
                    profile.picture1 = row["Picture1"].ToString();
                    profile.picture2 = row["Picture2"].ToString();
                    profile.picture3 = row["Picture3"].ToString();
                }
                    info = profile;
            }

            System.Diagnostics.Debug.WriteLine("Here2 " + info.quote);

            System.Diagnostics.Debug.WriteLine("HERRRRRRRE" + info.userAccount.firstName);

            return info;
        }





        public class UserInfoDto
        {
            public User User { get; set; }
            public string Username { get; set; }
        }



        // update passed users info
        [HttpPut("UpdateUserInfo")]
        public void UpdateUserInfo([FromBody] UserInfoDto userInfo)
        {
            User user = userInfo.User;
            string username = userInfo.Username;



            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserInfo";

            // add params to obj command
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName", username);

            // Check and add profileImage parameter
            if (!string.IsNullOrEmpty(user.profileImage))
                objCommand.Parameters.AddWithValue("@profileImage", user.profileImage);
            else
                objCommand.Parameters.AddWithValue("@profileImage", DBNull.Value);

            // Check and add phoneNumber parameter
            if (!string.IsNullOrEmpty(user.phoneNumber))
                objCommand.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
            else
                objCommand.Parameters.AddWithValue("@phoneNumber", DBNull.Value);

            // Check and add address parameter
            if (!string.IsNullOrEmpty(user.address))
                objCommand.Parameters.AddWithValue("@address", user.address);
            else
                objCommand.Parameters.AddWithValue("@address", DBNull.Value);

            // Check and add city parameter
            if (!string.IsNullOrEmpty(user.city))
                objCommand.Parameters.AddWithValue("@city", user.city);
            else
                objCommand.Parameters.AddWithValue("@city", DBNull.Value);

            // Check and add state parameter
            if (!string.IsNullOrEmpty(user.state))
                objCommand.Parameters.AddWithValue("@state", user.state);
            else
                objCommand.Parameters.AddWithValue("@state", DBNull.Value);

            // Check and add occupation parameter
            if (!string.IsNullOrEmpty(user.occupation))
                objCommand.Parameters.AddWithValue("@occupation", user.occupation);
            else
                objCommand.Parameters.AddWithValue("@occupation", DBNull.Value);

            // Check and add description parameter
            if (!string.IsNullOrEmpty(user.description))
                objCommand.Parameters.AddWithValue("@description", user.description);
            else
                objCommand.Parameters.AddWithValue("@description", DBNull.Value);

            // Check and add age parameter
            if (user.age != 0)
                objCommand.Parameters.AddWithValue("@age", user.age);
            else
                objCommand.Parameters.AddWithValue("@age", DBNull.Value);

            // Check and add weight parameter
            if (user.weight != 0)
                objCommand.Parameters.AddWithValue("@weight", user.weight.ToString("0"));
            else
                objCommand.Parameters.AddWithValue("@weight", DBNull.Value);

            // Check and add height parameter
            if (user.height != 0)
                objCommand.Parameters.AddWithValue("@height", user.height.ToString("0.00"));
            else
                objCommand.Parameters.AddWithValue("@height", DBNull.Value);

            // Check and add likes parameters
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(user.likes[i]))
                    objCommand.Parameters.AddWithValue($"@like{i + 1}", user.likes[i]);
                else
                    objCommand.Parameters.AddWithValue($"@like{i + 1}", DBNull.Value);
            }

            // Check and add dislikes parameters
            for (int i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(user.dislikes[i]))
                    objCommand.Parameters.AddWithValue($"@dislike{i + 1}", user.dislikes[i]);
                else
                    objCommand.Parameters.AddWithValue($"@dislike{i + 1}", DBNull.Value);
            }

            // Check and add restaurant parameter
            if (!string.IsNullOrEmpty(user.restaurant))
                objCommand.Parameters.AddWithValue("@restaurant", user.restaurant);
            else
                objCommand.Parameters.AddWithValue("@restaurant", DBNull.Value);

            // Check and add book parameter
            if (!string.IsNullOrEmpty(user.book))
                objCommand.Parameters.AddWithValue("@book", user.book);
            else
                objCommand.Parameters.AddWithValue("@book", DBNull.Value);

            // Check and add movie parameter
            if (!string.IsNullOrEmpty(user.movie))
                objCommand.Parameters.AddWithValue("@movie", user.movie);
            else
                objCommand.Parameters.AddWithValue("@movie", DBNull.Value);

            // Check and add quote parameter
            if (!string.IsNullOrEmpty(user.quote))
                objCommand.Parameters.AddWithValue("@quote", user.quote);
            else
                objCommand.Parameters.AddWithValue("@quote", DBNull.Value);

            // Check and add catOrDog parameter
            if (!string.IsNullOrEmpty(user.catOrDog))
                objCommand.Parameters.AddWithValue("@catOrDog", user.catOrDog);
            else
                objCommand.Parameters.AddWithValue("@catOrDog", DBNull.Value);

            // Check and add commitmentType parameter
            if (!string.IsNullOrEmpty(user.commitmentType))
                objCommand.Parameters.AddWithValue("@commitmentType", user.commitmentType);
            else
                objCommand.Parameters.AddWithValue("@commitmentType", DBNull.Value);

            // Check and add accountVisible parameter

            System.Diagnostics.Debug.WriteLine("AV  " + user.accountVisible);

            if (!string.IsNullOrEmpty(user.accountVisible))
                objCommand.Parameters.AddWithValue("@accountVisible", user.accountVisible);
            else
                objCommand.Parameters.AddWithValue("@accountVisible", DBNull.Value);

            // update DB
            objDB.DoUpdateUsingCmdObj(objCommand);

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddImageGallery";

            // add params to obj command
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@Username", username);
            objCommand.Parameters.AddWithValue("@Picture1", user.picture1);
            objCommand.Parameters.AddWithValue("@Picture2", user.picture2);
            objCommand.Parameters.AddWithValue("@Picture3", user.picture3);

            objDB.DoUpdateUsingCmdObj(objCommand);

        }

    }
}
