using CarRentingSystem.Common.Services.RateLimiting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace CarRentingSystem.Common.Middlewares;

public class RateLimiterMiddleware : IMiddleware
{
    public RateLimiterMiddleware()
    {

    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var rateLimiter = (IRateLimiter)context.RequestServices.GetService(typeof(IRateLimiter));

        if (!rateLimiter.AllowRequest())
        {
            context.Response.StatusCode = 429;
            await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
            return;
        }

        await next(context);
    }
}
