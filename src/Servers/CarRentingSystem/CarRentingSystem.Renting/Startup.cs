namespace CarRentingSystem.Renting
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CarRentingSystem.Common.Extensions;
    using CarRentingSystem.Renting.Data;
    using CarRentingSystem.Renting.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => services
                .AddDb<ApplicationDbContext>(this.Configuration)
                .AddJwtAuthentication(this.Configuration)
                .AddHealthChecks(this.Configuration)
                .AddAuthorization()
                .AddMessageBroker(this.Configuration)
                .AddTransient<IAppointmentsService, AppointmentsService>()
                .AddSwagger()
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.MigrateDatabase().AddWebServices(env);
    }
}
