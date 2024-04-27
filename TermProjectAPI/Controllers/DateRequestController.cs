using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;
using Utilities;
using System.Data.SqlClient;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TermProjectAPI.Controllers
{
    [Route("TermProjectAPI/DateRequest")]

    public class DateRequestController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        // return like ds 
        [HttpGet("GetDateRequestIDs")]
        public List<DateRequest> GetDateRequestIDs()
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllDateRequests";
            objCommand.Parameters.Clear();


            DataSet allRequests = objDB.GetDataSetUsingCmdObj(objCommand);

            List<DateRequest> requests = new List<DateRequest>();

            foreach (DataRow record in allRequests.Tables[0].Rows)
            {
                DateRequest request = new DateRequest();

                request.requestID = int.Parse(record["RequestID"].ToString());

                requests.Add(request);

            }

            return requests;

        }


        public class AddNewDateRequestInfoDto
        {   
            
            
            public int RequestID { get; set; }
            public string LoggedInUsername { get; set; }
            public string Requestee { get; set; }

            public DateTime DateAndTime0 { get; set; }
          
        }



        [HttpPut("AddNewDateRequest")]
        public void AddNewDateRequest([FromBody] AddNewDateRequestInfoDto info)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewDateRequest";

            objCommand.Parameters.Clear();

            System.Diagnostics.Debug.WriteLine(info.Requestee);

            System.Diagnostics.Debug.WriteLine(info.LoggedInUsername);


            System.Diagnostics.Debug.WriteLine(info.Requestee);





            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@receiver", info.Requestee);
            objCommand.Parameters.AddWithValue("@sender", info.LoggedInUsername);
            objCommand.Parameters.AddWithValue("@requestID", info.RequestID);



            objCommand.Parameters.AddWithValue("@viewed", "No");


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);

        }


        // get passed username's date requests from db
        [HttpGet("GetDateRequests/{username}")]
        public List<DateRequest> GetDateRequests(string username)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserDateRequests";
            objCommand.Parameters.Clear();


            objCommand.Parameters.AddWithValue("@receiver", username);


            
            DataSet allRequests = objDB.GetDataSetUsingCmdObj(objCommand);

            List<DateRequest> requests = new List<DateRequest>();

            foreach (DataRow record in allRequests.Tables[0].Rows)
            {
                DateRequest request = new DateRequest();

                request.requestID = int.Parse(record["RequestID"].ToString());

                request.sender = record["Sender"].ToString();

                request.receiver = record["Receiver"].ToString();

                request.viewed = record["Viewed"].ToString();

                requests.Add(request);

            }

            return requests;


        }


        public class RemoveRequestDto
        {
            public string AccepterUserName { get; set; }
            public string RequesteeUserName { get; set; }
        }



        // remove like from
        [HttpDelete("RemoveDateRequest")]
        public void RemoveDateRequest([FromBody] RemoveRequestDto removeRequest)
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveDateRequest";
            objCommand.Parameters.Clear();


            // set remove parameters
            objCommand.Parameters.AddWithValue("@receiver", removeRequest.AccepterUserName);
            objCommand.Parameters.AddWithValue("@sender", removeRequest.RequesteeUserName);


            //remove
            objDB.DoUpdateUsingCmdObj(objCommand);

        }


        public class UpdateUserDateRequestViewDto
        {


            public string LoggedInUserName { get; set; }
      

        }


        [HttpPut("UpdateUserDateRequestView")]
        public void UpdateUserDateRequestView([FromBody] UpdateUserDateRequestViewDto info)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserDateRequestView";

            // add param to obj command
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@receiver", info.LoggedInUserName);
            objCommand.Parameters.AddWithValue("viewed", "Yes");

            // update
            objDB.DoUpdateUsingCmdObj(objCommand);

        }



    }
}
