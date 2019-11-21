using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class ActivityLogCommentController : AdminPrmController
    {
        private SysActivityLogCommentService _activityLogCommentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityLogCommentService"></param>
        public ActivityLogCommentController(SysActivityLogCommentService activityLogCommentService)
        {
            _activityLogCommentService = activityLogCommentService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "logCommentIndex")]
        public IActionResult LogCommentIndex()
        {
            return View(_activityLogCommentService.GetActivityLogComments());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("init", Name = "logCommentINIT")]
        public IActionResult LogCommentInit()
        {
            _activityLogCommentService.Initialize();
            AjaxData.Success = true;
            AjaxData.Message = "初始化成功";
            return Json(AjaxData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("edit", Name = "logCommentEdit")]
        public IActionResult LogCommentEdit(string pk, string value)
        {
            _activityLogCommentService.Update(pk, value);
            AjaxData.Success = true;
            AjaxData.Message = "保存成功";
            return Json(AjaxData);
        }

    }
}
