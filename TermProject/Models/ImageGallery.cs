namespace TermProject.Models
{
    public class ImageGallery
    {
        private string username;
        private string picture1;
        private string picture2;   
        private string picture3;

        public ImageGallery(string username, string picture1, string picture2, string picture3)
        {
            this.username = username;
            this.picture1 = picture1;
            this.picture2 = picture2;
            this.picture3 = picture3;
        }

        public string Username { get { return username; } set { username = value; } }
        public string Picture1 { get {  return picture1; } set {  picture1 = value; } }
        public string Picture2 { get {  return picture2; } set {  picture2 = value; } }
        public string Picture3 { get {  return picture3; } set {  picture3 = value; } }
    }
}
