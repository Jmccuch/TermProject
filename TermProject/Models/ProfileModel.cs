using System;
using System.Collections.Generic;

namespace TermProject.Models
{
        public class ProfileModel
        {
            // Fields
            private AccountModel userAccount;
            private string profileImage, phoneNumber, address, occupation, catOrDog, commitmentType, description;
            private string city, state, accountVisible;
            private int? age;
            private float? height, weight;
            private string restaurant, book, movie, quote;
            private List<string> likes, dislikes;

            // Properties
            public AccountModel UserAccount { get; set; }
            public string ProfileImage { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Occupation { get; set; }
            public string CatOrDog { get; set; }
            public string Description { get; set; }
            public int? Age { get; set; }
            public float? Height { get; set; }
            public float? Weight { get; set; }
            public string Restaurant { get; set; }
            public string Book { get; set; }
            public string Movie { get; set; }
            public string Quote { get; set; }
            public List<string> Likes { get; set; }
            public List<string> Dislikes { get; set; }
            public string CommitmentType { get; set; }
            public string AccountVisible { get; set; }

            // Constructor
            public ProfileModel(
                AccountModel userAccount, string profileImage, string phoneNumber, string address,
                string city, string state, string occupation, string catOrDog, string description,
                int? age, float? height, float? weight, string restaurant, string book, string movie,
                string quote, string commitmentType, List<string> likes, List<string> dislikes,
                string accountVisible)
            {
                UserAccount = userAccount;
                ProfileImage = profileImage;
                PhoneNumber = phoneNumber;
                Address = address;
                City = city;
                State = state;
                Occupation = occupation;
                CatOrDog = catOrDog;
                Description = description;
                Age = age;
                Height = height;
                Weight = weight;
                Restaurant = restaurant;
                Book = book;
                Movie = movie;
                Quote = quote;
                CommitmentType = commitmentType;
                Likes = likes;
                Dislikes = dislikes;
                AccountVisible = accountVisible;
            }

        public ProfileModel() { 
        
        
        }

        }
    }
