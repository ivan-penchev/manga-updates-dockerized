using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MU.Translators.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Configurations
{
    public class TranslatedTitleConfiguration : IEntityTypeConfiguration<TranslatedTitle>
    {
        public void Configure(EntityTypeBuilder<TranslatedTitle> builder)
        {
            builder.HasKey(x => x.Id);

            builder
               .Property(x => x.Visible)
               .IsRequired()
               .HasDefaultValue(false);
            builder
                 .Property(e => e.DateTranslated)
                 .ValueGeneratedOnAdd()
                 .HasDefaultValueSql("GetDate()");

            builder
                .HasOne(x => x.Title)
                .WithMany(x => x.Translations)
                .HasForeignKey(x => x.TitleId);
            builder
               .HasOne(x => x.TranslatorGroup)
               .WithMany(x => x.TranslatedTitles)
               .HasForeignKey(x => x.TranslatorGroupId);

            builder
               .HasOne(x => x.Translator)
               .WithMany(x => x.Translations)
               .HasForeignKey(x => x.TranslatorId);
        }
    }
}
