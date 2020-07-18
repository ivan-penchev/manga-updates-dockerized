using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MU.Common.Infrastructure;
using MU.Common.Services;
using MU.Publishers.Data;
using MU.Publishers.Services.Genres;
using MU.Publishers.Services.MangaPublishers;
using MU.Publishers.Services.Mangas;

namespace MU.Publishers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddWebMicroService<PublishersDbContext>(this.Configuration)
                .AddTransient<IDataSeeder, PublisherDbSeeder>()
                .AddTransient<IGenreService, GenreService>()
                .AddTransient<IMangaPublisherService, MangaPublisherService>()
                .AddTransient<IMangaService, MangaService>()
                .AddMessaging(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseWebMicroService(env)
                .Initialize();
        }
    }
}
