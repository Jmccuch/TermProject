namespace TermProjectAPI
{
    public class Like
    {

        public int LikeNumber { get; set; }

        public string UserName { get; set; }

        public string LikedUserName { get; set; }



        public Like(int LikeNumber, string UserName, string LikedUserName )
        {
            this.LikeNumber = LikeNumber;
            this.UserName = UserName;
            this.LikedUserName = LikedUserName;


        }


        public Like()
        {


        }



    }

}
