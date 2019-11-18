using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 去除字符串 空格、制表符、换页符等等
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TrimSpace(this string source)
        { 
            return Regex.Replace(source,@"\s", "");
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSBC(this string source)
        {
            char[] c = source.ToCharArray();
            for (int i = 0; i <= c.Length - 1; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDBC(this string source)
        {
            char[] c = source.ToCharArray();
            for (int i = 0; i <= c.Length - 1; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
         
        /// <summary>
        /// 验证是否是邮箱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(this string source)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            return regex.IsMatch(source);
        }
         
        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="source"></param>
        public static bool IsPhoneNumber(this string source)
        {
            Regex regex = new Regex(@"^1[3456789]\d{9}$");
            return regex.IsMatch(source);
        }
    }
}
