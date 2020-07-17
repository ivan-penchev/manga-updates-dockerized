using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MU.Common.Extensions;
using Microsoft.Extensions.Configuration;
using GreenPipes;
using MU.Common.Models;

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
