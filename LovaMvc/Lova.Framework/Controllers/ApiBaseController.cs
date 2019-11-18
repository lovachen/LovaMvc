using Lova.Framework.Filters;
using cts.web.core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Framework.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [WebException(1)]
    [ProducesResponseType(typeof(ApiJsonResult), 200)]
    public abstract class ApiBaseController: BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiJsonResult ApiData = new ApiJsonResult() { code = -1, msg = "未知信息" };
         
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        protected IActionResult Ok((bool Status, string Message) res)
        {
            ApiData.code = res.Status ? 0 : 2001;
            ApiData.msg = res.Message;
            return Ok(ApiData);
        }

        /// <summary>
        /// 返回结果数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult OkModel(object data)
        {
            ApiData.code = 0;
            ApiData.msg = "获取成功";
            ApiData.data = data;
            return Ok(ApiData);
        }

    }
}
