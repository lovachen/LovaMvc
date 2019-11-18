using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlSerializeUtil
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                try
                {
                    T obj = (T)xs.Deserialize(sr);
                    return obj;
                }
                catch
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serializer<T>(T t)
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, t, xsn);
                string str = sw.ToString();
                return str;
            }
        }
    }
}
