using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data;
using Utilities;
using System.Data.SqlClient;

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

          
        }



        [HttpPut("AddNewDateRequest")]
        public void AddNewDateRequest([FromBody] AddNewDateRequestInfoDto info)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewDateRequest";

            objCommand.Parameters.Clear();


            // Add params for stored procedure
            objCommand.Parameters.AddWithValue("@receiver", info.Requestee);
            objCommand.Parameters.AddWithValue("@sender", info.LoggedInUsername);
            objCommand.Parameters.AddWithValue("@requestID", info.RequestID);
            objCommand.Parameters.AddWithValue("@viewed", "No");


            // add to db 
            objDB.DoUpdateUsingCmdObj(objCommand);

        }



    }
}
