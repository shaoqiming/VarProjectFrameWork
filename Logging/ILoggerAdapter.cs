using System;

namespace VarProject.FrameWork.Core.Logging
{
    /// <summary>
    /// 定义日志输出适配器的方法
    /// </summary>
    public interface ILoggerAdapter
    {
        ILog GetLogger(Type type);
        ILog GetLogger(string name);
    }
}
