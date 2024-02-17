namespace CarRentingSystem.Common.Services.RateLimiting;

public interface IRateLimiter
{
    bool AllowRequest();
}
