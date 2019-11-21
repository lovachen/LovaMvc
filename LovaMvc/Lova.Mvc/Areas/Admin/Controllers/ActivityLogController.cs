using cts.web.core.Datatable;
using Lova.Framework.Filters;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class ActivityLogController: AdminPrmController
    {

        private ActivityLogService _activityLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityLogService"></param>
        public ActivityLogController(ActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "activityLogIndex")]
        public IActionResult ActivityLogIndex()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("data", Name = "activityLogData"), PrmSpare("activityLogIndex")]
        public IActionResult ActivityLogData(ActivityLogSearchArg arg)
        {
            var parameters = Request.QueryString.ToTableParms();
            var pageList = _activityLogService.AdminSearch(arg, parameters);
            var data = pageList.ToAjax();
            return Json(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("details", Name = "activityLogDetails")]
        public IActionResult ActivityLogDetails(string id)
        {
            var item = _activityLogService.GetActivityLogMapping(id);
            NewJsonValu(item);
            return PartialView(item);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        private void NewJsonValu(Sys_ActivityLogMapping activityLog)
        {
            try
            {
                if (!String.IsNullOrEmpty(activityLog.oldvalue))
                { 
                    ViewBag.OldObject = JsonDocument.Parse(activityLog.oldvalue);
                }
                if (!String.IsNullOrEmpty(activityLog.newvalue))
                {
                    ViewBag.NewObject = JsonDocument.Parse(activityLog.newvalue);
                }
            }
            catch (Exception)
            {

            }
        }









    }
}
