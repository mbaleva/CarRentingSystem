namespace CarRentingSystem.Common.Services.Users
{
    using System;
    public interface ICurrentUserService
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; }
    }
}
