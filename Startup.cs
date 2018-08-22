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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MyStoreApi.Models;

namespace MyStoreApi
{
    public class Startup
    {
        private const string ConnectionString = "Server=localhost;Database=MyStore;User=root;Password=123;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //MySql connection
            services.AddDbContextPool<MyStoreContext>(
                options => options.UseMySql(ConnectionString,
                    mySqlOptionsAction =>
                    {
                        mySqlOptionsAction.ServerVersion(new Version(8, 0, 12), ServerType.MySql);
                    }

                )
            );

            //Redis connection
            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = "localhost";
                    options.InstanceName = "redis_test";
                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
