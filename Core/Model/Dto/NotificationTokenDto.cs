#nullable enable
namespace Core.Dto
{
    public class NotificationTokenDto
    {
        public string? ApnsToken { get; set; }
        
        public string? FcmToken { get; set; }
    }
}
#nullable disable