using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using LearningCore.Api.Middlewares;
using LearningCore.Data;
using LearningCore.Service;
using LearningCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LearningCore.Api
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

            services.AddControllers(option=> {
                option.RespectBrowserAcceptHeader = true;
            })
                .AddXmlSerializerFormatters()//添加 XML 格式支持
                .AddJsonOptions(option => {
                    option.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    // Use the default property (Pascal) casing.
                    option.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                ;
            #region 自定义注入
            services.Configure<WxConfigModel>(Configuration.GetSection("WxConfig"));//WxConfig注入
            //services.AddTransient<IMyScopedService, MyScopedService>();
            services.UseMyService();
            #endregion
            #region 注入Swagger
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
            #endregion

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
                app.UseExceptionHandler("/error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            #region 自定义中间件
            //app.UseCustomMiddleware();
            //app.UseRequestCultureMiddleware();
            #endregion

            #region 注入Swagger
            bool.TryParse(Configuration.GetSection("IsUseSwagger").Value,out bool IsUseSwagger);
            if (IsUseSwagger)
            {
                // Register the Swagger generator and the Swagger UI middlewares
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            #endregion

          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
