using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static APIServer.DataAccessLayer.DataAccessLayer;

namespace APIServer
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
            //
            //Register services: dependency injection.
            //Interfaces are easily swapable
            //

            //services.AddSingleton //one instance for all
            //services.AddScoped //get a new instance per connection per user
            //services.AddTransient //get a new instance everytime

            services.AddScoped<IRestaurantDataAccessLayer, RestaurantDataAccessLayer>();
            services.AddScoped<IRestaurantBusinessLayer, RestaurantBusinessLayer>();

            //register caching service
            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Twelve Oaks Restaurant Search API",
                    Description = "API Server v1.0.0",
                    TermsOfService = new Uri("http://12os.co.uk/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Abel Wodulo",
                        Email = "abel.wodulo@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/wodulo-abel-255663b1/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License Information",
                        Url = new Uri("http://12os.co.uk/"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting(); //routing middleware, matches request to an endpoint.

            app.UseAuthorization();

            //Execute the matched endpoint or route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
        }
    }
}
