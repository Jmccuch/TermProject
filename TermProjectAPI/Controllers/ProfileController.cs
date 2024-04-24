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

                System.Diagnostics.Debug.WriteLine("AGE VALUE: " + record["Age"].ToString());    

                if (record["Age"].ToString() != "") {

                    profile.age = int.Parse(record["Age"].ToString());

                }

                if (record["Height"].ToString() != "")
                {

                    profile.height = float.Parse(record["Height"].ToString());

                }

                if (record["Age"].ToString() != "")
                {
                    profile.weight = float.Parse(record["Weight"].ToString());
                }

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


            System.Diagnostics.Debug.WriteLine("ADDING: ");
            System.Diagnostics.Debug.WriteLine("Username: " + username);
            System.Diagnostics.Debug.WriteLine("ProfileImage: " + user.profileImage);
            System.Diagnostics.Debug.WriteLine("PhoneNumber: " + user.phoneNumber);
            System.Diagnostics.Debug.WriteLine("Address: " + user.address);
            System.Diagnostics.Debug.WriteLine("City: " + user.city);
            System.Diagnostics.Debug.WriteLine("State: " + user.state);
            System.Diagnostics.Debug.WriteLine("Occupation: " + user.occupation);
            System.Diagnostics.Debug.WriteLine("Description: " + user.description);
            System.Diagnostics.Debug.WriteLine("Age: " + user.age);
            System.Diagnostics.Debug.WriteLine("Weight: " + user.weight.ToString("0"));
            System.Diagnostics.Debug.WriteLine("Height: " + user.height.ToString("0.00"));

            System.Diagnostics.Debug.WriteLine("Like1: " + user.likes[0]);
            System.Diagnostics.Debug.WriteLine("Like2: " + user.likes[1]);
            System.Diagnostics.Debug.WriteLine("Like3: " + user.likes[2]);
            System.Diagnostics.Debug.WriteLine("Like4: " + user.likes[3]);
            System.Diagnostics.Debug.WriteLine("Like5: " + user.likes[4]);

            System.Diagnostics.Debug.WriteLine("Dislike1: " + user.dislikes[0]);
            System.Diagnostics.Debug.WriteLine("Dislike2: " + user.dislikes[1]);
            System.Diagnostics.Debug.WriteLine("Dislike3: " + user.dislikes[2]);
            System.Diagnostics.Debug.WriteLine("Dislike4: " + user.dislikes[3]);
            System.Diagnostics.Debug.WriteLine("Dislike5: " + user.dislikes[4]);

            System.Diagnostics.Debug.WriteLine("Restaurant: " + user.restaurant);
            System.Diagnostics.Debug.WriteLine("Book: " + user.book);
            System.Diagnostics.Debug.WriteLine("Movie: " + user.movie);
            System.Diagnostics.Debug.WriteLine("Quote: " + user.quote);
            System.Diagnostics.Debug.WriteLine("CatOrDog: " + user.catOrDog);

            System.Diagnostics.Debug.WriteLine("CommitmentType: " + user.commitmentType);
            System.Diagnostics.Debug.WriteLine("AccountVisible: " + user.accountVisible);



            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserInfo";

            // add param to obj command
            objCommand.Parameters.Clear();
            
            objCommand.Parameters.AddWithValue("@userName", username);

            objCommand.Parameters.AddWithValue("@profileImage", user.profileImage);
            objCommand.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
            objCommand.Parameters.AddWithValue("@address", user.address);
            objCommand.Parameters.AddWithValue("@city", user.city);
            objCommand.Parameters.AddWithValue("@state", user.state);
            objCommand.Parameters.AddWithValue("@occupation", user.occupation);
            objCommand.Parameters.AddWithValue("@description", user.description);
            objCommand.Parameters.AddWithValue("@age", user.age);
            objCommand.Parameters.AddWithValue("@weight", user.weight.ToString("0"));
            objCommand.Parameters.AddWithValue("@height", user.height.ToString("0.00"));

            objCommand.Parameters.AddWithValue("@like1", user.likes[0]);
            objCommand.Parameters.AddWithValue("@like2", user.likes[1]);
            objCommand.Parameters.AddWithValue("@like3", user.likes[2]);
            objCommand.Parameters.AddWithValue("@like4", user.likes[3]);
            objCommand.Parameters.AddWithValue("@like5", user.likes[4]);

            objCommand.Parameters.AddWithValue("@disLike1", user.dislikes[0]);
            objCommand.Parameters.AddWithValue("@disLike2", user.dislikes[1]);
            objCommand.Parameters.AddWithValue("@disLike3", user.dislikes[2]);
            objCommand.Parameters.AddWithValue("@disLike4", user.dislikes[3]);
            objCommand.Parameters.AddWithValue("@disLike5", user.dislikes[4]);

            objCommand.Parameters.AddWithValue("@restaurant", user.restaurant);
            objCommand.Parameters.AddWithValue("@book", user.book);
            objCommand.Parameters.AddWithValue("@movie", user.movie);
            objCommand.Parameters.AddWithValue("@quote", user.quote);
            objCommand.Parameters.AddWithValue("@catOrDog", user.catOrDog);

            objCommand.Parameters.AddWithValue("@commitmentType", user.commitmentType);

            objCommand.Parameters.AddWithValue("@accountVisible", user.accountVisible);


     

            // update DB
            objDB.DoUpdateUsingCmdObj(objCommand);




        }

    }
}
