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
    public record CategoryDto
    {
        [Range(0, 0, ErrorMessage = "{0} value is in invalid range")]
        public virtual int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
    /// <summary>
    /// Use it at Update Endpoints
    /// </summary>
    public record CategoryUpDto : CategoryDto
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        public override int Id { get; set; }

    }
}