using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Echo.Infrastructure.ErrorHandling;
using Echo.Infrastructure;
using Echo.Application;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;

using FluentValidation;
using Echo.Application.Mapping;
using Echo.Core.Configuration;
using System;

namespace Echo
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
            services.AddCustomErrorHandlingMiddleware();
            services.AddInfrastructure(Configuration);
            services.AddValidatorsFromAssemblyContaining<ApplicationLayer>();
            services.AddAutoMapper(typeof(MappingProfile));
            AddConfiguration(services);
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " ParrotInc Echo APIs"
                });
            });
        }

        private void AddConfiguration(IServiceCollection services)
        {
            services.Configure<PostLimitSettings>(Configuration.GetSection("PostLimitSettings"));
            services.Configure<ValidationSettings>(Configuration.GetSection("ValidationSettings"));
            services.Configure<LogOptions>(Configuration.GetSection("LogOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenUp v1"); });
            }
            app.UseProblemDetails();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}