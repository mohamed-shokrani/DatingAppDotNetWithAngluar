using DatingApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entity
{
    public class AppUser
    {
        public string UserName { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        //public string Email { get; set; }
       // public string? Password { get; set; }
        public int Id { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] PasswordSalt  { get; set; }
        //public string ImageUrl { get; set; }
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

        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();

        //}


    }
}
