namespace TermProjectAPI
{
    public class UserAccount
    {

        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName {  get; set; }
        public string email { get; set; }

        public UserAccount(string userName, string password, string firstName, string lastName, string email)
        {

            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;

        }

        public UserAccount() { }    



    }
}

