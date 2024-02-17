using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CarRentingSystem.Common.Middlewares;
public class SeleniumDetectorMiddleware : IMiddleware
{
    public SeleniumDetectorMiddleware()
    {
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string userAgent = context.Request.Headers["User-Agent"].ToString();

        if (IsSeleniumRequest(userAgent))
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Not Found");
            return;
        }

        await next(context);
    }

    private bool IsSeleniumRequest(string userAgent)
    {
        return userAgent.Contains("HeadlessChrome");
    }
}

