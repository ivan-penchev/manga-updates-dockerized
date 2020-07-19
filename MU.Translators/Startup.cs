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
using Microsoft.Extensions.Logging;
using MU.Common.Infrastructure;
using MU.Common.Services;
using MU.Translators.Data;
using MU.Translators.Messages;
using MU.Translators.Services;

namespace MU.Translators
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
                .AddWebMicroService<TranslatorDbContext>(this.Configuration)
                .AddMessaging(this.Configuration, 
                    typeof(MangaCreatedConsumer), 
                    typeof(MangaUpdatedConsumer), 
                    typeof(MangaDeletedConsumer)
                )
                .AddTransient<IDataSeeder, TranslatorDbContextSeeder>()
                .AddTransient<ITitleService, TitleService>()
                .AddTransient<ITranslatorGroupService, TranslatorGroupService>()
                .AddTransient<ITranslatorService, TranslatorService>();
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
