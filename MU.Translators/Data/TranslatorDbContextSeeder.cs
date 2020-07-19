using Microsoft.EntityFrameworkCore.Internal;
using MU.Common.Services;
using MU.Translators.Data.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data
{
    public class TranslatorDbContextSeeder : IDataSeeder
    {
        private readonly TranslatorDbContext translatorDbContext;

        public TranslatorDbContextSeeder(TranslatorDbContext translatorDbContext)
        {
            this.translatorDbContext = translatorDbContext;
        }

        public void SeedData()
        {
            if (this.translatorDbContext.Titles.Any()
                || this.translatorDbContext.TranslatorGroups.Any())
                return;
            Task
                .Run(async () =>
                {

                    var allTitle = GetTitles();
                    var allTranslatorGroups = GetTranslatorGroups();

                    this.translatorDbContext.Titles.AddRange(allTitle);
                    this.translatorDbContext.TranslatorGroups.AddRange(allTranslatorGroups);
                    await this.translatorDbContext.SaveChangesAsync();

                })
                .GetAwaiter()
                .GetResult();

        }

        public List<Title> GetTitles()
        {
            return new List<Title>
            {
                new Title{ TitleId = 1, PublishedBy="Shueisha"},
                new Title{ TitleId = 2, PublishedBy="Kodansha"},
                new Title{ TitleId = 3, PublishedBy="Shueisha"},
            };
        }

        public List<TranslatorGroup> GetTranslatorGroups()
        {
            return new List<TranslatorGroup>
            {
                new TranslatorGroup{ Name="Binktopia"},
                new TranslatorGroup{ Name="MangaStream"},
                new TranslatorGroup{ Name="Franky-House"},
            };
        }
    }
}
