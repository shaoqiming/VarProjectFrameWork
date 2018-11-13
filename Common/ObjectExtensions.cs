using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exin.FrameWork.Core.Common
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object o)
        {
            if (o == null)
            {
                return true;
            }
            if (o.Equals(DBNull.Value))
            {
                return true;
            }
            if (o is string)
            {
                return string.IsNullOrEmpty((string)o);
            }
            if ((o is Guid) || (o is Guid?))
            {
                if (o.ToString() == Guid.Empty.ToString())
                {
                    return true;
                }
            }
            else if (((o is DateTime) || (o is DateTime?)) && DateTime.MinValue.Equals(o))
            {
                return true;
            }
            return false;
        }


        //判断数组是否为空
        public static bool IsNotEmpty<T>(this T list) where T : ICollection
        {
            return ((list != null) && (list.Count > 0));
        }

    }
}
