using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Book.Apis
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book.Apis", Version = "v1" });
            });


            AddDependencyInjectionContainerForBookList(services);
            
            // CORS 
            #region CORS

            // // Allowing all
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                options.AddPolicy("AllowAnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            
            // Allowing all
            // services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
            // {
            //     options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            // }));

            // Allowing specific domains
            // services.AddCors(o => o.AddPolicy("AllowSpecific", options =>
            // {
            //     options.WithOrigins("https://localhost:5001")
            //         .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
            //         .WithHeaders("accept", "content-type", "origin", "X-TotalRecordCount");
            // }));

            #endregion

        }

        // Managing DependencyInjection  
        private void AddDependencyInjectionContainerForBookList(IServiceCollection service)
        {
            service.AddDbContext<BookDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            service.AddTransient<IBookRepository, BookRepository>();
        }
        
        
        // //
        // //     // [SQL server] 
        // //     // service.AddEntityFrameworkSqlServer().AddDbContext<BookDbContext>(Options =>
        // //     //     Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        // }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book.Apis v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}