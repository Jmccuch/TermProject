using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TermProjectAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;


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

        public async Task<IActionResult> GetRestaurants(string location)
        {
            var client = _clientFactory.CreateClient();

            string apiUrl = $"https://localhost:5281/api/restaurants?location={location}"; 

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var restaurants = JObject.Parse(responseBody);

                return View(restaurants);
            }
            else
            {
                return Content("Error getting restaurants");
            }
        }






        public IActionResult Index()
        {
            return View();
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


        public IActionResult Submit()
        {
            string redirect = HttpContext.Session.GetString("DateRequestRedirectedFrom");
            string username = HttpContext.Session.GetString("DRusername");
            string name = HttpContext.Session.GetString("DRname");

            // make new match and then update 
            if (redirect == "Matches")
            {

                // set profile being sent Date request
                string requestee = username;

                string loggedInUsername = HttpContext.Session.GetString("Username");


                // get next avaible request Id
                int requestID = GetRequestID();

                api.AddNewDateRequest(requestID, loggedInUsername, requestee);

                HttpContext.Session.SetString("DateRequestedUser", name);


              // update 
                





                return RedirectToAction("Index", "Matches");
            }

            //upda
            else
            {
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
