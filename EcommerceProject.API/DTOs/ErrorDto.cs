using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EcommerceProject.API.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }
        
        
        public  bool HasError() => Errors.Count > 0;
    }
}