namespace TermProjectAPI
{
    public class UserAccount
    {
        public string userName, password, lastName, email;

        public string firstName {  get; set; }

        public UserAccount(string userName, string password, string firstName, string lastName, string email)
        {

            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;

        }

        public UserAccount() { }    


        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

    
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

    }
}

