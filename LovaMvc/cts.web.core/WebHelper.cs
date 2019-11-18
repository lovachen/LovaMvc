using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace cts.web.core
{
    /// <summary>
    /// 
    /// </summary>
    public class WebHelper : IWebHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="hostingEnvironment"></param>
        public WebHelper(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        public string GetIPAddress()
        {
            string clientIp = string.Empty;

            var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
            if (_httpContextAccessor.HttpContext.Request.Headers != null)
            {
                var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                if (!StringValues.IsNullOrEmpty(forwardedHeader))
                    clientIp = forwardedHeader.FirstOrDefault();
            }
            if (String.IsNullOrEmpty(clientIp) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (clientIp != null && clientIp.Equals("::1", StringComparison.InvariantCultureIgnoreCase))
                return "127.0.0.1";

            if (!String.IsNullOrEmpty(clientIp))
              clientIp = clientIp.Split(':').FirstOrDefault();

            return clientIp;
        }

        /// <summary>
        /// 获取当前请求的Url
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUrlReferrer()
        {
            return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer];
        }

        /// <summary>
        /// 获取本机ip地址
        /// </summary>
        /// <returns></returns>
        public string GetLocalIpAddress()
        {
           return _httpContextAccessor.HttpContext.Connection.LocalIpAddress.ToString();
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string MapPath(string virtualPath)
        {
            return System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, virtualPath);
        }
    }
}
