using System;
using System.IO;
using log4net.Config;
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
        }


        protected override ILog CreateLogger(string name)
        {
            throw new NotImplementedException();
        }
    }
}
