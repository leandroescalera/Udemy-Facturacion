using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System_Ventas.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace System_Ventas
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureApplicationCookie(options=> {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Home/Index";

            });

            services.AddSession(options=> {
                options.Cookie.Name = ".SystemVentas.Session";
                options.IdleTimeout = TimeSpan.FromHours(12);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");
            //app.UseStatusCodePagesWithRedirects("/Error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapAreaRoute(
                //    name: "MyAreaUsuarios",
                //    areaName: "Usuarios",
                //    template: "Usuarios/{controller=Usuarios}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "MyAreaPrincipal",
                    areaName: "Principal",
                    template: "Principal/{controller=Principal}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name:"areas",
                //    template:"{area:exists}/{controller=Principal}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default", 
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
