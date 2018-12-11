using System;
using System.IO;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using VarProject.FrameWork.Core.Logging;

namespace VarProject.FrameWork.Core.Log4net
{
    public class Log4NetLoggerAdapter : LoggerAdapterBase
    {

        /// <summary>
        /// 初始化日记
        /// </summary>
        public Log4NetLoggerAdapter()
        {
            const string fileName = "log4net.config";
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(configFile))
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                return;
            }
            else
            {
                RollingFileAppender appender = new RollingFileAppender
                {
                    Name = "root",
                    File = "logs\\log_",
                    AppendToFile = true,
                    LockingModel = new FileAppender.MinimalLock(),
                    RollingStyle = RollingFileAppender.RollingMode.Date,
                    DatePattern = "yyyyMMdd-HH\".log\"",
                    StaticLogFileName = false,
                    Threshold = Level.Debug,
                    MaxSizeRollBackups = 10,
                    Layout = new PatternLayout("%n[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c %t %w %n%m%n")
                };
                appender.ClearFilters();
                appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Info });
                BasicConfigurator.Configure(appender);
                appender.ActivateOptions();
            }
        }


        protected override ILog CreateLogger(string name)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(name);
            return new Log4NetLog(log);
        }
    }
}
