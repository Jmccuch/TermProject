using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TermProjectAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TermProject.Controllers
{
    public class DateRequestController : Controller
    {
        API api = new API();

        private readonly IHttpClientFactory _clientFactory;

        public DateRequestController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

     /*   public async Task<IActionResult> GetRestaurants(string location)

        {
           





        }
*/





        public IActionResult Index()
        {
            string redirect = HttpContext.Session.GetString("DateRequestRedirectedFrom");
          


            // new
            if (redirect == "Matches")
            {
                System.Diagnostics.Debug.WriteLine("passing empty date");

                DateInfo date = new DateInfo();

                return View(date);
            }

            // populate
            else {


             
                string loggedInUserName = HttpContext.Session.GetString("Username");
                string accountUserName = HttpContext.Session.GetString("DateWithUsername");

                DateInfo date = api.GetDateBetweenUsers(loggedInUserName, accountUserName);

                System.Diagnostics.Debug.WriteLine("DB 123 USERS");
                System.Diagnostics.Debug.WriteLine(date.dateID + date.userName1 + date.userName2);
                System.Diagnostics.Debug.WriteLine(date.dateAndTime0);
     


                return View(date);
            }

               
        }


        public IActionResult Back()
        {
            string redirect = HttpContext.Session.GetString("DateRequestRedirectedFrom");

            if (redirect == "Matches")
            {
                return RedirectToAction("Index", "Matches");
            }
            else
            {
                return RedirectToAction("Index", "Dates");
            }
        }


        public IActionResult Update(string description, DateTime dateAndTime0)
        {

            System.Diagnostics.Debug.WriteLine("YYYYYYYYYYYYYYYYYYYY");


            System.Diagnostics.Debug.WriteLine("Y " + dateAndTime0);
            System.Diagnostics.Debug.WriteLine("Y " + description);
           
            string redirect = HttpContext.Session.GetString("DateRequestRedirectedFrom");
            string accountUserName = HttpContext.Session.GetString("DateWithUsername");
            string name = HttpContext.Session.GetString("DRname");


            // set profile being sent Date request
            string requestee = accountUserName;

            string loggedInUsername = HttpContext.Session.GetString("Username");


            // make new match and then update 
            if (redirect == "Matches")
            {


                // get next avaible request Id
                int requestID = GetRequestID();


                System.Diagnostics.Debug.WriteLine("GG " + requestID );

                System.Diagnostics.Debug.WriteLine("GG " + loggedInUsername);


                System.Diagnostics.Debug.WriteLine("GG " + requestee);


                api.AddNewDateRequest(requestID, loggedInUsername, requestee);

                HttpContext.Session.SetString("DateRequestedUser", name);

          
                return RedirectToAction("Index", "Matches");
            }

            //upda
            else
            {
                // update 

                System.Diagnostics.Debug.WriteLine("HERRRRRR UP DATE ");

                api.UpdateDate(loggedInUsername, requestee, description, dateAndTime0);

                HttpContext.Session.SetString("DateUpdated", "Yes");

                return RedirectToAction("Index", "Dates");
            }
        }

        private int GetRequestID()
        {   // keep track of request id
            int requestID = 0;

            // get next highest requestID

            // Retrieve dataset containing all requestID from the ds
            List<DateRequest> allRequestIDs = api.GetDateRequestIDs();

            // Check if ds contains any rows
            if (allRequestIDs != null && allRequestIDs.Count > 0)
            {
                // go through each row in request ds
                foreach (DateRequest request in allRequestIDs)
                {

                    // Compare current id with the existing max id
                    if (request.requestID > requestID)
                    {
                        // Update the max id
                        requestID = request.requestID;
                    }
                }

                // increase the max request ID 
                requestID++;
            }
            else
            {
                //if no requests are found return 1 
                return 1;
            }

            // Return the next request ID
            return requestID;

        }


    }
}
