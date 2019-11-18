using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 常用工具类
    /// </summary>
    public class ComomHelper
    {

        /// <summary>
        /// 获取行政区域编码的有效起始位
        /// 例：海南 460000==>46
        /// </summary>
        /// <param name="areaID">行政区域编码</param>
        /// <returns></returns>
        public static string StartAeaID(string areaID)
        {
            if (!String.IsNullOrEmpty(areaID) && areaID.Length == 6)
            {
                var arr = areaID.ToCharArray();
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    if (arr[i] != '0')
                    {
                        string start = areaID.Substring(0, i + 1);
                        return start;
                    }
                }
            }
            return "";
        }


    }
}
