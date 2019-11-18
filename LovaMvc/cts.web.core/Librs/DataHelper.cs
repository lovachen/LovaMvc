using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 数据帮助类
    /// </summary>
    public class DataHelper
    {

        /// <summary>
        /// 获取数据的默认类型，否则返回null
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object DefaultForType(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        /// <summary>
        /// 将字典中的数据转成对象，通过反射获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T ConvertDictionaryToEntity<T>(Dictionary<string, string> dic)
        {
            T val = Activator.CreateInstance<T>();
            PropertyInfo[] propertyInfos =  val.GetType().GetProperties();

            if (propertyInfos.Length>0 && dic.Keys.Count > 0)
            {
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (info.CanWrite)
                    {
                        string key = info.Name;
                        string value = dic[key] == null ? String.Empty : dic[key].ToString();
                        try
                        { 
                            info.SetValue(val, value);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return val;
        }
         


    }
}
