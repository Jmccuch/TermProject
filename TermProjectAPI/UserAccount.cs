namespace TermProjectAPI
{
    public class UserAccount
    {

        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName {  get; set; }
        public string email { get; set; }

        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public UserAccount(string userName, string password, string firstName, string lastName, string email, string answer1, string answer2, string answer3)
        {

            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;

            this.answer1 = answer1;
            this.answer2 = answer2;
            this.answer3 = answer3;

        }

        public UserAccount() { }    



    }
}

