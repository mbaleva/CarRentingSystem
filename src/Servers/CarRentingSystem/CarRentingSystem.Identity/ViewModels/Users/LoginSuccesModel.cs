namespace CarRentingSystem.Identity.ViewModels.Users
{
    public class LoginSuccesModel
    {
        public LoginSuccesModel(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
