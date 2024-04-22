namespace TermProjectAPI
{
    public class User
    {

        public UserAccount userAccount {get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        
            public string occupation { get; set; }

            public string catOrDog { get; set; }

            public string commitmentType {  get; set; }


        public string description {  get; set; }
            public string profileImage { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string accountVisible { get; set; }
            public int age { get; set; }
            public float height, weight;
            public string restaurant, book, movie, quote;
            public List<string> likes, dislikes;



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



            public float Height
            {
                get { return height; }
                set { height = value; }
            }

            public float Weight
            {
                get { return weight; }
                set { weight = value; }
            }

            public string Restaurant
            {
                get { return restaurant; }
                set { restaurant = value; }
            }

            public string Book
            {
                get { return book; }
                set { book = value; }
            }

            public string Movie
            {
                get { return movie; }
                set { movie = value; }
            }

            public string Quote
            {
                get { return quote; }
                set { quote = value; }
            }

            public List<string> Likes
            {
                get { return likes; }
                set { likes = value; }
            }

            public List<string> Dislikes
            {
                get { return dislikes; }
                set { dislikes = value; }
            }





        }
    }





