using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using exin.FrameWork.Core.Common;
using VarProject.FrameWork.Core.Api;
using VarProject.FrameWork.Core.Utils;

namespace VarProject.FrameWork.Core.DataAccess
{
    public class UnitOfData : DbContext, IUnitOfData
    {
        /// <summary>
        /// 可以添加多个上下文 在事务中一起提交
        /// </summary>
        public List<IUnitOfData> UnitOfDatas;

        private bool fIsSubmit;

        private bool IsSaveChange;

        private string fUnitSign;

        public List<SqlTrans> SqlTransList;

        public UnitOfData(string nameOrConnString)
            : base(nameOrConnString)
        {
            SqlTransList = new List<SqlTrans>();
            UnitOfDatas = new List<IUnitOfData>();
        }

        protected string ConnString { get; set; }

        public int RegisterSqlCommand(string sql, params SqlParameter[] param)
        {
            SqlTrans item = new SqlTrans
            {
                sql = sql,
                param = param,
                CommandType = CommandType.Text
            };
            this.SqlTransList.Add(item);
            return ((param == null) ? 0 : param.Length);
        }

        public int RegisterStored(string storedName, params SqlParameter[] param)
        {
            SqlTrans item = new SqlTrans
            {
                sql = storedName,
                param = param,
                CommandType = CommandType.StoredProcedure
            };
            this.SqlTransList.Add(item);
            return param.Length;
        }


        public int Submit()
        {
            this.CheckSubmit();
            return this.AdoSubmit();
        }

        private void CheckSubmit()
        {
            if (this.fIsSubmit)
            {
                throw new Exception("注意 DbContext 只能被提交一次");
            }
            this.fIsSubmit = true;
        }


        public int EFSubmit()
        {
            return this.SaveChanges();
        }



        public int TransListSubmit()
        {
            int num = 0;
            if (!(this.SqlTransList.IsNotEmpty() || this.UnitOfDatas.IsNotEmpty()))
            {
                return num;
            }

            SqlConnection connection = base.Database.Connection as SqlConnection;
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlTransaction transaction = connection.BeginTransaction();

                //开启事务
                using (transaction)
                {
                    try
                    {
                        foreach (var unitOfData in UnitOfDatas)
                        {
                            num += unitOfData.Submit();
                        }

                        num += this.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        throw;
                    }

                }
            }

            return 0;
        }

        public int AdoSubmit()
        {
            int num = 0;
            if (!(this.SqlTransList.IsNotEmpty() || this.UnitOfDatas.IsNotEmpty()))
            {
                return num;
            }

            SqlConnection connection = base.Database.Connection as SqlConnection;
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlTransaction transaction = connection.BeginTransaction();

                //开启事务
                using (transaction)
                {
                    try
                    {
                        if (this.SqlTransList.Count > 0)
                        {
                            foreach (var trans in this.SqlTransList)
                            {
                                num += SqlHelper.ExecuteNonQuery(transaction, trans.CommandType, trans.sql,
                                    trans.param);
                            }
                        }

                        foreach (var unitOfData in UnitOfDatas)
                        {
                            num += unitOfData.Submit();
                        }

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        throw;
                    }

                }
            }

            return 0;
        }



        //public int EFSubmit()
        //{
        //    int num2 = 0;
        //    try
        //    {
        //        if ((this.SqlTransList.Count > 0) || (this.UnitOfDatas.Count > 0))
        //        {
        //            TransactionOptions transactionOptions = new TransactionOptions
        //            {
        //                IsolationLevel = IsolationLevel.ReadCommitted //事务传播属性 
        //            };
        //            using (TransactionScope scope = new TransactionScope())
        //            {
        //                int num = 0;
        //                foreach (SqlTrans trans in this.SqlTransList)
        //                {
        //                    num += this.ExecuteSqlCommand(trans.sql, trans.param);
        //                }
        //                foreach (IUnitOfData data in this.UnitOfDatas)
        //                {
        //                    num += data.Submit();
        //                }
        //                num += this.SaveChanges();
        //                scope.Complete();
        //                return num;
        //            }
        //        }
        //        num2 = this.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //    return num2;
        //}


        public virtual int ExecuteSqlCommand(string sql, params SqlParameter[] param)
        {
            return base.Database.ExecuteSqlCommand(sql, param);
        }


        public void Dispose()
        {
            base.Dispose();
        }





    }
}
