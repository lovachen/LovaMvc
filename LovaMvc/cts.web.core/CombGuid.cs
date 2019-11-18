using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System
{
    /// <summary>
    /// COMB Guid生成
    /// </summary>
    public sealed class CombGuid
    {
        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// 获取可排序的guid
        /// 取时间的1/10毫秒进行排序，也就是计算在1毫秒内可以存在10个相同尾序的ID，对于一般性系统来说可以接受
        /// </summary>
        /// <param name="atStart">时间戳排在字符串前面，对于mysql来说有用</param>
        /// <returns></returns>
        public static Guid NewGuid(bool atStart = false)
        {
            var guidArray = Guid.NewGuid().ToByteArray();
            long timestamp = (DateTime.Now - Jan1st1970).Ticks / 10000L;// 1/10毫秒
            var numArray = BitConverter.GetBytes(timestamp);
            Array.Reverse(numArray);
            if (atStart)
            {
                Array.Copy(numArray, 2, guidArray, 0, 6);
            }
            else
            {
                Array.Copy(numArray, 2, guidArray, guidArray.Length - 6, 6);
            }
            return new Guid(guidArray);
        }

    }
}
