namespace CarRentingSystem.Identity.Services.Users
{
    using System.Threading.Tasks;
    using CarRentingSystem.Identity.ViewModels.Users;
    public interface IUsersService
    {
        Task<LoginSuccesModel> Login(LoginInputModel model);

        Task Register(RegisterInputModel model);
    }
}
