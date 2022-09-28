using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; } = string.Empty;

    }
}
