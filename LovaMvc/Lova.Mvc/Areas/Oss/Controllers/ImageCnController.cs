using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    public class ImageCnController: AreaOssController
    {

        public ImageCnController()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="name"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("{name}")]
        public IActionResult Get(string name,string query)
        {

            return Content("");
        }


         



    }
}
