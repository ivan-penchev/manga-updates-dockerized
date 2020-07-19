using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Translators.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Configurations
{
    public class TranslatorConfiguration : IEntityTypeConfiguration<Translator>
    {
        public void Configure(EntityTypeBuilder<Translator> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.UserId)
                .IsRequired();
            builder
                .Property(x => x.ApprovedByAdmin)
                .IsRequired()
                .HasDefaultValue(false);
            builder
                 .Property(e => e.TranslatorRegistered)
                 .ValueGeneratedOnAdd()
                 .HasDefaultValueSql("GetDate()");

            builder
              .HasOne(c => c.TranslatorGroup)
              .WithMany(m => m.Translators)
              .HasForeignKey(c => c.TranslatorGroupId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
