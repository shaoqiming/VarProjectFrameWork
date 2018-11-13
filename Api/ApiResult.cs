using VarProject.FrameWork.Core.Enums;

namespace VarProject.FrameWork.Core.Api
{
    public class ApiResult
    {
        #region 属性
        /// <summary>
        /// 返回值的消息体
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public int Ret { get; set; }
        /// <summary>
        /// 接口错误描述
        /// </summary>
        public string Msg { get; set; }

        #endregion

        public ApiResult()
        {
            this.Ret = (int)ApiReturnCode.Success;
        }

        public ApiResult(object data)
        {
            this.Ret = (int)ApiReturnCode.Success;
            this.Data = data;
        }

        public ApiResult(ApiReturnCode code, string msg)
        {
            this.Ret = (int)code;
            this.Msg = msg;
        }

        public ApiResult(int ret, string msg)
        {
            this.Ret = ret;
            this.Msg = msg;
        }

        /// <summary>
        /// 设置返回错误代码和错误信息
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ApiResult Return(int ret, string msg)
        {
            this.Ret = ret;
            this.Msg = msg;
            return this;
        }
    }
}
