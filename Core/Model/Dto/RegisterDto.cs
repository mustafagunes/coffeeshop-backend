using System.ComponentModel.DataAnnotations;

namespace Core.Dto
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        #nullable enable
        public string? ApnsToken { get; set; }
        
        public string? FcmToken { get; set; }
        #nullable disable
    }
}