using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCore.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 启用redis或内存缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Startup 中 的 IConfiguration 对象</param>
        public static void AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }

    }
}
