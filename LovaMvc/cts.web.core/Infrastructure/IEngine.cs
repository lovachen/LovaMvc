using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        void Initialize(IServiceCollection services);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceProvider ConfigureServices(IServiceCollection services);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;








    }
}
