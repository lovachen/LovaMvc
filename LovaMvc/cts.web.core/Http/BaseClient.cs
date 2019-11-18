using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace cts.web.core.Http
{
    /// <summary>
    /// http请求基础类
    /// </summary>
    public class BaseClient
    {
        /// <summary>
        /// HttpClient 对象
        /// </summary>
        public HttpClient CLIENT { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="basUri">域名。例：https://xx.com</param>
        public BaseClient(HttpClient httpClient, string basUri)
        {
            httpClient.BaseAddress = new Uri(basUri);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            CLIENT = httpClient;
        }
    }
}
