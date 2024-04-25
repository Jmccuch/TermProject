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


        // return match ds of users matches
        [HttpGet("GetUserMatches")]
        public List<Match> GetUserMatches(UserAccount account)
        {
            System.Diagnostics.Debug.WriteLine("UN:" + account.userName);

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserMatches";

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


    }
}
