using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VernumBlog.Areas.Identity.Data;
using VernumBlog.Data;

[assembly: HostingStartup(typeof(VernumBlog.Areas.Identity.IdentityHostingStartup))]
namespace VernumBlog.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<VernumBlogContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("VernumBlogContextConnection")));

                services.AddDefaultIdentity<BlogUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<VernumBlogContext>();
            });
        }
    }
}