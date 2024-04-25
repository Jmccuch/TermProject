using System;
using System.Collections.Generic; 
using System.IO;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
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

            // Deserialize 
            List<UserAccount> accounts = JsonSerializer.Deserialize<List<UserAccount>>(jsonData);


            return accounts;




        }




        public void UpdateUserInfo(User user, string username)
        {
            // obj to hold both
            var requestData = new
            {
                User = user,
                Username = username
            };

            // Serialize 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/Profile/UpdateUserInfo";

  
            WebRequest request = WebRequest.Create(route);
            request.Method = "PUT"; 
            request.ContentType = "application/json"; 

            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData); 
                streamWriter.Flush(); 
                streamWriter.Close(); 
            }

           
            using (WebResponse response = request.GetResponse())
            {
                
                response.Close();
            }
        }

        public void UpdateUserPassword(string username, string newPassword)
        {
            // obj to hold both
            var requestData = new
            {
           
                Username = username,
                NewPassword = newPassword
            };

            // Serialize 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/AddAccount/UpdateUserPassword";


            WebRequest request = WebRequest.Create(route);
            request.Method = "PUT";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }


            using (WebResponse response = request.GetResponse())
            {

                response.Close();
            }
        }


        public List<string> GetLikers(string username)
        {
            string route = $"{urlAPI}TermProjectAPI/Like/GetLikers/{username}";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<string> likers = JsonSerializer.Deserialize<List<string>>(jsonData);

            return likers;
        }



        public List<int> GetLikeIDs()
        {
            string route = $"{urlAPI}TermProjectAPI/Like/GetLikeIDs";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<int> likeIDs = JsonSerializer.Deserialize<List<int>>(jsonData);

            return likeIDs;
        }


        public void AddNewLike(int likeID, string liker, string likedUser)
        {
            System.Diagnostics.Debug.WriteLine("BA: " + likeID);


            // obj to hold data
            var requestData = new
            {
                LikeID = likeID,
                Liker = liker,
                LikedUser = likedUser
            };

            System.Diagnostics.Debug.WriteLine("BA: " + likeID);

            // Serialize all 3 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/Like/AddNewLike";


            WebRequest request = WebRequest.Create(route);
            request.Method = "PUT";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (WebResponse response = request.GetResponse())
            {

                response.Close();
            }


        }



        // remove like
        public class LikeData
        {
            public string LoggedInUserName { get; set; }
            public string RemoveUserName { get; set; }
        }

        public void RemoveLike(string loggedInUserName, string removeUserName)
        {


            // obj to hold data
            var requestData = new
            {
                LoggedInUserName = loggedInUserName,
                RemoveUserName = removeUserName
            };


            // Serializ 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/Like/RemoveLike";


            WebRequest request = WebRequest.Create(route);
            request.Method = "DElETE";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (WebResponse response = request.GetResponse())
            {

                response.Close();
            }
        }


        public List<string> GetLikerUsernames()
        {
            string route = $"{urlAPI}TermProjectAPI/Like/GetLikerUsernames";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<string> usernames = JsonSerializer.Deserialize<List<string>>(jsonData);

            System.Diagnostics.Debug.WriteLine("AAAA: " + usernames[0]);
            System.Diagnostics.Debug.WriteLine("BBBB: " + usernames[1]);
            System.Diagnostics.Debug.WriteLine("count: " + usernames.Count);
            return usernames;


        }


        public List<string> GetLikedUsernames()
        {
            string route = $"{urlAPI}TermProjectAPI/Like/GetLikedUsernames";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<string> usernames = JsonSerializer.Deserialize<List<string>>(jsonData);

            System.Diagnostics.Debug.WriteLine("cccc: " + usernames[0]);
            System.Diagnostics.Debug.WriteLine("ssss: " + usernames[1]);
            System.Diagnostics.Debug.WriteLine("count: " + usernames.Count);
            return usernames;


        }




        public void AddNewMatch(int matchID, string liker, string likedUser)
        {
          
            // obj to hold data
            var requestData = new
            {
                MatchID = matchID,
                Liker = liker,
                LikedUser = likedUser
            };

       

            // Serialize all 3 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/Match/AddNewMatch";


            WebRequest request = WebRequest.Create(route);
            request.Method = "POST";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (WebResponse response = request.GetResponse())
            {

                response.Close();
            }


        }


        public List<Match> GetMatches()
        {
            string route = $"{urlAPI}TermProjectAPI/Match/GetMatches";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<Match> matches = JsonSerializer.Deserialize<List<Match>>(jsonData);

            return matches;
        }



        public List<string> GetMatchUsername1()
        {
            string route = $"{urlAPI}TermProjectAPI/Match/GetMatchUsername1";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<string> usernames = JsonSerializer.Deserialize<List<string>>(jsonData);

            System.Diagnostics.Debug.WriteLine("cccc: " + usernames[0]);
            System.Diagnostics.Debug.WriteLine("ssss: " + usernames[1]);
            System.Diagnostics.Debug.WriteLine("count: " + usernames.Count);
            return usernames;


        }




        public List<string> GetMatchUsername2()
        {
            string route = $"{urlAPI}TermProjectAPI/Match/GetMatchUsername2";

            WebRequest request = WebRequest.Create(route);
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string jsonData = reader.ReadToEnd();

            reader.Close();
            response.Close();

            // Deserialize 
            List<string> usernames = JsonSerializer.Deserialize<List<string>>(jsonData);

            System.Diagnostics.Debug.WriteLine("cccc: " + usernames[0]);
            System.Diagnostics.Debug.WriteLine("ssss: " + usernames[1]);
            System.Diagnostics.Debug.WriteLine("count: " + usernames.Count);
            return usernames;


        }




        public void RemoveMatches(string loggedInUserName, string removeUserName)
        {
            // obj to hold data
            var requestData = new
            {
                LoggedInUserName = loggedInUserName,
                RemoveUserName = removeUserName
            };


            // Serializ 
            string jsonData = JsonSerializer.Serialize(requestData);

            string route = $"{urlAPI}TermProjectAPI/Match/RemoveMatch";


            WebRequest request = WebRequest.Create(route);
            request.Method = "DElETE";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (WebResponse response = request.GetResponse())
            {

                response.Close();
            }
        }




    }
}
