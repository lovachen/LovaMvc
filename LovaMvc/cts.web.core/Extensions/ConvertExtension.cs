using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 转换扩展
    /// </summary>
    public static class ConvertExtension
    {
        private readonly static char[] charSet = new char[] { '0', '1', '2', '3','4','5','6','7','8','9',
           'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        /// <summary>
        /// 将数字id转换成62进制字符串短id
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string To62(this long value, int length = 6)
        {
            StringBuilder sb = new StringBuilder();

            if (value < 62)
            {
                sb.Append(charSet[value]);
            }
            else
            {
                long result = value;
                while (result > 0)
                {
                    long val = result % 62;
                    sb.Insert(0, charSet[val]);
                    result = result / 62;
                }
            }
            return sb.ToString().PadLeft(length, '0');
        }
    }
}
