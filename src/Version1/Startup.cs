using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Version1.Models;
using Version1.Services;

using Microsoft.Extensions.Configuration;
 

namespace Version1
{
    public class Startup
    {
        
        // public Startup(IHostingEnvironment env)IHostingEnvironment hostingEnv
        public Startup(IHostingEnvironment hostingEnv)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                 //  .SetBasePath(env.ContentRootPath)
                 .SetBasePath(hostingEnv.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnv.EnvironmentName}.json", optional: true);

            if(hostingEnv.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddEntityFramework()
            //    .AddEntityFrameworkSqlServer()
            //    .AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddDbContext<ApplicationDbContext>(ops =>
            ops.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            } );  

            services.AddSession();
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            // Should work like this
            // Refer: https://docs.asp.net/en/latest/fundamentals/configuration.html#using-options-and-configuration-objects
           // services.AddOptions();
            /*IOptions pattern to config how to create the class"AppParameterSettings" 
             * Refer: http://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2*/

             services.Configure<AppParameterSettings>(options => Configuration.GetSection("AppParameterSettings").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if(env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseStaticFiles();
            app.UseSession(new SessionOptions { IdleTimeout = TimeSpan.FromMinutes(60) });

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "Management",
                   template: "{area:exists}/{controller=ManageIndex}/{action=Index}/{param?}");

                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{param?}");
            });
        }
    }
}

