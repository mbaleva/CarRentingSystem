namespace CarRentingSystem.Common.Services.Users
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Security.Claims;

    public class CurrentUserService
    {
        private readonly ClaimsPrincipal userClaims;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            this.userClaims = httpContext.HttpContext?.User;

            if (userClaims == null)
            {
                throw new Exception("Unauthenticated");
            }
            this.UserId = this.userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string UserId { get; set; }
        public bool IsAdmin => this.userClaims.IsInRole("Administrator");
    }
}
