using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("RestaurantAPI/GetRestaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RestaurantsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetRestaurants(string location)
        {
            string apiUrl = $"https://opentable.herokuapp.com/api/restaurants?city={location}";

            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to retrieve nearby restaurants");
            }

            var restaurants = await response.Content.ReadAsStringAsync();
            return Ok(restaurants);
        }
    }
}
