using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Data.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>

    {
        private readonly int[] _ids;

        public CategorySeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new  { Id = _ids[0], Name = "Shirts", },
                new  { Id = _ids[1], Name = "Shoes" });
        }
    }
}