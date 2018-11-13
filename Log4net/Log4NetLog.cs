using System;
using log4net.Core;
using VarProject.FrameWork.Core.Logging;

namespace VarProject.FrameWork.Core.Log4net
{
    public class Log4NetLog : LogBase
    {

        private static readonly Type DeclaringType = typeof(Log4NetLog);
        private readonly log4net.Core.ILogger _logger;

        protected override void Write(LogLevel level, object message, Exception exception, bool isData = false)
        {
            Level log4NetLevel = GetLevel(level);
            _logger.Log(DeclaringType, log4NetLevel, message, exception);
        }

        public override bool IsDataLogging
        {
            get { return false; }
        }


        public Log4NetLog(ILoggerWrapper wrapper)
        {
            _logger = wrapper.Logger;
        }

        public override bool IsTraceEnabled
        {
            get { return _logger.IsEnabledFor(Level.Trace); }
        }
        public override bool IsErrorEnabled { get { return _logger.IsEnabledFor(Level.Error); } }
        public override bool IsDebugEnabled { get { return _logger.IsEnabledFor(Level.Debug); } }
        public override bool IsInfoEnabled { get { return _logger.IsEnabledFor(Level.Info); } }
        public override bool IsWarnEnabled { get { return _logger.IsEnabledFor(Level.Warn); } }
        public override bool IsFatalEnabled { get { return _logger.IsEnabledFor(Level.Fatal); } }

        private static Level GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                case LogLevel.Off:
                    return Level.Off;
                default:
                    return Level.Off;
            }
        }
    }
}
