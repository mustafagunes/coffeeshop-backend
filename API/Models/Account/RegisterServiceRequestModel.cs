namespace CoffeeShop.API.Models.Account
{
    public class RegisterServiceRequestModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ApnsToken { get; set; }
        public string FcmToken { get; set; }
    }
}