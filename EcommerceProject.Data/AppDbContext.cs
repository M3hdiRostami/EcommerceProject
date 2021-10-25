using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EcommerceProject.Core.Models;
using EcommerceProject.Data.Configurations;
using EcommerceProject.Data.Seeds;

namespace EcommerceProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CurrencyUnit> Currencies { get; set; }

        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new CurrencySeed(new int[] { 1, 2 ,3 ,4}));
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }, new int[] { 1, 2, 3, 4 }));
           

            
        }
    }
}