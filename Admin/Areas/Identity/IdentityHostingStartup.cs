using System;
using Admin.Areas.Identity.Data;
using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Admin.Areas.Identity.IdentityHostingStartup))]
namespace Admin.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AdminContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AdminContextConnection")));
                //services.AddDbContext<Admin_Context>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("AdminContextConnection")));

                services.AddDefaultIdentity<AdminUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<AdminContext>();
            });
        }
    }
}