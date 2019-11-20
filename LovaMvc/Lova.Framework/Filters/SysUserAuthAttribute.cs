using cts.web.core.Model;
using Lova.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Framework.Filters
{
    public class SysUserAuthAttribute : TypeFilterAttribute
    {
        private int _platform;
        private bool _ignoreFilter;

        public SysUserAuthAttribute(int platform = 0, bool ignore = false) : base(typeof(SysUserAuthFilter))
        {
            _platform = platform;
            _ignoreFilter = ignore;
            Arguments = new object[] { platform, ignore };
        }


        public bool IgnoreFilter => _ignoreFilter;


        private class SysUserAuthFilter : IAuthorizationFilter
        {
            private int _platform;
            private bool _ignoreFilter;
            private SysUserAuthentication _sysUserAuthentication;

            public SysUserAuthFilter(SysUserAuthentication sysUserAuthentication,
                int platform,
                bool ignore)
            {
                _sysUserAuthentication = sysUserAuthentication;
                _platform = platform;
                _ignoreFilter = ignore;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var actionFilter = context.ActionDescriptor.FilterDescriptors.Where(filter => filter.Scope == FilterScope.Action)
                        .Select(filter => filter.Filter).OfType<SysUserAuthAttribute>().FirstOrDefault();

                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if (context.Filters.Any(filter => filter is SysUserAuthFilter))
                {
                    if (!_sysUserAuthentication.VerifyLogged(_platform))
                    {
                        switch (_platform)
                        {
                            case 0:
                                if (context.HttpContext.Request.IsAjaxRequest())
                                {
                                    AjaxResult apiJsonResult = new AjaxResult() { Success = false, Message = "未登录或已过期" };
                                    context.Result = new JsonResult(apiJsonResult);
                                }
                                else
                                {
                                    context.Result = new RedirectToRouteResult("adminLogin");
                                }
                                break;
                            case 1:
                                context.Result = new JsonResult(new ApiJsonResult() { code = 1003, msg = "未登录或已过期" });
                                break;
                        }
                    };
                }



            }
        }

    }
}
