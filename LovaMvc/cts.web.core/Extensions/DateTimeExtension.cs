using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 时间转为10位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime time)
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            return (long)(time.ToUniversalTime() - startTime).TotalSeconds;
        }

        /// <summary>
        /// 10位时间戳转为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToTime(this long timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 13位时间戳转成时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToTime(this string timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


        /// <summary>
        /// 获取时间戳，毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToMilliseconds(this DateTime time)
        {
            return (time.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// 时间戳毫秒转base64
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToBase64(this DateTime time)
        {
            long timestamp = time.ToMilliseconds();
            byte[] numArray = BitConverter.GetBytes(timestamp);
            return Convert.ToBase64String(numArray);
        }



    }
}
