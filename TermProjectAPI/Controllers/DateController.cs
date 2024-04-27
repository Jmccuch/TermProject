using Microsoft.AspNetCore.Mvc;
using Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Azure.Core;

namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/Date")]

    public class DateController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        // get all liker usernames from DB
        [HttpGet("GetUserDates/{username}")]
        public List<DateInfo> GetUserDates(string username)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserDates";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName", username);

            DataSet userDates = objDB.GetDataSetUsingCmdObj(objCommand);

            List <DateInfo> dates = new List<DateInfo>();


            foreach (DataRow record in userDates.Tables[0].Rows)
            {

                DateInfo date = new DateInfo();

                date.dateID = int.Parse(record["DateID"].ToString());
                date.userName1 = record["UserName1"].ToString();
                date.userName2 = record["UserName2"].ToString();
              //  date.date = DateTime.Parse(record["Date"].ToString());
           
                date.description = record["Description"].ToString();

                dates.Add(date);

            }

            return dates;

        }



        // get date betweeb users
        [HttpGet("GetDateBetweenUsers/{loggedInUserName}/{accountUserName}")]
        public DateInfo GetDateBetweenUsers(string loggedInUserName, string accountUserName)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetDateBetweenUsers";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName1", loggedInUserName);
            objCommand.Parameters.AddWithValue("@userName2", accountUserName);

            DataSet userDates = objDB.GetDataSetUsingCmdObj(objCommand);

            DateInfo date = new DateInfo();

            foreach (DataRow record in userDates.Tables[0].Rows)
            {

                date.dateID = int.Parse(record["DateID"].ToString());
                date.userName1 = record["UserName1"].ToString();
                date.userName2 = record["UserName2"].ToString();

                if (record != null && record["Date"] != null)
                {
                    try
                    {
                        date.dateAndTime0 = DateTime.Parse(record["Date"].ToString());
                    }
                    
                    catch (Exception ex)
                    {
                        // null
                        date.dateAndTime0 = DateTime.Now;
                    }
                }


                date.description = record["Description"].ToString();


            }

            return date;

        }

        public class DateDto
        { 
                public string LoggedInUsername { get; set; }
                public string Requestee { get; set; }
                public string Description { get; set; }
                public DateTime DateAndTime0 { get; set; }
            

        }

        // update date
        [HttpPut("UpdateDate")]
        public void UpdateDate([FromBody] DateDto dateDto) {

          

            System.Diagnostics.Debug.WriteLine("UPADTAING DATE " + dateDto.LoggedInUsername);
            System.Diagnostics.Debug.WriteLine("UPADTAING DATE " + dateDto.DateAndTime0);
            System.Diagnostics.Debug.WriteLine("UPADTAING DATE " + dateDto.Requestee);
            System.Diagnostics.Debug.WriteLine("UPADTAING DATE " + dateDto.Description);

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateDate";

            // add param to obj command
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName1", dateDto.LoggedInUsername);
            objCommand.Parameters.AddWithValue("@userName2", dateDto.Requestee);
            objCommand.Parameters.AddWithValue("@date", dateDto.DateAndTime0);
            objCommand.Parameters.AddWithValue("@description", dateDto.Description);

            // update
            objDB.DoUpdateUsingCmdObj(objCommand);


        }



        [HttpGet("GetDates")]
        public List<DateInfo> GetDates()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllDates";
            objCommand.Parameters.Clear();


            DataSet allDates = objDB.GetDataSetUsingCmdObj(objCommand);

            List<DateInfo> dates = new List<DateInfo>();


            foreach (DataRow record in allDates.Tables[0].Rows)
            {
                DateInfo date = new DateInfo();

                date.dateID = int.Parse(record["DateID"].ToString());


                dates.Add(date);

            }

            return dates;

        }


        public class AddDateDto
        {
            public int DateID { get; set; }
            public string AccepterUserName { get; set; }
            public string RequesteeUserName { get; set; }
        }



        [HttpPost("AddNewDate")]
        public void AddNewDate([FromBody] AddDateDto date)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewDate";
            objCommand.Parameters.Clear();


            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@userName2", date.AccepterUserName);
            objCommand.Parameters.AddWithValue("@userName1", date.RequesteeUserName);
            objCommand.Parameters.AddWithValue("@dateID", date.DateID);


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);


        }



    }
}
