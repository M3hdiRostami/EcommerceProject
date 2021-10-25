using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerceProject.API.DTOs;
using EcommerceProject.Core.Models;
using EcommerceProject.Core.Services;
using AutoMapper;
using EcommerceProject.API.Filters;

namespace EcommerceProject.API.Controllers
{

    public class CurrenciesController : APIControllerBase
    {
        private readonly IService<CurrencyUnit> _CurrenciesService;
        
        public CurrenciesController(IService<CurrencyUnit> service, IMapper mapper):base(mapper)
        {
            _CurrenciesService = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var curencies = await _CurrenciesService.GetAllAsync();
            return Ok(curencies);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CurrencyDto currency)
        {
            var newCurrency = await _CurrenciesService.AddAsync(_mapper.Map<CurrencyUnit>(currency));

            return Ok(newCurrency);
        }


        [HttpPut]
        [TypeFilter(typeof(NotFoundFilter<CurrencyUnit, CurrencyUpDto>))]
        public async Task<IActionResult> Update(CurrencyUpDto currency)
        {
            _CurrenciesService.Update(_mapper.Map<CurrencyUnit>(currency));

            return NoContent();
        }


        [HttpDelete("{id}")]
        [TypeFilter(typeof(NotFoundFilter<CurrencyUnit, CurrencyUpDto>), Arguments = new object[] { NotFoundFilterCheckType.BySearchIdInDTOJson })]
        public async Task<IActionResult> Delete(int id)
        {
             var currency = _CurrenciesService.GetByIdAsync(id).Result;
            _CurrenciesService.Remove(currency);

            return NoContent();
        }


    }
}