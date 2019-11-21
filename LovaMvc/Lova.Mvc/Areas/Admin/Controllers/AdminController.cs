using Lova.Framework;
using Lova.Framework.Filters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    [Route("admin/[controller]")]
    [SysUserAuth]
    public abstract class AdminController : AreaBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        protected string UserId { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var workContext = context.HttpContext.RequestServices.GetService<WorkContext>();
            this.UserId = workContext.GetUserId(0);
        }
    }
}
