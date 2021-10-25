using EcommerceProject.API.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EcommerceProject.API.Extensions
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;

                        ErrorDto errorDto = new ErrorDto();
                        //todo:enabale if FluentValidation is used
                        /* if (error.Error.GetBaseException() is FluentValidation.ValidationException)
                             errorDto.Status = 400;
                         else
                        */
                        errorDto.Status = 500;

                        errorDto.Errors.Add(ex.Message);

                        context.Response.StatusCode = errorDto.Status;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                    }
                });
            });
        }
    }
}