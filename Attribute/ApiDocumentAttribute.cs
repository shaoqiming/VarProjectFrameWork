using System;
using System.Linq;

namespace VarProject.FrameWork.Core.Attribute
{
    public class ApiDocumentAttribute : System.Attribute
    {
        public ApiDocumentAttribute(double sort, Type parent = null, bool hidden = false)
        {
            this.Sort = sort;
            this.Hidden = hidden;
            this.Parent = parent;
        }

        public bool Hidden { get; set; }
        public Type Parent { get; set; }
        public double Sort { get; set; }
        public object Tag { get; set; }


        public static ApiDocumentAttribute GetAttribute(Type type)
        {
            ApiDocumentAttribute attribute = type.GetCustomAttributes(typeof(ApiDocumentAttribute), false).FirstOrDefault<object>() as ApiDocumentAttribute;
            if (attribute == null)
            {
                attribute = new ApiDocumentAttribute(0.0, null, false);
            }
            return attribute;
        }
    }
}
