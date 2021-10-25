using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceProject.Core.Models;

namespace EcommerceProject.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);

        //  bool ControlInnerBarcode(Product product);
    }
}