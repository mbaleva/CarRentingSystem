namespace CarRentingSystem.Analyses
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CarRentingSystem.Common.Extensions;
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Analyses.Consumers;
    using CarRentingSystem.Analyses.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => services
                .AddDb<ApplicationDbContext>(this.Configuration)
                .AddJwtAuthentication(this.Configuration)
                .AddRequestPipelineExtensions()
                .AddHealthChecks(this.Configuration)
                .AddAuthorization()
                .AddMessageBroker(this.Configuration, true,
                    typeof(CarCreatedConsumer),
                    typeof(CarViewedConsumer),
                    typeof(DealerCreatedConsumer))
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddSwagger()
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.MigrateDatabase().AddWebServices(env);
    }
}
