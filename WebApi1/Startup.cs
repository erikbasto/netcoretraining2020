using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using AT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using AT.DataAccess.Repository;
using AT.IDataAccess.IRepositoryPattern;
using AT.Model.Common;

namespace WebApi1
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
            services.AddControllers();

            services.AddSwaggerGen(c=> {
               c.SwaggerDoc("v1", 
               new OpenApiInfo
               {
                   Title="WebApi1", 
                   Version="v1"
               });     
            });

            services.AddDbContext<ATDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<ProductType>, ProductTypeRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(
                c=>{
                    c.SwaggerEndpoint("/swagger/v1/swagger.json","WebApi1");
                }    
            );
        }
    }
}
