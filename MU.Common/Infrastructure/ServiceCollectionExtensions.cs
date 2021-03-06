﻿using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MU.Common.Extensions;
using Microsoft.Extensions.Configuration;
using GreenPipes;
using MU.Common.Models;
using MU.Common.Services.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace MU.Common.Infrastructure
{
        public static class ServiceCollectionExtensions
        {
            public static IServiceCollection AddWebMicroService<TDbContext>(
                this IServiceCollection services,
                IConfiguration configuration)
                where TDbContext : DbContext
            {
            services
                .AddDatabase<TDbContext>(configuration)
                .AddTokenApplicationSettings(configuration)
                .AddTokenAuthentication(configuration)
                .AddHealth(configuration)
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddAutoMapperProfile(Assembly.GetCallingAssembly())
                .AddControllers();

                return services;
            }

            public static IServiceCollection AddDatabase<TDbContext>(
               this IServiceCollection services,
               IConfiguration configuration)
               where TDbContext : DbContext
               => services
                   .AddScoped<DbContext, TDbContext>()
                   .AddDbContext<TDbContext>(options => options
                       .UseSqlServer(
                           configuration.GetDefaultConnectionString(),
                           sqlOptions => sqlOptions
                               .EnableRetryOnFailure(
                                   maxRetryCount: 10,
                                   maxRetryDelay: TimeSpan.FromSeconds(30),
                                   errorNumbersToAdd: null)));

        public static IServiceCollection AddTokenApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<TokenSettings>(
                    configuration.GetSection(nameof(TokenSettings)),
                    config => config.BindNonPublicProperties = true);

        public static IServiceCollection AddHealth(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();

            healthChecks
                .AddSqlServer(configuration.GetDefaultConnectionString());
            // 	Multiple segments in path of AMQP URI: /, /, rabbitmq:guest@guest/

            var rabbitConnectionString = "amqp://"+$"{configuration["RabbitMq:host"]}:{configuration["RabbitMq:user"]}@{configuration["RabbitMq:pass"]}"+"/";
            healthChecks
                .AddRabbitMQ(rabbitConnectionString: rabbitConnectionString);

            return services;
        }


        public static IServiceCollection AddTokenAuthentication(
           this IServiceCollection services,
           IConfiguration configuration,
           JwtBearerEvents events = null)
        {
            var secret = configuration
                .GetSection(nameof(TokenSettings))
                .GetValue<string>(nameof(TokenSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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

                    if (events != null)
                    {
                        bearer.Events = events;
                    }
                });

            return services;
        }

        public static IServiceCollection AddAutoMapperProfile(
                this IServiceCollection services,
                Assembly assembly)
                => services
                    .AddAutoMapper(
                        (_, config) => config
                            .AddProfile(new MappingProfile(assembly)),
                        Array.Empty<Assembly>());

            public static IServiceCollection AddMessaging(
                this IServiceCollection services,
                IConfiguration configuration,
                params Type[] consumers)
            {
                services
                    .AddMassTransit(mt =>
                    {
                        consumers.ForEach(consumer => mt.AddConsumer(consumer));

                        mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                        {
                            rmq.Host(configuration["RabbitMq:host"], host =>
                            {
                                host.Username(configuration["RabbitMq:user"]);
                                host.Password(configuration["RabbitMq:pass"]);
                            });

                            rmq.UseHealthCheck(context);


                            consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                            {
                                endpoint.PrefetchCount = 6;
                                endpoint.UseMessageRetry(retry => retry.Interval(10, 1000));

                                endpoint.ConfigureConsumer(context, consumer);
                            }));
                        }));
                    })
                    .AddMassTransitHostedService();
                return services;
            }
        }
}
