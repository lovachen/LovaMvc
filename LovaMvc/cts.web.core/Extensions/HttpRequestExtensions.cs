using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// HttpRequest扩展
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }

        /// <summary>
        /// 获取当前请求的绝对路径，含有参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        /// <summary>
        /// 获取Host，完整域名地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetHostUri(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.Value}";
        }


    }
}
