using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceProject.Core.Models
{
    public class Product 
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public int Stock { get; set; }
   
        public bool IsDeleted { get; set; }
        public string Serialnumber { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual CurrencyUnit Currency { get; set; }
    }
}