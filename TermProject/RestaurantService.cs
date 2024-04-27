using System;
using System.Net.Http;
using System.Threading.Tasks;

public class RestaurantService
{
    private readonly HttpClient _httpClient;

    public RestaurantService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> GetRestaurantsNearbyAsync(string location)
    {
        string apiUrl = $"http://localhost:5281/restaurants/{location}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to retrieve nearby restaurants: {response.StatusCode}");
        }

        var restaurantsJson = await response.Content.ReadAsStringAsync();

        return restaurantsJson;
    }
}
