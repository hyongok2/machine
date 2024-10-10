using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Dit.Framework.Data.MsSqlMulti
{
    public interface IConnector : IDisposable
    {
        // 프로퍼티
        string Name { get; set; }
        string ConnectionString { get; set; }

        // 메서드
        DataSet Fill(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings);
        int ExecuteNonQuery(string query, SqlParameter[] sqlParam, CommandType command);
        object ExecuteScalar(string query, SqlParameter[] sqlParam, CommandType command);
        bool TryBulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout);
        void BulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout);

        // 메서드 추가 [ 130802 ]
        DataSet FillEach(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings);
        DataSet FillAny(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings);

        // 메서드 추가
        IEnumerable<IConnector> GetSubConnector();
    }
}
