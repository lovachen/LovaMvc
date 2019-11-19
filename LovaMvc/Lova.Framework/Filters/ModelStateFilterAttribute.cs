using cts.web.core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Framework.Filters
{
    public class ModelStateFilterAttribute: TypeFilterAttribute
    {
        private bool _ignoreFilter;


        public ModelStateFilterAttribute(bool ignore = false) :base(typeof(ModelStateFilter))
        {
            _ignoreFilter = ignore;
            Arguments = new object[] { ignore };
        }
        public bool IgnoreFilter => _ignoreFilter;


        private class ModelStateFilter : IActionFilter
        {
            private bool _ignoreFilter;
            public ModelStateFilter(bool ignore)
            {
                _ignoreFilter = ignore;
            }
            public void OnActionExecuted(ActionExecutedContext context)
            { 

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                //获取action上的特性
                var actionFilter = context.ActionDescriptor.FilterDescriptors.Where(o => o.Scope == FilterScope.Action)
                    .Select(o => o.Filter).OfType<ModelStateFilterAttribute>().FirstOrDefault();
                //如果忽略
                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if(!context.ModelState.IsValid)
                {
                    AjaxResult data = new AjaxResult();
                    data.Message = context.ModelState.GetErrMsg();
                    context.Result = new JsonResult(data);
                } 
            }
        }
    }
}
