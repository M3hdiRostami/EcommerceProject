using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Data.Configurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyUnit>
    {
        public void Configure(EntityTypeBuilder<CurrencyUnit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(4);
            builder.Property(x => x.Symbol).IsRequired().HasMaxLength(1);

            builder.ToTable("Currencies");

        }
    }
}