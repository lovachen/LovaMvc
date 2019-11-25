using cts.web.core.Model;
using Lova.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    [Route("oss/[controller]")]
    public abstract class AreaOssController: WebBaseController
    {
        public ApiJsonResult ApiData = new ApiJsonResult() { code = -1, msg = "未知信息" };
    }
}
