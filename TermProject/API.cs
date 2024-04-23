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

    }
}
