﻿namespace CarRentingSystem.Common.Extensions
{
    using CarRentingSystem.Common.Filters;
    using CarRentingSystem.Common.Middlewares;
    using CarRentingSystem.Common.Services;
    using CarRentingSystem.Common.Services.RateLimiting;
    using CarRentingSystem.Common.Settings;
    using GreenPipes;
    using Hangfire;
    using MassTransit;
    using MassTransit.RabbitMqTransport;
    using MassTransit.RabbitMqTransport.Integration;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = false)
        {
            var healthChecks = services.AddHealthChecks();

            if (databaseHealthChecks)
            {
                healthChecks
                    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }

            if (messagingHealthChecks)
            {
                var settings = GetMessageBrokerSettings(configuration);

                var messageQueueConnectionString =
                    $"amqp://{settings.Username}:{settings.Password}@{settings.Host}:5672/";

                healthChecks
                    .AddRabbitMQ(rabbitConnectionString: messageQueueConnectionString);
            }

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            return services;
        }
        public static IServiceCollection AddDb<TDbContext>(
            this IServiceCollection services,
            IConfiguration config) where TDbContext : DbContext
                => services.AddScoped<DbContext, TDbContext>()
                        .AddDbContext<TDbContext>(dbOptions =>
                                dbOptions.UseSqlServer(config.GetConnectionString("DefaultConnection")))
                        .AddDatabaseDeveloperPageExceptionFilter();


        public static IServiceCollection AddAppSettings(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
               .Configure<ApplicationSettings>(
                   configuration.GetSection(nameof(ApplicationSettings)));
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration config,
            JwtBearerEvents events = null)
        {
            services
                    .AddHttpContextAccessor();

            var secret = config
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.JwtSecret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    if (events is not null)
                    {
                        bearer.Events = events;
                    }

                });

            return services;
        }
        public static IServiceCollection AddMessageBroker(
            this IServiceCollection services,
            IConfiguration configuration,
            bool usePolling = true,
            params Type[] consumers)
        {

            var settings = GetMessageBrokerSettings(configuration);

            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host("amqp://" + settings.Host, (config) => 
                        {
                            config.Username(settings.Username);
                            config.Password(settings.Password);
                        });
                        Console.WriteLine(settings.Host);
                        Console.WriteLine(settings.Username);
                        Console.WriteLine(settings.Password);
                        consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, consumer);
                        }));
                    }));
                })
                .AddMassTransitHostedService();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services
                .AddHangfire(x => x
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<MessagesHostedService>();

            return services;
        }
        private static MessageBrokerSettings GetMessageBrokerSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MessageBrokerSettings));

            return new MessageBrokerSettings(
                settings.GetValue<string>(nameof(MessageBrokerSettings.Host)),
                settings.GetValue<string>(nameof(MessageBrokerSettings.Username)),
                settings.GetValue<string>(nameof(MessageBrokerSettings.Password)));
        }

        public static IServiceCollection AddRequestPipelineExtensions(this IServiceCollection services) 
        {
            services.AddScoped<RateLimiterMiddleware>();
            services.AddMemoryCache();

            services.AddScoped<SeleniumDetectorMiddleware>();
            services.AddSingleton<IRateLimiter, RateLimiter>((sp) => 
            {
                return new RateLimiter(1000, TimeSpan.FromMinutes(1));
            });
            services.AddScoped<CacheActionAttribute>();

            services.AddControllers(options => 
            {
                options.Filters.AddService(typeof(CacheActionAttribute));
            });

            return services;
        }
    }
}
