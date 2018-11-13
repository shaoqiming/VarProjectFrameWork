using System.Data.Entity;

namespace VarProject.FrameWork.Core.DataAccess
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        DbContext unitofdata = null;
        protected readonly DbSet<T> dbSet;



        public DbContext DataContext
        {
            get
            {
                return unitofdata;
            }
        }

        public DbSet<T> Dbset
        {
            get
            {
                return this.dbSet;
            }
        }


        public Repository(UnitOfData DbContext)
            : base(DbContext)
        {
        }





    }
}
