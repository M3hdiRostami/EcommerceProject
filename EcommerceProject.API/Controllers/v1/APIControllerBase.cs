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

namespace EcommerceProject.API.Controllers
{


    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class APIControllerBase : ControllerBase
    {
        protected readonly IMapper _mapper;
        public APIControllerBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
