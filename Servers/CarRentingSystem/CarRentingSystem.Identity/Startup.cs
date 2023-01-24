namespace CarRentingSystem.Identity
{
    using CarRentingSystem.Identity.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CarRentingSystem.Identity.Data.Models;
    using CarRentingSystem.Identity.Services.Users;
    using CarRentingSystem.Identity.Services.Jwt;
    using CarRentingSystem.Common.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDb<ApplicationDbContext>(this.Configuration)
                .AddHealthChecks(this.Configuration)
                .AddAppSettings(this.Configuration)
                .AddJwtAuthentication(this.Configuration);

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IJwtService, JwtService>();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddWebServices(env);
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<DbContext>();

            db.Database.Migrate();
        }
    }
}
