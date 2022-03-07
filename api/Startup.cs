using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library.datacenter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using library.utilities;

namespace api
{
    public class Startup
    {
        private string EFConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DbServer"] ?? Configuration.GetValue<string>("ConnectionStrings:DbServer");
            var port = Configuration["DbPort"] ?? Configuration.GetValue<string>("ConnectionStrings:DbPort");
            var user = Configuration["DbUser"] ?? Configuration.GetValue<string>("ConnectionStrings:DbUser");
            var password = Configuration["DbPassword"] ?? Configuration.GetValue<string>("ConnectionStrings:DbPassword");
            var database = Configuration["DbName"] ?? Configuration.GetValue<string>("ConnectionStrings:DbName");
            EFConnectionString = $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}";
            System.Console.WriteLine(EFConnectionString);
            //services.AddDbContext<APIDbContext>(options => options.UseSqlServer(EFConnectionString));
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //DbContextMigrations.InitialMigrate(app);

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
