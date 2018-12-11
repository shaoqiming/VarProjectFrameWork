
using System.ComponentModel;

namespace VarProject.FrameWork.Core.Enums
{
    /// <summary>
    /// 返回代码
    /// </summary>
    public enum ApiReturnCode
    {
        [Description("成功")]
        Success = 1,

        [Description("普通错误")]
        NormalError = -1,

        [Description("Token无效")]
        TokenInvalid = -5,

        [Description("未登录")]
        NotLogin = -6,

        [Description("Token过期")]
        TokenExpire = -9,

        [Description("登录失败")]
        SignInvalid = -8
    }
}
