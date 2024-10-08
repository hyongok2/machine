using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class UserSqlException : Exception
    {
        // 프로퍼티
        public string DatabaseName { get; set; }
        public new SqlException InnerException { get; set; }

        public UserSqlException(string name, SqlException innerException)
        {
            DatabaseName = name;
            InnerException = innerException;
        }
    }
}
