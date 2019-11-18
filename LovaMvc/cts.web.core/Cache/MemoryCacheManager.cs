using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AspNetCore.Cache
{
    /// <summary>
    /// 内存缓存实现
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// 健集合
        /// </summary>
        protected static readonly ConcurrentDictionary<string, bool> _allKeys;

        /// <summary>
        /// 
        /// </summary>
        protected CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static MemoryCacheManager()
        {
            _allKeys = new ConcurrentDictionary<string, bool>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryCache"></param>
        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cancellationTokenSource = new CancellationTokenSource();

        }
         
        /// <summary>
        /// 添加key到集合对象里面
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string AddKey(string key)
        {
            _allKeys.TryAdd(key, true);
            return key;
        }

        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        protected string RemoveKey(string key)
        {
            TryRemoveKey(key);
            return key;
        }

        /// <summary>
        /// 尝试移除key
        /// </summary>
        /// <param name="key"></param>
        protected void TryRemoveKey(string key)
        {
            if (!_allKeys.TryRemove(key, out bool _))
                _allKeys.TryUpdate(key, false, false);
        }

        /// <summary>
        /// 移除所有的标记值为false 的 key
        /// </summary>
        private void ClearKeys()
        {
            foreach (var key in _allKeys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                RemoveKey(key);
            }
        }


        /// <summary>
        /// 移除所有的缓存
        /// </summary>
        public void Clear()
        {
            _allKeys.Where(p => p.Value).Select(x=>x.Key).ToList().ForEach(Remove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        protected MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int cacheTime)
        {
            var options = new MemoryCacheEntryOptions().
                AddExpirationToken(new CancellationChangeToken(_cancellationTokenSource.Token))
                .RegisterPostEvictionCallback(PostEviction);

            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime); 

            return options;
        }

        /// <summary>
        /// 当移除缓存时调用，从key集合中移除标记为false 的key，和移除当前的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        private void PostEviction(object key, object value, EvictionReason reason, object state)
        {
            if (reason == EvictionReason.Replaced)
                return;

            ClearKeys();

            TryRemoveKey(key.ToString());
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            T _t = default(T);
            _memoryCache.TryGetValue(key, out _t);
            return _t;
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsSet(string key)
        {
            return _memoryCache.TryGetValue(key, out object _);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _memoryCache.Remove(RemoveKey(key));
        }

        /// <summary>
        /// 正则移除缓存
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, _allKeys.Where(p => p.Value).Select(p => p.Key));
        }

        /// <summary>
        /// 设置缓存并将key添加至key集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
                _memoryCache.Set(AddKey(key), data, GetMemoryCacheEntryOptions(cacheTime));
        }
    }
}
