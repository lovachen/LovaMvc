using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fast.Framework;
using Microsoft.AspNetCore.Mvc;
using Lova.Mapping;
using Lova.Entities;
using Lova.Services;
using Lova.Mvc.Areas.Admin.Controllers;

namespace Lova.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary> 
    public class QuarztController : AdminPrmController
    {
        private QuarztScheduleService _quarztScheduleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quarztScheduleService"></param>
        public QuarztController(QuarztScheduleService quarztScheduleService)
        {
            _quarztScheduleService = quarztScheduleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "quarztIndex")]
        public IActionResult QuarztIndex()
        { 
            return View(_quarztScheduleService.GetTaskList());
        } 

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <returns></returns>
        [Route("start",Name = "startQuarzt")]
        public IActionResult StartQuarzt(Guid id)
        {
            var res = _quarztScheduleService.Start(id);
            return Json(res);
        }


        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns></returns>
        [Route("stop", Name = "stopQuarzt")]
        public IActionResult StopQuarzt(Guid id)
        {
            var res = _quarztScheduleService.Stop(id);
            return Json(res);
        }


         






    }
}