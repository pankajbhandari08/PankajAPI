using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PankajAPI.Filters;

namespace PankajAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //This method adds the services to be used. THis is for DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options=> {

                options.Filters.Add<JsonExceptionFilter>();
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument();

            services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.ApiVersionReader = new MediaTypeApiVersionReader();
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.ApiVersionSelector = new CurrentImplementationApiVersionSelector(option);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //This method sets the pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

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
