using System.Security.Cryptography;
using System.Text;

namespace VarProject.FrameWork.Core.Utils
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public static class EnCodeUtil
    {

        /// <summary>
        /// MD5 不加盐
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="bit">16 或者32位的返回</param>
        /// <returns></returns>
        public static string MD5(string str, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(str));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
            {
                return tmp.ToString().Substring(8, 16);
            }
            else
            if (bit == 32)
            {
                return tmp.ToString();//默认情况
            }
            else
            {
                return string.Empty;
            }
        }

        public static string MD5(string password)
        {
            return MD5(password, 16);
        }
    }
}
