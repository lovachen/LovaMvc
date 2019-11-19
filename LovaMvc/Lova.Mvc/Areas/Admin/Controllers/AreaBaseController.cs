using Lova.Framework.Controllers;
using Lova.Framework.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Admin")]
    [Route("admin/[controller]")]
    [ModelStateFilter]
    public abstract class AreaBaseController: WebBaseController
    {

    }
}
