using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Cache
{
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 通过制定的Key获取缓存的数据，不存在则返回默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>缓存的数据</returns>
        T Get<T>(string key);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">缓存时间，分钟数</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 验证缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 正则移除缓存
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清理缓存
        /// </summary>
        void Clear();
    }
}
