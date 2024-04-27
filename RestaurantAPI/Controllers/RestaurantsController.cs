using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("RestaurantAPI/GETR")]
    public class RestaurantsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RestaurantsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("GetRestaurantsNearby/{location}")]
        public async Task<IActionResult> GetRestaurantsNearby(string location)
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

