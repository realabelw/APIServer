using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        }
    }
}
