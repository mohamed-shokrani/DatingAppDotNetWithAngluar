namespace DatingApp.Entity
{
    public class UserLike
    {
        public int SourceUserId { get; set; }
        public AppUser SourceUser { get; set; } //liked by other users 

        public int LikedUserId { get; set; }

        public AppUser LikedUser { get; set; }// the other side 


        

    }
}
