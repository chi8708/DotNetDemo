using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Jsose.CMS.Common.DataCache
{
    public abstract class Cached
    {
        /// <summary>
        /// 添加缓存值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="val">值</param>
        /// <param name="seconds">超时（秒）</param>
        /// <returns>是否成功</returns>
        public abstract bool Add(string key, object val, int seconds);

        /// <summary>
        /// 添加或更新缓存值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="val">值</param>
        /// <param name="seconds">超时（秒）</param>
        /// <returns></returns>
       public abstract bool Set(string key, object val, int seconds);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
       public abstract T Get<T>(string key);

        /// <summary>
        /// 获取全部缓存键名集合
        /// </summary>
        /// <returns>键名集合</returns>
       public abstract ICollection GetAllKeys();

        /// <summary>
        /// 移除缓存键
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>是否成功</returns>
       public abstract bool Remove(string key);

        /// <summary>
        /// 移除全部缓存键
        /// </summary>
        /// <returns>是否成功</returns>
       public abstract bool RemoveAll();
    }
}
