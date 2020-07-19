using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Translators.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Configurations
{
    public class TranslatorGroupConfiguration : IEntityTypeConfiguration<TranslatorGroup>
    {
        public void Configure(EntityTypeBuilder<TranslatorGroup> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Name)
                .IsRequired();

        }
    }
}
