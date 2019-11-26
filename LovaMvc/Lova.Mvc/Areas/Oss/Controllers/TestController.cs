using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cts.web.core.Librs;
using Force.Crc32;
using Microsoft.AspNetCore.Mvc;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    [Area("oss")]
    [Route("oss/test")]
    public class TestController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }
    }
}