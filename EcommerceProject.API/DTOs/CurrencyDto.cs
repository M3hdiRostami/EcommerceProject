using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.API.DTOs
{

    /// <summary>
    /// Use it at Create Endpoints
    /// or as representing model
    /// </summary>
    public record CurrencyDto
    {
        [Range(0, 0, ErrorMessage = "{0} value is in invalid range")]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Symbol { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public string Code { get; set; }
    }
    /// <summary>
    /// Use it at Update Endpoints
    /// </summary>
    public record CurrencyUpDto : CurrencyDto
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        public override int Id { get; set; }

    }
}