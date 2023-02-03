namespace CarRentingSystem.Identity.Services.Jwt
{
    using CarRentingSystem.Identity.Data.Models;
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
