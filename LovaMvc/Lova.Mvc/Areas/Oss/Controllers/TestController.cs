using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cts.web.core.Librs;
using Microsoft.AspNetCore.Mvc;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    [Area("oss")]
    [Route("oss/test")]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
           var md5 = EncryptorHelper.GetMD5Byte(CombGuid.NewGuidAsString());
           var i = CRC32.GetCRC32(md5);
           var d = i % 256;

            return View();
        }
    }
}