using System;
using System.Collections.Generic; 
using System.IO;
using System.Net;
using System.Text.Json; 
using TermProjectAPI; 

namespace TermProject
{
    public class API
    {


        private string urlAPI = "http://localhost:5281/";

        public List<User> GetUserPotentialMatches(string username)
        {
           
            string route = $"{urlAPI}TermProjectAPI/main/GetUserPotentialMatches/{username}";


            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize JSON string into a list of User objects
            List<User> profiles = JsonSerializer.Deserialize<List<User>>(jsonData);


            return profiles;
                    
                

            
        }



        public User GetUserInfo(string username)
        {

            string route = $"{urlAPI}TermProjectAPI/Profile/GetUserInfo/{username}";


            System.Diagnostics.Debug.WriteLine(route);

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize
           User profile = JsonSerializer.Deserialize<User>(jsonData);


            return profile;




        }

        public void AddNewUserAccount(UserAccount userAccount)
        {
            // Serialize UserAccount 
            string jsonData = JsonSerializer.Serialize(userAccount);

            string route = $"{urlAPI}TermProjectAPI/AddAccount/AddNewUserAccount";
            WebRequest request = WebRequest.Create(route);
            request.Method = "PUT";
            request.ContentType = "application/json";

            // Write data to body
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = request.GetResponse();
            response.Close();
        }



        public List<UserAccount> GetUserAccount()
        {

            string route = $"{urlAPI}TermProjectAPI/AddAccount/GetUseraccount";


            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize JSON string into a list of User objects
            List<UserAccount> accounts = JsonSerializer.Deserialize<List<UserAccount>>(jsonData);


            return accounts;




        }




        public void UpdateUserInfo(User user, string username)
        {
            // Create an anonymous object to hold both user and username
            var requestData = new
            {
                User = user,
                Username = username
            };

            // Serialize the requestData object to JSON
            string jsonData = JsonSerializer.Serialize(requestData);

            // Construct the request URL
            string route = $"{urlAPI}TermProjectAPI/Profile/UpdateUserInfo";

            // Create a web request
            WebRequest request = WebRequest.Create(route);
            request.Method = "PUT"; // Specify the HTTP method
            request.ContentType = "application/json"; // Specify the content type

            // Write data to the request body
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData); // Write JSON data to the stream
                streamWriter.Flush(); // Flush the stream to ensure all data is sent
                streamWriter.Close(); // Close the stream
            }

            // Get the response from the server
            using (WebResponse response = request.GetResponse())
            {
                // Close the response
                response.Close();
            }
        }

    }
}
