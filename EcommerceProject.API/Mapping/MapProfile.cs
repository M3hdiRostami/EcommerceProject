using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceProject.API.DTOs;
using EcommerceProject.Core.Models;

namespace EcommerceProject.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //todo:there is no diffrece between UpDTO and  DTO types except validatiions rules,we dont map UpDTO here and ,
            //if you made a structural modify on UpDTO dont forget to map those here.
            CreateMap<Category, CategoryDto>().ReverseMap();
          
            CreateMap<CurrencyUnit, CurrencyDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            
            CreateMap<Category, CategoryWithProductDto>().ReverseMap(); ;
           
            CreateMap<Product, ProductWithCategoryDto>().ReverseMap();

           

        }
    }
}