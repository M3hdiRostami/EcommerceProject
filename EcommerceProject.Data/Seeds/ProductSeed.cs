using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Data.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {

        private int[] _CurrenciesId { get; }
        private int[] _CategoriesId { get; }
        public ProductSeed(int[] categoriesId, int[] currenciesId)
        {
            _CategoriesId = categoriesId;
            _CurrenciesId = currenciesId;
        }

  

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "x T-shirt", Price = 12.50m, Stock = 100, CategoryId = _CategoriesId[0] ,CurrencyId= _CurrenciesId[2] },
                new Product { Id = 2, Name = "y T-shirt", Price = 40.50m, Stock = 200, CategoryId = _CategoriesId[0], CurrencyId = _CurrenciesId[0] },
                new Product { Id = 3, Name = "x Shirt", Price = 500m, Stock = 300, CategoryId = _CategoriesId[0], CurrencyId = _CurrenciesId[1] },
                new Product { Id = 4, Name = "x Boot", Price = 12.50m, Stock = 100, CategoryId = _CategoriesId[1], CurrencyId = _CurrenciesId[0] },
                new Product { Id = 5, Name = "y Sneakers", Price = 12.50m, Stock = 100, CategoryId = _CategoriesId[1], CurrencyId = _CurrenciesId[2] },
                new Product { Id = 6, Name = "y Snow Boot", Price = 12.50m, Stock = 100, CategoryId = _CategoriesId[1], CurrencyId = _CurrenciesId[1] }
                );
        }
    }
}