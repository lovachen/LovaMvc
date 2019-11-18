using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 
    /// </summary>
    public class UTF8Json
    {
        /// <summary>
        /// 将对象序列化成字节数组
        /// </summary>
        public static byte[] ToBytes(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj 为空");
            string jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        /// <summary>
        /// 字节数组转换成对象
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T ToObject<T>(byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array 为空");
            string jsonString = Encoding.UTF8.GetString(array);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
