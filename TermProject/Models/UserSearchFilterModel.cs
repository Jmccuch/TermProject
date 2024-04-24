namespace TermProject.Models
{
    public class UserSearchFilterModel
    {
        public string CatOrDog { get; set; }
        public string State { get; set; }
        public string CommitmentType { get; set; }
        public int HighAge { get; set; }

        public int LowAge { get; set; }
    }
}
