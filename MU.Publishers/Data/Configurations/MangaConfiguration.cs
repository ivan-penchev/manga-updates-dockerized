using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MU.Publishers.Data.Configurations
{
    public class MangaConfiguration : IEntityTypeConfiguration<Manga>
    {
        public void Configure(EntityTypeBuilder<Manga> builder)
        {
            builder
               .HasKey(c => c.Id);

            builder
                .Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .Property(e => e.PostDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(e => e.LastUpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GetDate()");

            builder
                .Property(e => e.Status)
                .IsRequired();

            builder
               .HasOne(c => c.Publisher)
               .WithMany(m => m.Mangas)
               .HasForeignKey(c => c.PublisherId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Genre)
                .WithMany(c => c.Mangas)
                .HasForeignKey(c => c.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
