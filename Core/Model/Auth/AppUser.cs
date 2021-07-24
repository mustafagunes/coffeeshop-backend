using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Model.Auth
{
    public class AppUser : IdentityUser
    {
        [MaxLength(70)]
        public string FullName { get; set; }
        
#nullable enable
        public string? ApnsToken { get; set; }
        
        public string? FcmToken { get; set; }
#nullable disable
    }
}