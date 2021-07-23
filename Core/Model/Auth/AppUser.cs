using Microsoft.AspNetCore.Identity;

namespace Core.Model.Auth
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        
#nullable enable
        public string? ApnsToken { get; set; }
        
        public string? FcmToken { get; set; }
#nullable disable
    }
}