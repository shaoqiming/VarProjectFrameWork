using System.ComponentModel;

namespace VarProject.FrameWork.Core.Enums
{        /// <summary>
         /// 用户登录状态
         /// </summary>
    [Description("登录状态")]
    public enum LoginStatus
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        OK = 0,

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        NotFound = 1,

        /// <summary>
        /// 账户未激活
        /// </summary>
        [Description("账户未激活")]
        NotActivated = 2,

        /// <summary>
        /// 账户被封禁
        /// </summary>
        [Description("账户被封禁")]
        Banned = 3,

        /// <summary>
        /// 账号未注册
        /// </summary>
        [Description("账号未注册")]
        NotRegister = 4,


        /// <summary>
        /// 账号未注册
        /// </summary>
        [Description("token过期")]
        TokenExpirationTime = 5,


        /// <summary>
        /// 账号未注册
        /// </summary>
        [Description("Refreshtoken过期")]
        RefreshTokenExpirationTime = 6,

        /// <summary>
        /// 其它错误
        /// </summary>
        [Description("其它错误")]
        OtherError = 100

    }
}
