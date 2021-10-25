using EcommerceProject.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.API.DTOs
{  /// <summary>
   /// Use it at Create Endpoints
   /// or as representing model
   /// </summary>
    public record ProductDto
    {
        public ProductDto()
        {
                
        }
        [Range(0, 0, ErrorMessage = "{0} value is in invalid range")]
        public virtual int Id { get; set; }

        [MaxLength(200, ErrorMessage = "{0} MaxLength is 200")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [MaxLength(50,ErrorMessage = "{0} MaxLength is 50")]
        public string Serialnumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} value must be greater than 1")]
        public int Stock { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "{0} value must be greater than 1")]
        public decimal Price { get; set; }

        public int CurrencyID  { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }
    }
    /// <summary>
    /// Use it at Update Endpoints
    /// </summary>
    public record ProductUpDto : ProductDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        public override int Id { get; set; }
    }
}