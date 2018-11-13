using System;
using System.Collections.Generic;
using System.Linq;

namespace VarProject.FrameWork.Core.Pagination
{
    public static class PaginationUtil
    {
        public static Pagination<T> FetchPage<T>(this IQueryable<T> input, Func<IQueryable<T>, IQueryable<T>> whereAndOrderBy = null, int currentPageIndex = 0, int pageSize = 20)
            where T : class
        {
            return FetchPage(input, i => i, whereAndOrderBy, currentPageIndex, pageSize);
        }


        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="input"></param>
        /// <param name="selector"></param>
        /// <param name="whereAndOrderBy"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Pagination<TOut> FetchPage<T, TOut>(this IQueryable<T> input, Func<IQueryable<T>, IQueryable<TOut>> selector, Func<IQueryable<T>, IQueryable<T>> whereAndOrderBy = null, int currentPageIndex = 0, int pageSize = 20)
            where T : class
        {
            var temp = currentPageIndex;
            IQueryable<T> query = input;
            if (whereAndOrderBy != null)
                query = whereAndOrderBy(query);

            var totalCount = query.Count();
            int pageCount;
            if ((totalCount % pageSize) == 0)
                pageCount = totalCount / pageSize;
            else
                pageCount = (totalCount / pageSize) + 1;

            if (temp > pageCount - 1)
                return new Pagination<TOut>
                {
                    CurrentPageSize = 0,
                    Items = new List<TOut>(),
                    PageIndex = temp,
                    PageSize = pageSize,
                    TotalPageCount = pageCount,
                    TotalRecordCount = totalCount
                };
            if (temp <= 0)
                temp = 0;

            if (selector == null)
                throw new Exception("selector函数不得为空！");
            var query1 = selector(query);
            var items = query1.Skip(temp * pageSize).Take(pageSize).ToList();

            return new Pagination<TOut>
            {
                CurrentPageSize = items.Count,
                Items = items,
                PageIndex = temp,
                PageSize = pageSize,
                TotalPageCount = pageCount,
                TotalRecordCount = totalCount
            };
        }
    }
}
