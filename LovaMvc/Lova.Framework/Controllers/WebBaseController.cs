using cts.web.core.Model;
using Lova.Framework.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Framework.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [WebException(0)]
    public abstract class WebBaseController:BaseController
    {

        /// <summary>
        /// ajax请求返回结果
        /// </summary> 
        protected AjaxResult AjaxData = new AjaxResult() { Status = -1, Message = "未知信息" };


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IActionResult NotValid()
        {
            AjaxData.Status = 1005;
            AjaxData.Message = ModelState.GetErrMsg();
            return new JsonResult(AjaxData);
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        protected IActionResult Json((bool Status, string Message) res)
        {
            AjaxData.Status = res.Status ? 0 : 2001;
            AjaxData.Message = res.Message;
            return Json(AjaxData);
        }

        /// <summary>
        /// 返回结果数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult JsonModel(object data)
        {
            AjaxData.Status = 0;
            AjaxData.Message = "获取成功";
            AjaxData.Data = data;
            return Json(AjaxData);

        }
    }
}
