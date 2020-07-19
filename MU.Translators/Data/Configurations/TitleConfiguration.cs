using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Translators.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MU.Translators.Data.Configurations
{
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
              .Property(x => x.TitleId)
              .IsRequired();
            builder
             .Property(x => x.PublishedBy)
             .IsRequired();
        }
    }
}
