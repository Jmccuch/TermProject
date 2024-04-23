namespace TermProject.Models
{
    public class LoginModel
    {

        private string? userName;
        private string? password;



        public string? UserName
        {

            get { return userName; }

            set { userName = value; }
        }

        public string? Password
        {
            get { return password; }

            set { password = value; }
        }

    }
}
