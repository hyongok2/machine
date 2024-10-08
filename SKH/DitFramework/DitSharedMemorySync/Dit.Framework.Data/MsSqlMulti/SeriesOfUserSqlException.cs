using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class SeriesOfUserSqlException : Exception
    {
        public new List<UserSqlException> InnerException { get; set; }

        public SeriesOfUserSqlException()
        {
            InnerException = new List<UserSqlException>();
        }
    }
}
