namespace TermProjectAPI
{
    public class User
    {

        public UserAccount userAccount { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }

        public string occupation { get; set; }
        public string catOrDog { get; set; }
        public string commitmentType { get; set; }


        public string description { get; set; }
        public string profileImage { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string accountVisible { get; set; }
        public int age { get; set; }

        public float height { get; set; }
        public float weight { get; set; }



        public string restaurant { get; set; }
        public string book { get; set; }

        public string movie { get; set;}

        public string quote { get; set; }
    
     
        public List<string> likes { get; set; }
        public List<string> dislikes { get; set; }

        public string picture1 { get; set; }
        public string picture2 { get; set; }
        public string picture3 { get; set; }



        public User(UserAccount userAccount, string profileImage, string phoneNumber, string address, string city, string state, string occupation, string catOrDog, string description
                , int age, float height, float weight, string restaurant, string book, string movie, string quote, string commitmentType, List<string> likes, List<string> dislikes, string accountVisible)
            {

                this.userAccount = userAccount;
                this.profileImage = profileImage;
                this.phoneNumber = phoneNumber;
                this.address = address;
                this.city = city;
                this.state = state;
                this.occupation = occupation;
                this.catOrDog = catOrDog;
                this.description = description;
                this.age = age;
                this.height = height;
                this.weight = weight;
                this.restaurant = restaurant;
                this.book = book;
                this.movie = movie;
                this.quote = quote;
                this.commitmentType = commitmentType;
                this.likes = likes;
                this.dislikes = dislikes;
                this.accountVisible = accountVisible;


            }


        public User() { 
        
        
        }



        }

}





