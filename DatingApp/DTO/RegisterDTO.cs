using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [StringLength(50,MinimumLength =5)]

        public string Password { get; set; } = string.Empty;

    }
}
