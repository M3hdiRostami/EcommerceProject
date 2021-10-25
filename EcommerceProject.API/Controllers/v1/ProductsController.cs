using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceProject.API.DTOs;
using EcommerceProject.API.Filters;
using EcommerceProject.Core.Models;
using EcommerceProject.Core.Services;
using EcommerceProject.Service.Services;

namespace EcommerceProject.API.Controllers
{

    public class ProductsController : APIControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, IMapper mapper) : base(mapper)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }


        [HttpGet("{id}",Name = nameof(GetById))]
        [TypeFilter(typeof(NotFoundFilter<Product, ProductDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(product));
        }


        [TypeFilter(typeof(NotFoundFilter<Product, ProductDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }


        /// <summary>
        /// If ProductWithCategoryDto is used and CategoryDto(child) of ProductWithCategoryDto has not id, EF adds
        /// child AlongSide parent entity
        /// </summary>
        [HttpPost]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryUpDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson, "categoryId" })]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var newproduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            
            return Created($"{Request.Path}/{newproduct.Id}", _mapper.Map<ProductDto>(newproduct));
        }


        [HttpPost(Name=nameof(CreateRage))]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryUpDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson, "categoryId" })]
        public async Task<IActionResult> CreateRage(ProductDto[] productDto)
        {

            var prods = _mapper.Map<Product[]>(productDto);
            var newproducts = await _productService.AddRangeAsync(prods);
            return Created("", _mapper.Map<ProductDto[]>(newproducts));
        }


        [HttpPut]
        [TypeFilter(typeof(NotFoundFilter<Product, ProductDto>))]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryUpDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson, "categoryId" })]
        public async Task<IActionResult> Update(ProductUpDto productDto)
        {
             _productService.Update(_mapper.Map<Product>(productDto));
  
            return NoContent();
        }


        [HttpDelete("{id}")]
        [TypeFilter(typeof(NotFoundFilter<Product, ProductDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);
     
            return NoContent();
        }

    }
}