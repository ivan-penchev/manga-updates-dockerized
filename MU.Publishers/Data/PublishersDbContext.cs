using Microsoft.EntityFrameworkCore;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MU.Publishers.Data
{
    public class PublishersDbContext : DbContext
    {
        public PublishersDbContext(DbContextOptions<PublishersDbContext> options)
            : base(options)
        {
        }
        public DbSet<Manga> Mangas { get; set; }

        public DbSet<MangaPublisher> MangaPublishers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
