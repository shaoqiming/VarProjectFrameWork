using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VarProject.FrameWork.Core.Api
{
    public interface IRepository<T> where T: class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        /// <summary>
        /// 查询 返回List
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        ICollection<T> GetMany(Func<T, bool> where);

        /// <summary>
        /// 查询 返回IqueryList
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> QueryMany(Expression<Func<T, bool>> where);
    }
}
