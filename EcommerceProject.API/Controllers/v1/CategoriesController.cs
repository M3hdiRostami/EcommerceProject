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

namespace EcommerceProject.API.Controllers
{
  
    public class CategoriesController : APIControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService, IMapper mapper):base(mapper)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }


        [HttpGet("{id}")]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }


        [HttpGet("{id}/products")]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);

            return Ok(_mapper.Map<CategoryWithProductDto>(category));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            return Created($"{Request.Path}/{newCategory.Id}", _mapper.Map<CategoryDto>(newCategory));
        }


        [HttpPut]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryUpDto>))]
        public async Task<IActionResult> Update(CategoryUpDto category)
        {
             _categoryService.Update(_mapper.Map<Category>(category));

            return NoContent();
        }


        [HttpDelete("{id}")]
        [TypeFilter(typeof(NotFoundFilter<Category, CategoryUpDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);

            return NoContent();
        }


    }
}