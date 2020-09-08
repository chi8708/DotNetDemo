using Memcached.ClientLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Jsose.CMS.Common.DataCache
{
    public class MemCache : Cached
    {
        private static readonly MemCache instance = new MemCache();
        private static MemcachedClient client;

        private MemCache()
        {
            try
            {
                var pool = SockIOPool.GetInstance("Memcache");
                pool.SetServers(new string[] { System.Configuration.ConfigurationManager.AppSettings["MemcacheServer"].ToString() });
                pool.Initialize();

                client = new MemcachedClient();
                client.PoolName = "Memcache";
                client.EnableCompression = false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool Add(string key, object val, int seconds)
        {
            if (null != client)
            {
                try 
                {
                    return client.Add(key, val, DateTime.Now.AddSeconds(seconds));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return false;
        }

        public override bool Set(string key, object val, int seconds)
        {
            if (null != client)
            {
                try
                {
                    return client.Set(key, val, DateTime.Now.AddSeconds(seconds));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return false;
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

        public override ICollection GetAllKeys()
        {
            if (null != client)
            {
                try
                {
                    return client.Stats().Keys;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return null;
        }

        public override bool Remove(string key)
        {
            if (null != client)
            {
                try
                {
                    return client.Delete(key);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return false;
        }

        public override bool RemoveAll()
        {
            if (null != client)
            {
                try
                {
                    return client.FlushAll();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return false;
        }

        public static MemCache GetInstance()
        {
            return instance;
        }
    }
}
