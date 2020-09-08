using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Com.Jsose.CMS.Common.DataCache
{
    public class HttpCache : Cached
    {
        private static readonly HttpCache instance = new HttpCache();
        private static System.Web.Caching.Cache client;

        private HttpCache()
        {
            client = HttpRuntime.Cache;
        }

        public override bool Add(string key, object val, int seconds)
        {
            try
            {
                client.Insert(key, val, null, DateTime.Now.AddSeconds(seconds), TimeSpan.Zero);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool Set(string key, object val, int seconds)
        {
            return this.Add(key, val, seconds);            
        }

        public override T Get<T>(string key)
        {
            if (null != client)
            {
                try
                {
                    var obj = client.Get(key);
                    return null != obj ? (T)obj : default(T);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return default(T);
        }

        public override System.Collections.ICollection GetAllKeys()
        {
            var result = new List<string>();
            var enumerator = client.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Key.ToString());
            }
            return result;
        }

        public override bool Remove(string key)
        {
            try
            {
                client.Remove(key);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool RemoveAll()
        {
            try
            {
                var allKeys = this.GetAllKeys() as List<string>;
                foreach (var key in allKeys)
                {
                    this.Remove(key);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpCache GetInstance()
        {
            return instance;
        }
    }
}
