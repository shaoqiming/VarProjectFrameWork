using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace VarProject.FrameWork.Core.Utils
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class GenUtil
    {
        /// <summary>
        /// 取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return str;
            }

            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan
            span = DateTime.Now - dt;
            if (span.TotalDays > 420)
            {
                return dt.GetDateTimeFormats('y')[0].ToString();
            }
            if (span.TotalDays > 360)
            {
                return "1年前";
            }
            if (span.TotalDays > 330)
            {
                return "11个月前";
            }
            if (span.TotalDays > 300)
            {
                return "10个月前";
            }
            if (span.TotalDays > 270)
            {
                return "9个月前";
            }
            if (span.TotalDays > 240)
            {
                return "8个月前";
            }
            if (span.TotalDays > 210)
            {
                return "7个月前";
            }
            if (span.TotalDays > 180)
            {
                return "6个月前";
            }
            if (span.TotalDays > 150)
            {
                return "5个月前";
            }
            if (span.TotalDays > 120)
            {
                return "4个月前";
            }
            if (span.TotalDays > 90)
            {
                return "3个月前";
            }
            if (span.TotalDays > 60)
            {
                return "2个月前";
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前",
                (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前",
                (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        /// <summary>
        /// 数量转换
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumChange(int Number)
        {
            var num = Number + string.Empty;
            if (Number < 10000)
            {
                return num;
            }
            num = (1.0 * Number / 10000) + "w";
            return num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Value"></param>
        public static Object stringToEnum(Type type, String Value)
        {
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var fi in fields)
            {
                if (((int)fi.GetValue(null)).ToString() == Value)
                {
                    var returdata = System.Enum.ToObject(type, int.Parse(Value));
                    return returdata;
                }
            }

            return null;
        }



        public class DescendingAlphabeticComparer : IComparer<string>
        {
            public static Regex SortRegex = new Regex(@"^(?<sort>\d+\.\d+)\s(\w|\W)+$");

            private double GetSort(string name)
            {
                MatchCollection matchs = SortRegex.Matches(name);
                foreach (Match match in matchs)
                {
                    GroupCollection groups = match.Groups;
                    Group g = groups["sort"];
                    if (g != null)
                    {
                        double sort = Convert.ToDouble(g.Value);
                        return sort;
                    }
                }
                return 100;
            }
            public int Compare(string x, string y)
            {
                double sortX = GetSort(x);

                System.Diagnostics.Debug.Write(x + "  -  " + y + "\r\n");

                double sortY = GetSort(y);
                if (sortX > 0 || sortY > 0)
                {
                    return -sortY.CompareTo(sortX);
                }
                return -y.CompareTo(x);
            }
        }
    }
}
