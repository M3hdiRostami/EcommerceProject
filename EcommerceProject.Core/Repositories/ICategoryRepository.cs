using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);
    }
}