using CarRentingSystem.UpdateService.Hubs;
using CarRentingSystem.UpdateService.Infrastructure;
using CarRentingSystem.Common.Messages;
using CarRentingSystem.UpdateService.Notifications;
using CarRentingSystem.Common.Extensions;

namespace CarRentingSystem.UpdateService;

public class Startup
{
    public Startup(IConfiguration configuration)
        => this.Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
        => services
            .AddCors()
            .AddJwtAuthentication(
                this.Configuration,
                JwtConfig.BearerEvents)
            .AddMessageBroker(
                this.Configuration,
                true,
                typeof(CarCreatedNotification))
            .AddSignalR();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseRouting()
            .UseCors(options => options
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints
            .MapHub<NotificationsHub>("/notifications"));
    }
}
