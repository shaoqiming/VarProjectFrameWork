using System;
using System.Collections.Concurrent;

namespace VarProject.FrameWork.Core.Logging
{
    public abstract class LoggerAdapterBase : ILoggerAdapter
    {

        //线程安全的一个日志缓存
        private readonly ConcurrentDictionary<string, ILog> _cacheLoggers;

        protected LoggerAdapterBase()
        {
            _cacheLoggers = new ConcurrentDictionary<string, ILog>();
        }


        /// <summary>
        /// 子类去实现这个接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected abstract ILog CreateLogger(string name);

        #region Implementation of ILoggerFactoryAdapter

        public ILog GetLogger(Type type)
        {
            //TODO:要对空的处理
            if (type != null)
            {
                return GetLogger(type.FullName);
            }
            else
            {
                return null;
            }
        }

        public ILog GetLogger(string name)
        {
            //TODO:要对空的处理
            if (!string.IsNullOrWhiteSpace(name))
            {
                return GetLoggerInternal(name);
            }
            else
            {
                return null;
            }

        }

        #endregion


        protected virtual ILog GetLoggerInternal(string name)
        {
            ILog log;
            if (_cacheLoggers.TryGetValue(name, out log))
            {
                return log;
            }
            log = CreateLogger(name);
            if (log == null)
            {
                throw new NotSupportedException(string.Format("创建名称为“{0}”的日志实例时“{1}”返回空实例。", name, GetType().FullName));
            }
            _cacheLoggers[name] = log;
            return log;
        }

    }
}
