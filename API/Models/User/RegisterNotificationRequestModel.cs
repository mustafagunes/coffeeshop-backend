namespace CoffeeShop.API.Models.User
{
    public class RegisterNotificationRequestModel
    {
        public int UserId { get; set; }
        public string FcmToken { get; set; }
        public string ApnsToken { get; set; }
    }
}