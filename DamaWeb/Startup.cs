using DamaWeb.Tools;
using DamaWeb.Tools.BackGroundTask;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Config { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration config, IWebHostEnvironment environment)
        {
            Config = config;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            MicroORM.ORMConfig.ConnectionString = Config.GetConnectionString("DefaultConnection");
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
            MicroORM.Logging.FileLoggerOptions.FolderPath = System.IO.Path.Combine(this.Environment.WebRootPath, "Log");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Home/Login";
                options.LogoutPath = "/Home/Login";                
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddHostedService<TimeHostedService>();
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            //services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
