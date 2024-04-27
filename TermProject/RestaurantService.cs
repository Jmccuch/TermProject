using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TermProject.Models;
using Azure.Core;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
using TermProjectAPI;

namespace TermProject
{
    public class RestaurantService
    {


        public List<RestaurantViewModel> GetRestaurantsNearby(string location)

        {
            location = "new-york-city";

            string route = $"http://localhost:5281/RestaurantAPI/GETR/GetRestaurantsNearby/{location}";


            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<RestaurantViewModel> restaurants = JsonSerializer.Deserialize<List<RestaurantViewModel>>(jsonData);

            return restaurants;


        }

    }
}
