using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.API.DTOs
{
    /// <summary>
    /// Use it at Create Endpoints
    /// or as representing model
    /// </summary>
    public record CategoryWithProductDto : CategoryDto
    {
        public CategoryWithProductDto()
        {
            Products = new Collection<ProductDto>();
        }
        public ICollection<ProductDto> Products { get; set; }
    }
    /// <summary>
    /// Use it in Update Endpoints
    /// Notice:if you use ProductDto instead of ProductUpDto below and use direct EF Update method, it would add new child entity in not existance cases
    /// </summary>
    public record CategoryWithProductUpDto : CategoryUpDto
    {
        public ICollection<ProductUpDto> Products { get; set; }
    }
}