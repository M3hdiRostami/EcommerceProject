using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Data.Seeds
{
    internal class CurrencySeed : IEntityTypeConfiguration<CurrencyUnit>

    {
        private readonly int[] _ids;

        public CurrencySeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<CurrencyUnit> builder)
        {
            builder.HasData(
                new { Id = _ids[0], Name = "US dollar" ,Code="USD", Symbol="$" },
                new { Id = _ids[1], Name = "Pound", Code= "GBP", Symbol= "£" },
                new { Id = _ids[2], Name = "Japanese yen", Code= "JPY", Symbol= "¥" },
                new { Id = _ids[3], Name = "Euro", Code = "EUR", Symbol = "€" });
        }
    }
}