using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(50,MinimumLength =5)]

        public string Password { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; }

        public string City { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public DateTime DateOfBirth { get; set; }
        

      
        [Required]

        public string KnownAs { get; set; }

    }
}
