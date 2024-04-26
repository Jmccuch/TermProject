using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;
using static TermProjectAPI.Controllers.LikesAPIController;
using Utilities;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;


namespace TermProjectAPI.Controllers
{  
    
    [Route("TermProjectAPI/Match")]
    public class MatchController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        public class AddMatchInfonfoDto
        {
            public int matchID { get; set; }
            public string liker { get; set; }
            public string likedUser { get; set; }
        }



        [HttpPost("AddNewMatch")]
        public void AddNewMatch([FromBody] AddMatchInfonfoDto match)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewMatch";

            objCommand.Parameters.Clear();


            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@userName1", match.liker);
            objCommand.Parameters.AddWithValue("@userName2", match.likedUser);
            objCommand.Parameters.AddWithValue("@matchID", match.matchID);
            objCommand.Parameters.AddWithValue("@viewed", "No");


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);




            // add again with opposite usernames
            objCommand.Parameters.Clear();


            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@userName1", match.likedUser);
            objCommand.Parameters.AddWithValue("@userName2", match.liker);
            objCommand.Parameters.AddWithValue("@matchID", match.matchID + 1);
            objCommand.Parameters.AddWithValue("@viewed", "No");


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);


        }


        // return match username 1 list
        [HttpGet("GetMatchUsername1")]
        public List<string> GetMatchUsername1()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetMatches";
            objCommand.Parameters.Clear();


            DataSet username1DS = objDB.GetDataSetUsingCmdObj(objCommand);

            List<string> usernames = new List<string>();

            foreach (DataRow record in username1DS.Tables[0].Rows)
            {
                Match match = new Match();

            
                string userName1 = record["UserName1"].ToString();


                usernames.Add(userName1);
            }

            return usernames;

        }




        // return match username 1 list
        [HttpGet("GetMatchUsername2")]
        public List<string> GetMatchUsername2()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetMatches";
            objCommand.Parameters.Clear();


            DataSet username1DS = objDB.GetDataSetUsingCmdObj(objCommand);

            List<string> usernames = new List<string>();

            foreach (DataRow record in username1DS.Tables[0].Rows)
            {
                Match match = new Match();


                string userName2 = record["UserName2"].ToString();


                usernames.Add(userName2);
            }

            return usernames;

        }




        // return match ds of users matches
        [HttpGet("GetUserMatches")]
        public List<Match> GetUserMatches([FromBody] UserAccount account)
        {
            System.Diagnostics.Debug.WriteLine("UN:" + account.userName);

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetMatches";

            // add params
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@userName1", account.userName);

            DataSet usersMatches = objDB.GetDataSetUsingCmdObj(objCommand);

            List<Match> matches = new List<Match>();

            // matches ds
            foreach (DataRow record in usersMatches.Tables[0].Rows)
            {
                Match match = new Match();

                match.matchID = int.Parse(record["MatchID"].ToString());
                match.userName1 = record["UserName1"].ToString();
                match.userName1 = record["UserName2"].ToString();
                match.viewed = record["Viewed"].ToString();


                matches.Add(match);
            }

            return matches;
        }

        // return match ds 
        [HttpGet("GetMatches")]
        public List<Match> GetMatches()
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetMatches";
            objCommand.Parameters.Clear();


            DataSet allMatches = objDB.GetDataSetUsingCmdObj(objCommand);

            List<Match> matches = new List<Match>();

            foreach (DataRow record in allMatches.Tables[0].Rows)
            {
                Match match = new Match();

                match.matchID = int.Parse(record["MatchID"].ToString());
                match.userName1 = record["UserName1"].ToString();
                match.userName1 = record["UserName2"].ToString();
                match.viewed = record["Viewed"].ToString();


                matches.Add(match);
            }


            return matches;

        }


        public class RemoveMatchInfoDto
        {
            public string loggedInUserName { get; set; }
            public string removeUserName { get; set; }
        }

        // remove match from db
        [HttpDelete("RemoveMatch")]
        public void RemoveMatch([FromBody] RemoveMatchInfoDto removeMatchInfoDto)
        {


            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveMatch";
            objCommand.Parameters.Clear();


            // set remove parameters
            objCommand.Parameters.AddWithValue("@userName1", removeMatchInfoDto.loggedInUserName);
            objCommand.Parameters.AddWithValue("@userName2", removeMatchInfoDto.removeUserName);


            //remove
            objDB.DoUpdateUsingCmdObj(objCommand);



            // run again with parameters oppostie to remove match from other user

            objCommand.Parameters.Clear();


            // set remove parameters
            objCommand.Parameters.AddWithValue("@userName1", removeMatchInfoDto.removeUserName);
            objCommand.Parameters.AddWithValue("@userName2", removeMatchInfoDto.loggedInUserName);


            //remove
            objDB.DoUpdateUsingCmdObj(objCommand);


        }




        public class UpdateUserMatchesViewDto
        {


            public string LoggedInUserName { get; set; }


        }


        [HttpPut("UpdateUserMatchesView")]
        public void UpdateUserDateRequestView([FromBody] UpdateUserMatchesViewDto info)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserMatchesView";

            // add param to obj command
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName1", info.LoggedInUserName);
            objCommand.Parameters.AddWithValue("viewed", "Yes");

            // update
            objDB.DoUpdateUsingCmdObj(objCommand);

        }



    }
}
