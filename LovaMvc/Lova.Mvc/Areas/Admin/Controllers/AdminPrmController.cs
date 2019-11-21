using Lova.Framework.Filters;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    [SysUserPrm]
    [Route("admin/[controller]")]
    public abstract class AdminPrmController : AdminController
    {

    }
}
