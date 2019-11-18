using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast.Framework
{
    public class AddAuthTokenHeaderParameter<T>: IOperationFilter where T:class
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

            //先判断是否是匿名访问,
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                if (descriptor.FilterDescriptors.Select(o=>o.Filter).OfType<T>().Any())
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "token",
                        In = "header",//query header body path formData
                        Type = "string",
                        Required = true //是否必选
                    });
                }
            } 
        }
         
    }
}
