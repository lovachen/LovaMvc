using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebHelper
    {
        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        string GetIPAddress();

        /// <summary>
        /// 获取当前请求的Url
        /// </summary>
        /// <returns></returns>
        string GetUrl();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUrlReferrer();

        /// <summary>
        /// 获取本机ip地址
        /// 如果是hostlocal 会获取 127.0.0.1
        /// </summary>
        /// <returns></returns>
        string GetLocalIpAddress();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        string MapPath(string virtualPath);
    }
}
