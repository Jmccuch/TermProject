using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Utilities;

namespace TermProjectAPI.Controllers
{

    public class LikesAPIController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        // get all liker usernames from DB
        [HttpGet("GetLikers/{username}")]
         public List<string> GetLikers(string username)
        {

            // add param to obj command
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLikers";


            // add param to obj command
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@likedUserName", username);

            DataSet likerDS = objDB.GetDataSetUsingCmdObj(objCommand);

            List<string> likers = new List<string>();

            foreach (DataRow record in likerDS.Tables[0].Rows) {

                likers.Add(record["UserName"].ToString());

      


            }
            return likers;

        }





        // return like ds 
        [HttpGet("GetLikeIDs")]
        public List<int> GetLikeIDs()
        {



            // get likes
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLikes";

            objCommand.Parameters.Clear();


            DataSet allLikesDS = objDB.GetDataSetUsingCmdObj(objCommand);


            List<int> likes = new List<int>();

            foreach (DataRow record in allLikesDS.Tables[0].Rows)
            {

                likes.Add(int.Parse(record["LikeNumber"].ToString()));


            }

           return likes;

        }


        public class LikeInfoDto
        {
            public int LikeID { get; set; }
            public string Liker { get; set; }
            public string LikedUser { get; set; }
        }

        [HttpPut("AddNewLike")]
        public void AddNewLike([FromBody] LikeInfoDto likeInfo)
        {
          

            // add like to db 
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewLike";
            objCommand.Parameters.Clear();


            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@userName", likeInfo.Liker);
            objCommand.Parameters.AddWithValue("@likedUserName", likeInfo.LikedUser);
            objCommand.Parameters.AddWithValue("@likeNumber", likeInfo.LikeID);


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);


        }

        public class RemoveLikeInfoDto
        {
            public string LoggedInUserName { get; set; }
            public string RemoveUserName { get; set; }
        }



        // remove like from
        [HttpDelete("RemoveLike")]
        public void RemoveLike([FromBody] RemoveLikeInfoDto removeLikeInfo)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveLike";
            objCommand.Parameters.Clear();


            // set remove parameters
            objCommand.Parameters.AddWithValue("@likedUserName", removeLikeInfo.LoggedInUserName);
            objCommand.Parameters.AddWithValue("@userName", removeLikeInfo.RemoveUserName);


            //remove
            objDB.DoUpdateUsingCmdObj(objCommand);


        }


      

        // return like ds 
        [HttpGet("GetLikerUsernames")]
        public List<string> GetLikerUsernames()
        {
            System.Diagnostics.Debug.WriteLine("Getting LIKES");


            // get likes
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLikes";

            objCommand.Parameters.Clear();


            DataSet allLikesUsernamesDS = objDB.GetDataSetUsingCmdObj(objCommand);


            List<string> usernames = new List<string>();

            foreach (DataRow record in allLikesUsernamesDS.Tables[0].Rows)
            {
                string username;

                username = record["UserName"].ToString();
                
                usernames.Add(username);

            }


            return usernames;

        }



        // return like ds 
        [HttpGet("GetLikedUsernames")]
        public List<string> GetLikedUsernames()
        {
            System.Diagnostics.Debug.WriteLine("Getting LIKES");


            // get likes
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLikes";

            objCommand.Parameters.Clear();


            DataSet allLikesUsernamesDS = objDB.GetDataSetUsingCmdObj(objCommand);


            List<string> usernames = new List<string>();

            foreach (DataRow record in allLikesUsernamesDS.Tables[0].Rows)
            {
                string username;

                username = record["LikedUserName"].ToString();

                usernames.Add(username);

            }


            return usernames;

        }


        // return like ds 
        [HttpGet("GetAll2048")]
        public List<int> GetAll2048()
        {

            // get likes
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAll2048";

            objCommand.Parameters.Clear();


            DataSet all2048 = objDB.GetDataSetUsingCmdObj(objCommand);


            List<int> scores = new List<int>();

            foreach (DataRow record in all2048.Tables[0].Rows)
            {

                scores.Add(int.Parse(record["Score"].ToString()));


            }

            return scores;

        }



    }
}
