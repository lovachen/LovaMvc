using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {

            context.Fail();

            return Task.CompletedTask;
        }
    }
}
