using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceProject.Core.Models;
using EcommerceProject.Core.Repositories;
using EcommerceProject.Core.UnitOfWorks;
using EcommerceProject.Data.Repositories;

namespace EcommerceProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _CategoryRepository;
        private Repository<CurrencyUnit> _CurrenciesRepository;

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public ICategoryRepository Categories => _CategoryRepository = _CategoryRepository ?? new CategoryRepository(_context);
        public IRepository<CurrencyUnit> Currencies => _CurrenciesRepository = _CurrenciesRepository ?? new Repository<CurrencyUnit>(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}