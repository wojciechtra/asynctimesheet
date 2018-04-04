using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Core.EntityFramework.UoW;
using DataAccessLayer.Core.Interfaces.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timesheet.BLL.Interfaces;
using Timesheet.DAL.EntityFramework;
using Timesheet.DAL.Repositories;

namespace Timesheet.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = false;
                
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITimesheetRepository, MockTimesheetRepository>();
            services.AddMvc();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            Services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            var dbContext = Services.BuildServiceProvider().GetRequiredService<AppDbContext>();

            var userManager = Services.BuildServiceProvider().GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            DbInitializer.Seed(dbContext, userManager, roleManager);

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
