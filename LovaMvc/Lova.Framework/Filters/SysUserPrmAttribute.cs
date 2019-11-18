using cts.web.core.Model;
using Lova.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Framework.Filters
{

    /// <summary>
    /// 权限验证
    /// </summary>
    public class SysUserPrmAttribute : TypeFilterAttribute
    {
        private bool _ignoreFilter;


        public SysUserPrmAttribute(bool ignore = false) : base(typeof(SysUserPrmFilter))
        {
            _ignoreFilter = ignore;
            Arguments = new object[] { ignore };
        }

        public bool IgnoreFilter => _ignoreFilter;

        private class SysUserPrmFilter : IActionFilter
        {
            private bool _ignoreFilter;
            private WorkContext _workContext;

            public SysUserPrmFilter(bool ignore,
                WorkContext workContext)
            {
                _workContext = workContext;
                _ignoreFilter = ignore;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {

                //获取action上的特性
                var actionFilter = context.ActionDescriptor.FilterDescriptors.Where(o => o.Scope == FilterScope.Action)
                    .Select(o => o.Filter).OfType<SysUserPrmAttribute>().FirstOrDefault();
                //如果忽略
                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                bool result = false;

                var spareFilter = context.ActionDescriptor.EndpointMetadata.OfType<PrmSpareAttribute>().FirstOrDefault();
                if (spareFilter != null)
                {
                    result = _workContext.AuthorityCheck(spareFilter.Name);
                }
                else
                {
                    var route = context.ActionDescriptor.AttributeRouteInfo;

                    if (!String.IsNullOrEmpty(route.Name))
                    {
                        result = _workContext.AuthorityCheck(route.Name);
                    }
                    else
                    {
                        string action = context.RouteData.Values["action"].ToString();
                        string controller = context.RouteData.Values["controller"].ToString();
                        result = _workContext.AuthorityCheck(action, controller);
                    }
                }
                if (result) return;
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult(new AjaxResult() { Code = 1007, Message = "没有权限" });
                }
                else
                {
                    context.Result = new ViewResult() { ViewName = "NoPermission" };
                }

            }



        }
    }
}
