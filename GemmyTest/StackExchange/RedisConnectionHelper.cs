using Common;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading.Tasks;

namespace RedisHelp
{
    /// <summary>
    /// ConnectionMultiplexer对象管理帮助类
    /// </summary>
    public static class RedisConnectionHelper
    {
        //系统自定义Key前缀
        public static readonly string SysCustomKey = ConfigurationManager.AppSettings["redisKey"] ?? "";

        //"127.0.0.1:6379,allowadmin=true
        private static readonly string RedisConnectionString = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;

        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _instance;
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();
       
        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {

            connectionString = connectionString ?? RedisConnectionString;
            try
            {

                var connect = ConnectionMultiplexer.Connect(connectionString);
                //注册如下事件
                connect.ConnectionFailed += MuxerConnectionFailed;
                connect.ConnectionRestored += MuxerConnectionRestored;
                connect.ErrorMessage += MuxerErrorMessage;
                connect.ConfigurationChanged += MuxerConfigurationChanged;
                connect.HashSlotMoved += MuxerHashSlotMoved;
                connect.InternalError += MuxerInternalError;
    
                return connect;

            }
            catch (Exception ex)
            {
                Task.Run(() => WrtieRedisLog(LogLevel.Error, "redis初始化错误 GetManager: " + ex.Message));
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// redis日志写入
        /// </summary>
        public static void WrtieRedisLog(LogLevel logLevel, string message)
        {
            LogHelper m_Log = LogFactory.GetLogger(LogType.RedisLog);
            switch (logLevel)
            {
                case LogLevel.Debug:
                    m_Log.Debug(message);
                    break;
                case LogLevel.Error:
                    m_Log.Error(message);
                    break;
                case LogLevel.Info:
                    m_Log.Info(message);
                    break;
                case LogLevel.Warning:
                    m_Log.Warning(message);
                    break;
            }
        }

        #region 事件

      
        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Task.Run(() => WrtieRedisLog(LogLevel.Warning, "Configuration changed: " + e.EndPoint));
           // Console.WriteLine("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Task.Run(() => WrtieRedisLog(LogLevel.Error, "ErrorMessage: " + e.Message));

           // Console.WriteLine("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Task.Run(() => WrtieRedisLog(LogLevel.Warning, "ConnectionRestored 启用成功: " + e.EndPoint));
           // Console.WriteLine("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
           string msg="重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message));
           Task.Run(() => WrtieRedisLog(LogLevel.Error, msg));
           // Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {

            Task.Run(() => WrtieRedisLog(LogLevel.Info,"HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint));
          //  Console.WriteLine("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {

            Task.Run(() => WrtieRedisLog(LogLevel.Error, "InternalError:Message" + e.Exception.Message));
          //  Console.WriteLine("InternalError:Message" + e.Exception.Message);
        }

        #endregion 事件
    }
}