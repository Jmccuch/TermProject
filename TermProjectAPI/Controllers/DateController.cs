using Microsoft.AspNetCore.Mvc;
using Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/Date")]

    public class DateController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        // get all liker usernames from DB
        [HttpGet("GetUserDates/{username}")]
        public List<Date> GetUserDates(string username)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserDates";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@userName", username);

            DataSet userDates = objDB.GetDataSetUsingCmdObj(objCommand);

            List <Date> dates = new List<Date>();


            foreach (DataRow record in userDates.Tables[0].Rows)
            {

                Date date = new Date();

                date.dateID = int.Parse(record["DateID"].ToString());
                date.userName1 = record["UserName1"].ToString();
                date.userName2 = record["UserName2"].ToString();
              //  date.date = DateTime.Parse(record["Date"].ToString());
                date.time = record["Time"].ToString();
                date.description = record["Description"].ToString();

                dates.Add(date);

            }

            return dates;

        }
    }
}
