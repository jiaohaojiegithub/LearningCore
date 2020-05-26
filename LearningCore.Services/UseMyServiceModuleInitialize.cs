using LearningCore.Common.Extentions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningCore.Services
{
    /// <summary>
    /// 服务依赖注入
    /// </summary>
    public static class UseMyServiceModuleInitialize
    {
        public static void UseMyService(this IServiceCollection services)
        {
            //AutoMapper配置
            //services.AddScoped<IConfigurationProvider>(_ => AutoMapperConfig.GetMapperConfiguration());
            //services.AddScoped(_ => AutoMapperConfig.GetMapperConfiguration().CreateMapper());

            //通过反射，批量取出需要注入的接口和实现类
            var registrations =
                from type in typeof(UseMyServiceModuleInitialize).Assembly.GetTypes()
                where type.Namespace != null && (!type.Namespace.IsNullOrWhiteSpace() &&
                                               type.Namespace.StartsWith("LearningCore.Services") &&
                                               type.GetInterfaces().Any(x => x.Name.EndsWith("Service")) &&
                                               type.GetInterfaces().Any())
                select new { Service = type.GetInterfaces().First(), Implementation = type };

             foreach (var t in registrations)
            {
                services.AddScoped(t.Service, t.Implementation);
            }
        }
    }
}
