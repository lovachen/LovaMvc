using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AspNetCore.Cache
{
    /// <summary>
    /// 缓存扩展类型
    /// </summary>
    public static class CacheExtensions
    {
        private static int DefaultCacheTimeMinutes { get { return 60; } }

        /// <summary>
        /// 如果缓存存在就从缓存获取，不存在执行Func并缓存后返回 默认 60 分钟
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire">func委托方法</param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, DefaultCacheTimeMinutes, acquire);
        }

        /// <summary>
        /// 如果缓存存在就从缓存获取，不存在执行Func并缓存后返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime">缓存分钟</param>
        /// <param name="acquire">func委托方法</param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            var result = acquire();

            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);

            return result;
        }

        /// <summary>
        /// 正则移除缓存
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="pattern"></param>
        /// <param name="keys"></param>
        public static void RemoveByPattern(this ICacheManager cacheManager, string pattern, IEnumerable<string> keys)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matchesKeys = keys.Where(key => regex.IsMatch(key)).ToList();

            matchesKeys.ForEach(cacheManager.Remove);
        }
    }
}
