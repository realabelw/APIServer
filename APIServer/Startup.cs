using APIServer.Abstractions;
using APIServer.DataAccessLayer;
using APIServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http.Headers;

namespace APIServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //configurations are automatically passed here
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

            services.AddScoped<IRestaurantDataAccessLayer, RestaurantDataAccessLayer>(); //register dependency on DAL
            services.AddScoped<IRestaurantSearchService, RestaurantSearchService>(); //dependency on service

            string apiKey = Configuration.GetValue<string>("YelpFusionAPIKey");
            string endpoint = Configuration.GetValue<string>("YelpFusionAPIEndpoint");

            //add dependency to httpclient default and named
            services.AddHttpClient(); //default
            services.AddHttpClient("meta", c =>
                {
                    c.BaseAddress = new Uri(endpoint);
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                });

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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
        }
    }
}
