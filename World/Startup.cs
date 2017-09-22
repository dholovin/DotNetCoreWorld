using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using World.Services;
using Microsoft.Extensions.Configuration;

namespace World
{
    public class Startup
    {
        private IHostingEnvironment _environment;
        private IConfigurationRoot _config;

        //public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _environment = env;

            //Configuration = configuration;
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(_environment.ContentRootPath)  // exactly web site root path                
                .AddJsonFile("config.json")
                .AddEnvironmentVariables(); // Order Matters -> Environment Variables  might override settings in config.json (useful for multiple different environments)
            _config =  builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (_environment.IsDevelopment())
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // Implement non-debug  version of MailService;
            }

            services.AddSingleton(_config);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseDefaultFiles(); -> this should try to search for index, default, etc... under wwwroot folder. Instead,  use  routing
            
            app.UseStaticFiles();

            app.UseNullObjectHandler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    //defaults: new {controller = "Home", action = "Index"}
                    );
            });

            if (env.IsEnvironment("Development")) // get name from project poroperties -> debug -> aspnetcore_environment
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
