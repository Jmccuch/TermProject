namespace TermProjectAPI
{
    public class DateInfo
    {
        public string userName1 { get; set; }
        public string userName2 { get; set; }

        public string description { get; set; }

        public DateTime dateAndTime0 { get; set; }

        public int dateID { get; set; }



        public DateInfo(string userName1, string userName2, string description, DateTime dateAndTime0) { 

            this.userName1 = userName1;
            this.userName2 = userName2;
            this.description = description;
            this.dateAndTime0 = dateAndTime0;
        
        
        }

        public DateInfo() { 
        
        
        }


    }
}
