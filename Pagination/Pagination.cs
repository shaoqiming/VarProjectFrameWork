using System.Collections.Generic;

namespace VarProject.FrameWork.Core.Pagination
{
    public class Pagination<T>
    {
        public Pagination()
        {
            Items = new List<T>();
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页面索引号（从0开始）
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 当前页面数
        /// </summary>
        public int CurrentPageSize { get; set; }

        /// <summary>
        /// 元素列表
        /// </summary>
        public List<T> Items { get; set; }
    }
}
