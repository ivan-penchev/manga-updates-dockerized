using Microsoft.EntityFrameworkCore;
using MU.Translators.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MU.Translators.Data
{
    public class TranslatorDbContext : DbContext
    {
        public TranslatorDbContext(DbContextOptions<TranslatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Title> Titles { get; set; }

        public DbSet<Translator> Translators { get; set; }
        public DbSet<TranslatorGroup> TranslatorGroups { get; set; }
        public DbSet<TranslatedTitle> Translations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
