namespace DatingApp.Entity
{
    public class AppUser
    {
        public string? UserName { get; set; }
       // public string? Password { get; set; }
        public int id { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] PasswordSalt  { get; set; }



    }
}
