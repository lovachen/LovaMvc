using cts.web.core.Datatable;
using Lova.Framework.Filters;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class NLogController : AdminPrmController
    {
        private SysNLogService _sysNLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysNLogService"></param>
        public NLogController(SysNLogService sysNLogService)
        {
            _sysNLogService = sysNLogService;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "nLogIndex")]
        public IActionResult NLogIndex()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("data", Name = "nLogData"), PrmSpare("nLogIndex")]
        public IActionResult NLogData(NLogSearchArg arg)
        {
            var parameters = Request.QueryString.ToTableParms();
            var pageList = _sysNLogService.AdminSearch(arg, parameters);
            return Json(pageList.ToAjax());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("details", Name = "nLogDetails")]
        public IActionResult NLogDetails(int id)
        {
            return PartialView(_sysNLogService.GetNlog(id));
        }


    }
}
