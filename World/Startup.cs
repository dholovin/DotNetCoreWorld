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
using World.Middleware;
using World.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            //Use one instance throughout application lifecycle. Now may inject into any class/service/controller
            services.AddSingleton(_config);

            services.AddLogging();

            if (_environment.IsDevelopment())
            {
                //"Scoped" dependencies are created and injected as needed per web request                 
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // Implement non-debug  version of MailService;
            }

            //uses 'Scoped' dependency type
            services.AddDbContext<WorldContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnectionString")));
            //int poolSize = 128; //default anyway
            //services.AddDbContextPool<WorldContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnectionString")), poolSize);

            //"Transient" objects are always different; a new instance is provided to every controller and every service
            services.AddTransient<IWorldRepository, WorldRepository>();
            //For Test Project Implementation
            //services.AddTransient<IWorldRepository, MockWorldRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {   
            if (env.IsEnvironment("Development")) // get name from project properties -> debug -> aspnetcore_environment
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information); //Information and everything higher (all but Trace and Debug)
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error); //errors or higher {Error, Critical}
                app.UseExceptionHandler("/home/error");
            }

            //app.UseDefaultFiles(); -> this should try to search for index, default, etc... under wwwroot folder. Instead,  use  routing
            app.UseStaticFiles();

            app.UseNullObjectMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    //defaults: new {controller = "Home", action = "Index"}
                    );
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
