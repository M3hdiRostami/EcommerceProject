using System;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EcommerceProject.API.DTOs;
using EcommerceProject.Core.Services;

namespace EcommerceProject.API.Filters
{
    public enum NotFoundFilterCheckType
    {
        ByParsingDTO,
        BySearchIdInDTOJson
    }

    /// <summary>
    /// 
    /// It will check if an model is exist or not in database before executing of action methods.
    /// 
    /// usage examples:
    ///  1.[TypeFilter(typeof(NotFoundFilter<Product, ProductDto>),IsReusable =false, Arguments = new object[] { false, true })]
    ///    •Adding DI config in Startup.cs is not neccessary
    ///    •IsReusable=false  => run as Scoped
    ///    •IsReusable=true  => run as Singleton
    ///  2.[ServiceFilter(typeof(NotFoundFilter<Product, ProductDto>))]
    ///    •Add services.[AddScoped,AddTransient,AddSingleton](typeof(NotFoundFilter<,>)); in Startup.cs,
    ///    •passing parameters directly not supported
    /// </summary>
    /// <typeparam name="Entity">Database entity model</typeparam>
    /// <typeparam name="DTO">Data Transfer Object for ${Entity}</typeparam>
    public class NotFoundFilter<Entity, DTO> : IAsyncActionFilter where Entity : class where DTO : class
    {

        private readonly IService<Entity> _Service;
        private readonly IMapper _mapper;
        /// <summary>
        /// Enable it to check existance of Entity type with requested id 
        /// </summary>
        private bool _CheckByParsingDTO { get; }
        /// <summary>
        /// Enable it to check existance of Entity type with requested id 
        /// </summary>
        private bool _CheckBySearchIdInDTOJson { get; }
        /// <summary>
        /// _NameOfIdProperty in request DTO for using with  _CheckBySearchIdInDTOJson
        /// Default is:Id
        /// </summary>
        private string _NameOfIdProperty { get; init; }
        private ErrorDto _errorDto { get; set; }
        public NotFoundFilter(IService<Entity> service, IMapper mapper, NotFoundFilterCheckType checktype = NotFoundFilterCheckType.ByParsingDTO, string NameOfIdProperty = "Id")
        {
            _Service = service;
            _mapper = mapper;
            _NameOfIdProperty = NameOfIdProperty;
            _errorDto = new ErrorDto() { Status = 404 };

            switch (checktype)
            {
                case NotFoundFilterCheckType.ByParsingDTO:
                    _CheckByParsingDTO = true;
                    break;
                case NotFoundFilterCheckType.BySearchIdInDTOJson:
                    _CheckBySearchIdInDTOJson = true;
                    break;

            };
        }
        public async Task Check(Entity[] model)
        {
            foreach (var item in model)
            {
                await Check(item);
            }
           
        }
        public async Task Check(Entity model)
        {

            PropertyInfo idProperty = model.GetType().GetProperty(_NameOfIdProperty, BindingFlags.Public | BindingFlags.Instance);
            var idPropertyvalue = idProperty.GetValue(model);

            if (idPropertyvalue is not null)
            {
                await Check(Convert.ToInt32(idPropertyvalue));

            }
            else
            {

                dynamic param = model;
                await Check(param.Id);
            }

            
            //auto detect key property
            //foreach (var property in model.GetType().GetProperties())
            //{
            //    var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))
            //    as KeyAttribute;
            //    if (attribute != null) // This property has a KeyAttribute
            //    {
            //        // Do something, to read from the property:
            //        int val = Convert.ToInt32(property.GetValue(model));

            //         await Check(val);
            //          return;
            //    }
            //}

        }
        public async Task Check(int Id)
        {
            bool IsExist = await _Service.IsExist(Id);

            if (!IsExist)
            {
                _errorDto.Errors.Add($"Specified { typeof(Entity).Name} with {Id} Id couldn't be found in database");
            }

        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (_CheckByParsingDTO)
                foreach (var argument in context.ActionArguments.Values)
                {

                   
                    if (argument is Array)
                        await Check(_mapper.Map<Entity[]>(argument));
                    else if (argument is DTO)
                        await Check(_mapper.Map<Entity>(argument));
                    else
                        _errorDto.Errors.Add($"Couldn't Check DTO,it should be type of {typeof(DTO)} or {typeof(DTO[])}");
                }



            if (_CheckBySearchIdInDTOJson && _NameOfIdProperty.Length > 0)
            {

                var serialstring = JsonSerializer.Serialize(context.ActionArguments);
                string pattern = $"\"{ _NameOfIdProperty}\":\\w+";
                var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match match = rx.Match(serialstring);
                bool is_IdName_found = false;
                while (match.Success)
                {
                    is_IdName_found |= true;
                    string value = match.Value.Split(":")[1];
                    int Id = Convert.ToInt32(value);

                    await Check(Id);
                    match = match.NextMatch();
                }
                if (!is_IdName_found)
                    _errorDto.Errors.Add($"Couldn't find {_NameOfIdProperty} in request to check it.");

            }
            if (_errorDto.HasError())
            {
                context.Result = new NotFoundObjectResult(_errorDto);
            }
            else
                await next();

        }
    }

}