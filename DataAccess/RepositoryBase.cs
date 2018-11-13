using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VarProject.FrameWork.Core.Api;

namespace VarProject.FrameWork.Core.DataAccess
{
    public abstract class RepositoryBase<T> : IDisposable, IRepository<T> where T : class
    {
        protected readonly IDbSet<T> dbset;

        private UnitOfData _fDataContext;

        public List<SqlTrans> SqlTransList;


        protected RepositoryBase(UnitOfData DbContext)
        {
            this._fDataContext = DbContext;
            this.dbset = this.DataContext.Set<T>();
        }


        public DbContext DataContext
        {
            get { return this._fDataContext; }
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.DataContext != null)
            {
                this._fDataContext.Dispose();
            }
        }

        public virtual void Add(T entity)
        {
            this.dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            this.dbset.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            this.dbset.Attach(entity);
            this.DataContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual ICollection<T> GetMany(Func<T, bool> where)
        {
            return this.dbset.Where<T>(where).ToList();
        }


        public virtual IQueryable<T> QueryMany(Expression<Func<T,bool>> where)
        {
            return this.dbset.Where<T>(where);
        }
    }
}
