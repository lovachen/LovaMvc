using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cts.web.core
{
    /// <summary>
    /// 引擎
    /// </summary>
    public abstract class Engine : IEngine
    {
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Engine(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        public virtual IConfiguration Configuration => _configuration;

        /// <summary>
        /// 
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();

            return _serviceProvider;
        }

        /// <summary>
        /// 重写此方法
        /// </summary>
        /// <param name="services"></param>
        public abstract void Initialize(IServiceCollection services);
         
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        /// <summary>
        /// 获取ServiceProvider
        /// </summary>
        /// <returns></returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context != null ? context.RequestServices : ServiceProvider;
        }
    }
}
