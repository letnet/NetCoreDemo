using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreTemplate.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 自动注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoInject(this IServiceCollection services)
        {
            var allTypes = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(p => !Path.GetFileName(p).StartsWith("System.")
                            && !Path.GetFileName(p).StartsWith("Microsoft."))
                .Select(Assembly.LoadFrom)
                .SelectMany(y => y.DefinedTypes)
                .ToList();
            allTypes?.ForEach(thisType =>
            {
                //注入的限制
                var allInterfaces = thisType.GetInterfaces().Where(p => p.GetInterfaces().Contains(typeof(IBaseApplication)) || p.GetInterfaces().Contains(typeof(IBaseDomain))).ToList();
                allInterfaces?.ForEach(thisInterface => {
                    services.AddScoped(thisInterface, thisType);//只要符合条件的所有接口都注入t类
                });
            });
        }

        /// <summary>
        /// 自动创建映射
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var allProfile = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(p => !Path.GetFileName(p).StartsWith("System.") 
                            && !Path.GetFileName(p).StartsWith("Microsoft."))
                .Select(Assembly.LoadFrom)
                .SelectMany(y => y.DefinedTypes)
                .Where(x => typeof(Profile).IsAssignableFrom(x) && x.IsClass)?
                .ToArray();
            if (allProfile != null)
                services.AddAutoMapper(allProfile);
        }
    }
}
