using cts.web.core.Librs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IServiceCollection 容器的拓展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 程序集依赖注入
        /// 如果类未包含接口类型则默认注入类，否则使用接口引用
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="serviceLifetime"></param>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");

            if (String.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName) + "为空");

            var assembly = RuntimeHelper.GetAssemblyByName(assemblyName);

            if (assembly == null)
                throw new DllNotFoundException(nameof(assembly) + ".dll不存在");

            var types = assembly.GetTypes();
            var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();
            if (list == null && !list.Any())
                return;
            foreach (var type in list)
            {
                var interfacesList = type.GetInterfaces();
                //如果类未包含接口类型则默认注入类
                if (interfacesList == null || !interfacesList.Any())
                {
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(type);
                            break;
                    }
                }
                else
                {
                    var inter = interfacesList.First();
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(inter, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(inter, type);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 配置文件扩展
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class,new()
        {
            var config = new TConfig();

            configuration.Bind(config);

            services.AddSingleton(config);

            return config;
        }


    }
}
