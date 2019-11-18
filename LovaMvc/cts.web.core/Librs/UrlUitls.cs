using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlUitls
    {
        /// <summary>
        /// url添加参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param">多个参数，例：abc=11111</param>
        /// <returns></returns>
        public static string AddParam(string url, params string[] param)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url) + "不能为空");

            StringBuilder sb = new StringBuilder();
            sb.Append(url.TrimEnd('&'));

            if (param != null && param.Length > 0)
            {
                if (url.IndexOf('?') == -1)
                    sb.Append("?");
                foreach (var p in param)
                    sb.Append("&" + p);
            }
            return sb.ToString();

        }
    }
}
