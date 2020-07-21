using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MU.Admin.Infrastructure;
using MU.Admin.Services;
using MU.Common.Infrastructure;
using MU.Common.Services.Identity;
using Refit;

namespace MU.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                 .GetSection(nameof(ServiceEndpoints))
                 .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
              .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
              .AddTokenAuthentication(this.Configuration)
              .AddScoped<ICurrentTokenService, CurrentTokenService>()
              .AddTransient<JwtCookieAuthenticationMiddleware>()
              .AddControllersWithViews(options => options
                  .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
               .AddRefitClient<IIdentityService>()
               .WithConfiguration(serviceEndpoints.Identity);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
               .UseStaticFiles()
               .UseRouting()
               .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
               .UseJwtCookieAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints
                   .MapDefaultControllerRoute());
        }
    }
}
