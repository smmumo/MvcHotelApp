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
using MvcHotelApp.Models;
using MvcHotelApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MvcHotelApp
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
            services.AddControllersWithViews();
            services.AddIdentity<IdentityUser,IdentityRole>(  
                options=>
                {options.Password.RequiredLength=6;
                options.Password.RequiredUniqueChars=3;
               }              
            ).AddEntityFrameworkStores<AppDbContext>();
            //.AddDefaultTokenProviders();
            //.AddEntityFrameworkStores<AppDbContext>();
            //services.AddMvc();
            //  services.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlite(Configuration.GetConnectionString("EmployeeDb")));
            
            // services.AddScoped<IHotelRepository,SQLHotelRepository>();//MockHotelRepository
            //     services.AddDbContext<AppDbContext>(options =>
            // options.UseSqlite(Configuration.GetConnectionString("MvcHotelContext")));

             services.AddScoped<IHotelRepository,SQLHotelRepository>();//MockHotelRepository
                services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("APPSQLDBContext")));

            //APPSQLDBContext
           
            //services.AddIdentity<ApplicationUser, IdentityRole>() .AddDefaultTokenProviders();
                    //.AddEntityFrameworkStores<AppDbContext>()
                    

            //services.AddIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
             //   .AddEntityFrameworkStores<AppDbContext>();
            // services.Configure<IdentityOptions>(
            //      options=>{options.Password.RequiredLength=5;
            //      options.Password.RequiredUniqueChars=3;
            // });

            /* authorize all controler,cant access if u not signed in*/
            /*services.AddMvc(Config=>{
                var policy=new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build();
                Config.Filters.Add(new AuthorizeFilter(policy));
            });*/

            /* claim policy authorization*/
           /* services.AddAuthorization(options=>{
                options.AddPolicy("DeleteRolePolicy",
                policy=>policy.RequireClaim("Delete Role"));
            });*/
            /* services.AddAuthorization(options=>{
                options.AddPolicy("EditRolePolicy",
                policy=>policy.RequireClaim("Edit Role"));
            });*/

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
                //custom error page
                // app.UseExceptionHandler("/Error");
                // app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
            });
        }
    }

    //dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 5.0.0
    //<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.0" />   
    // <PackageReference Include="Microsoft.EntityFrameworkCore" Version="3.0.1" /> 
    //dotnet add package Microsoft.DotNet.Watcher.Tools --version 2.0.2
}
