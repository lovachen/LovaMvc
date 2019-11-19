﻿using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Model
{
    /// <summary>
    /// ajax请求结果
    /// </summary>
    [Serializable]
    public class AjaxResult
    {
        /// <summary>
        /// 
        /// </summary>
        public AjaxResult()
        {
            Message = ""; 
        }

        /// <summary>
        /// 状态码 0 代表请求正常
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public object Data { get; set; }
    }
}
