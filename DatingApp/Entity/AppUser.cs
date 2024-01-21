using DatingApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entity
{
    public class AppUser
    {
        public string UserName { get; set; }

        public int Id { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] PasswordSalt  { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserLike> LikedByUsers { get; set; }// this the list of users that like the currently loged in user 
        public ICollection<UserLike> LikedUsers { get; set; } // this the list of users that the currently loged in  user has liked  



    }
}
