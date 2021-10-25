using AutoMapper;
using EcommerceProject.API.Extensions;
using EcommerceProject.API.Filters;
using EcommerceProject.API.HealthCheck;
using EcommerceProject.Core.Repositories;
using EcommerceProject.Core.Services;
using EcommerceProject.Core.UnitOfWorks;
using EcommerceProject.Data;
using EcommerceProject.Data.Repositories;
using EcommerceProject.Data.UnitOfWorks;
using EcommerceProject.Service.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace EcommerceProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region healthcheck
            services.AddHealthChecks()
            .AddSqlServer(Configuration["ConnectionStrings:SqlConStr"])
            .AddCheck<DatabaseHealthCheck>("Manual DB check", tags: new List<string> { "SQL", "NETWORK" })
            .AddPingHealthCheck(option => option.AddHost("localhost", 5001), "ping localhost:5001", tags: new List<string> { "PING", "NETWORK" })
            .AddDbContextCheck<AppDbContext>("DbContext Health Check", HealthStatus.Unhealthy, tags: new string[] { "db", "sql", "efcore", "sqlserver" });

            services
            .AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(5); //Configures the UI to poll for healthchecks updates every 5 seconds
            })
            .AddInMemoryStorage();
            #endregion
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(NotFoundFilter<,>));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("EcommerceProject.Data");
                });
            });
            #region customFilter
            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\EcommerceProject.API.xml", AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo { Title = AppDomain.CurrentDomain.FriendlyName, Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                //app.UseCustomException();

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceProject.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Products}/{action=GetAll}/{id?}");

                #region healthcheck
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI(opts =>
                {
                    opts.UIPath = "/health-ui";
                });
                #endregion
            });
        }
    }
}