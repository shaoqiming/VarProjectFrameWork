using System;

namespace VarProject.FrameWork.Core.Logging
{
    /// <summary>
    /// 定义日志行为
    /// </summary>
    public interface ILogger
    {

        #region Trace
        /// <summary>
        /// 跟踪级别的日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Trace<T>(T message);

        /// <summary>
        /// 写入跟踪级别的日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Trace(string format, params object[] args);

        #endregion

        #region Debug
        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Debug<T>(T message);

        /// <summary>
        /// 写入Debug级别的日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Debug(string format, params object[] args);
        #endregion

        #region Info
        /// <summary>
        /// 写入信息日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="isData">是否数据日志</param>
        void Info<T>(T message, bool isData);

        /// <summary>
        /// 写入信息格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Info(string format, params object[] args);
        #endregion

        #region warn
        /// <summary>
        /// 写入警告日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Warn<T>(T message);

        /// <summary>
        /// 写入警告格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Warn(string format, params object[] args);
        #endregion

        #region Error
        /// <summary>
        /// 写入错误日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Error<T>(T message);

        /// <summary>
        /// 写入错误格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, params object[] args);

        /// <summary>
        /// 写入错误日志消息，并记录异常
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        void Error<T>(T message, Exception exception);

        /// <summary>
        /// 写入<see cref="LogLevel.Error"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, Exception exception, params object[] args);

        #endregion

        #region Fatal
        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Fatal<T>(T message);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, params object[] args);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>日志消息，并记录异常
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常</param>
        void Fatal<T>(T message, Exception exception);

        /// <summary>
        /// 写入<see cref="LogLevel.Fatal"/>格式化日志消息，并记录异常
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="exception">异常</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, Exception exception, params object[] args);
        #endregion
    }
}
