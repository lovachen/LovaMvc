using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Model
{
    /// <summary>
    /// 接口请求结果
    /// </summary>
    public class ApiHttpResult<T>: ApiHttpResult
    { 
        /// <summary>
        /// 结果
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 接口请求结果
    /// </summary>
    public class ApiHttpResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

    }


}
