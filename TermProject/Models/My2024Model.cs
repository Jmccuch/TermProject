namespace TermProject.Models
{
    public class My2024Model
    {
        private string username;
        private int finalScore;

        public My2024Model(string username, int finalScore) { 
        
            this.username = username;
            this.finalScore = finalScore;
        }

        public string Username { get { return username; } set { username = value; } }
        public int FinalScore { get { return finalScore; } set { finalScore = value; } }
    }
}
