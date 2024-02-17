namespace CarRentingSystem.Common.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System.Runtime.CompilerServices;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using HealthChecks.UI.Client;
    using CarRentingSystem.Common.Middlewares;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddWebServices(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseStatusCodePages();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("swagger/v1/swagger.json", "MyAPI V1");
                    c.RoutePrefix = "";
                });
            }

            app
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<SeleniumDetectorMiddleware>()
                .UseMiddleware<RateLimiterMiddleware>()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "CarByIdRoute",
                        pattern: "Cars/{controller=ById}/{id}/{name}",
                        defaults: new { action = "ById" },
                        constraints: new { id = @"\d+" });

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

            app.UseHealthChecks("/health", new HealthCheckOptions 
            {
                Predicate = check => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app) 
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<DbContext>();

            db.Database.Migrate();
            return app;
        }
    }
}
