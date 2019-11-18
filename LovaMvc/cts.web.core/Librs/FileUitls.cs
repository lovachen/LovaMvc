using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileUitls
    {
        /// <summary>
        /// 获取文件的SHA1值
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string GetSHA1(Stream inputStream)
        {
            byte[] b = new byte[inputStream.Length];
            inputStream.Read(b, 0, b.Length);
            var calculator = SHA1.Create();
            byte[] bt = calculator.ComputeHash(b);
            calculator.Clear();
            return BitConverter.ToString(bt).Replace("-", "");

        }

        /// <summary>
        /// 获取文件的SHA1值
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static string GetSHA1(byte[] bt)
        {
            var calculator = SHA1.Create();
            byte[] sha1By = calculator.ComputeHash(bt);
            calculator.Clear();
            return BitConverter.ToString(sha1By).Replace("-", "");

        }
    }
}
