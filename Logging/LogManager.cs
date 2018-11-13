﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace VarProject.FrameWork.Core.Logging
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LogManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        private static readonly object LockObj = new object();
        internal static ICollection<ILoggerAdapter> Adapters { get; private set; }

        static LogManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
            Adapters = new List<ILoggerAdapter>();
        }

        public static void AddLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.Any(m => m == adapter))
                {
                    return;
                }
                Adapters.Add(adapter);
                Loggers.Clear();
            }
        }


        public static void RemoveLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.All(m => m != adapter))
                {
                    return;
                }
                Adapters.Remove(adapter);
                Loggers.Clear();
            }
        }



        /// <summary>
        /// 设置日志记录入口参数
        /// </summary>
        /// <param name="enabled">是否允许记录日志，如为 false，将完全禁止日志记录</param>
        /// <param name="entryLevel">日志级别的入口控制，级别决定是否执行相应级别的日志记录功能</param>
        public static void SetEntryInfo(bool enabled, LogLevel entryLevel)
        {
            InternalLogger.EntryEnabled = enabled;
            InternalLogger.EntryLogLevel = entryLevel;
        }



        /// <summary>
        /// 获取日志记录者实例
        /// </summary>
        public static ILogger GetLogger(string name)
        {
            lock (LockObj)
            {
                ILogger logger;
                if (Loggers.TryGetValue(name, out logger))
                {
                    return logger;
                }
                logger = new InternalLogger(name);
                Loggers[name] = logger;
                return logger;
            }
        }

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILogger GetLogger(Type type)
        {
            if (type == null) throw new ArgumentNullException();
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }


    }
}
