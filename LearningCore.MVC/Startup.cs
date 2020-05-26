using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using LearningCore.Data;
using LearningCore.MVC.Filters;
using LearningCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LearningCore.MVC
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
            #region 数据库连接
            //services.AddDbContext<BloggingContext>(options =>
            //                                       options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")
            //                                       , providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddDbContext<LearningCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LearningDatabase")));
            #endregion
            #region 自定义注入
            services.Configure<WxConfigModel>(Configuration.GetSection("WxConfig"));//WxConfig注入
            services.AddScoped<MyActionFilterAttribute>();
            #endregion
            services.AddControllersWithViews()
                .AddJsonOptions(option => option.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All))//json序列化乱码问题
                 .AddMvcOptions(option =>
                 {
                     option.Filters.Add(typeof(MyActionFilterAttribute));//全局过滤器
                 })
                ;
            

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
