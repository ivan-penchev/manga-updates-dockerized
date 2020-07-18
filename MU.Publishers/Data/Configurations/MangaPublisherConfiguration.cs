using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Data.Configurations
{
    public class MangaPublisherConfiguration : IEntityTypeConfiguration<MangaPublisher>
    {
        public void Configure(EntityTypeBuilder<MangaPublisher> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(d => d.SiteUrl)
                .IsRequired()
                .HasMaxLength(250);

        }
    }
}
