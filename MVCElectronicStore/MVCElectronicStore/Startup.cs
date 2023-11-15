using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MVCElectronicStore.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCElectronicStore
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
            services.AddDistributedMemoryCache(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc()
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<electronic_storeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ElectronicStoreDBContext")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DBHelper>();
            services.AddHttpContextAccessor();


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
            app.UseSession();
            app.UseHttpMethodOverride();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "Product/Search",
                    defaults: new { controller = "Product", action = "Search" });
                endpoints.MapControllerRoute(
                    name: "AddToCart",
                    pattern: "cart/add",
                    defaults: new { controller = "Cart", action = "AddToCart" });
                endpoints.MapControllerRoute(
                    name: "myOrders",
                    pattern: "cart/my-orders",
                    defaults: new { controller = "Cart", action = "MyOrders" });

                endpoints.MapControllerRoute(
                    name: "cancelOrder",
                    pattern: "cart/cancel-order/{orderId}",
                    defaults: new { controller = "Cart", action = "CancelOrder" });

                endpoints.MapControllerRoute(
                    name: "confirmCancelOrder",
                    pattern: "cart/confirm-cancel-order/{orderId}",
                    defaults: new { controller = "Cart", action = "ConfirmCancelOrder" });


            });
        }
    }
}
