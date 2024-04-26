using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThirdPartyAPI.Controllers
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class RestaurantsController : ControllerBase
        {
            private readonly HttpClient _httpClient;

            public RestaurantsController(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient();
            }

            [HttpGet]
            public async Task<ActionResult<JObject>> GetRestaurantsAsync(string location)
            {
                string apiKey = "yXCUfjmclgFJh9JZEaboPbPHph_IwO2gfNzKn99Yj6DyapMGmrgVEI4YLR411n8AwHwRW1A1n8bordHFWhXqxNkb8UGmdtOUKFfbpTtRP5saOYFOb9SwsJeRKI8rZnYx";
                string apiUrl = $"https://api.yelp.com/v3/businesses/search?term=restaurants&location={location}&limit=3";

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(JObject.Parse(responseBody));
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error getting restaurants");
                }
            }


        }
}
