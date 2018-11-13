using System.Data;
using System.Data.SqlClient;

namespace VarProject.FrameWork.Core.DataAccess
{
    public class SqlTrans
    {
        public CommandType CommandType { get; set; }

        public SqlParameter[] param { get; set; }

        public string sql { get; set; }
    }
}
