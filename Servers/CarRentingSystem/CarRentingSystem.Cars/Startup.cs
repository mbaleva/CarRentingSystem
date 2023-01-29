namespace CarRentingSystem.Cars
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Services.Cars;
    using CarRentingSystem.Cars.Services.Categories;
    using CarRentingSystem.Cars.Services.Dealers;
    using CarRentingSystem.Cars.Services.Manufacturers;
    using CarRentingSystem.Common.Extensions;
    using CarRentingSystem.Common.Services.Users;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDb<ApplicationDbContext>(this.Configuration)
                .AddAppSettings(this.Configuration)
                .AddJwtAuthentication(this.Configuration)
                .AddTransient<ICarsService, CarsService>()
                .AddTransient<IManufacturerService, ManufacturerService>()
                .AddTransient<IDealersService, DealersService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddMessageBroker(this.Configuration)
                .AddHealthChecks(this.Configuration)
                .AddSwagger()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
                => app.MigrateDatabase().AddWebServices(env);
    }
}
