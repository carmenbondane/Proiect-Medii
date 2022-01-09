using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bondane_Carmen_Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Bondane_Carmen_Proiect
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
            services.Configure<IdentityOptions>(options => {

                //lungimea minima a parolei sa fie de min 7 caractere
                options.Password.RequiredLength = 7;

                // accesul unui utilizator sa fie blocat dupa 3 incercari esuate de autentificare
                options.Lockout.MaxFailedAccessAttempts = 3;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });

            //doar cei care lucreaza in departamentul Spital si au rolul de Manager pot accesa
            services.AddAuthorization(opts => {
                opts.AddPolicy("HospitalManager", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Department", "Hospital");
                });
            });
            //doar cei care au Pozitia in spital de Sef de sectie pot accesa
            services.AddAuthorization(opts => {
                opts.AddPolicy("OnlyHeadDoctor", policy => {
                    policy.RequireClaim("Position", "HeadDoctor");
                });
            });

            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });

            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}
