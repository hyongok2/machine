using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class MsSqlDBAgent : ICollection<IConnector>
    {
        // 필드 [ 정적 ]
        protected static object _syncRoot = new object();
        protected static MsSqlDBAgent _singleton;
        protected static readonly int DEFAULT_BULKCOPY_TIMEOUT = 30;      // 단위 (초)

        // 필드
        private List<IConnector> _conn;

        // 인덱서
        public IConnector this[int index]
        {
            get { return _conn.ElementAtOrDefault(index); }
        }
        public IConnector this[string name]
        {
            get { return _conn.Where(f => f.Name == name).FirstOrDefault(); }
        }

        // 생성자
        private MsSqlDBAgent()
        {
            _conn = new List<IConnector>();
        }

        // 메서드 [ 정적 ]
        public static MsSqlDBAgent GetInstance()
        {
            if (_singleton == null)
                lock (_syncRoot)
                    if (_singleton == null)
                        _singleton = new MsSqlDBAgent();

            return _singleton;
        }

        // 메서드
        public bool Contains(string name)
        {
            return _conn.Where(f => f.Name == name).Count() > 0;
        }
        public bool TryGetFromName(string name, out IConnector result)
        {
            try
            {
                result = this[name];
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
        public bool TryGetFromIndex(int index, out IConnector result)
        {
            try
            {
                result = this[index];
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        // 메서드
        public DataSet Fill(string connectionName, string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, System.Data.Common.DataTableMapping[] dataTableMappings)
        {
            var conn = _singleton[connectionName];
            if (conn == null) return null;

            return conn.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
        }
        public int ExecuteNonQuery(string connectionName, string query, SqlParameter[] sqlParam, CommandType command)
        {
            var conn = _singleton[connectionName];
            if (conn == null) return -1;

            return conn.ExecuteNonQuery(query, sqlParam, command);
        }
        public object ExecuteScalar(string connectionName, string query, SqlParameter[] sqlParam, CommandType command)
        {
            var conn = _singleton[connectionName];
            if (conn == null) return null;

            return conn.ExecuteScalar(query, sqlParam, command);
        }
        public bool TryBulkCopy(string connectionName, string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            var conn = _singleton[connectionName];
            if (conn == null) return false;

            return conn.TryBulkCopy(destTableName, data, copyOptions, timeout);
        }
        public void BulkCopy(string connectionName, string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            var conn = _singleton[connectionName];
            if (conn == null) throw new Exception("해당 데이터베이스 커넥션 항목이 없습니다.");

            conn.BulkCopy(destTableName, data, copyOptions, timeout);
        }

        // 메서드 [ 파생 ]
        public DataSet Fill(string connectionName, string query, string alias, DataSet dsDataSet, SqlParameter[] sqlParam, CommandType command)
        {
            return this.Fill(connectionName, query, alias, dsDataSet, sqlParam, command, null);
        }
        public DataSet Fill(string connectionName, string query, string alias, DataSet dsDataSet, SqlParameter[] sqlParam)
        {
            return this.Fill(connectionName, query, alias, dsDataSet, sqlParam, CommandType.Text, null);
        }
        public DataSet Fill(string connectionName, string query, string alias, DataSet dsDataSet)
        {
            return this.Fill(connectionName, query, alias, dsDataSet, null, CommandType.Text, null);
        }
        public DataSet Fill(string connectionName, string query, string alias)
        {
            return this.Fill(connectionName, query, alias, null, null, CommandType.Text, null);
        }
        public DataSet Fill(string connectionName, string query, System.Data.Common.DataTableMapping[] dataTableMappings)
        {
            return this.Fill(connectionName, query, null, null, null, CommandType.Text, dataTableMappings);
        }
        public DataSet Fill(string connectionName, string query)
        {
            return this.Fill(connectionName, query, null, null, null, CommandType.Text, null);
        }

        public int ExecuteNonQuery(string connectionName, string query, SqlParameter[] sqlParam)
        {
            return this.ExecuteNonQuery(connectionName, query, sqlParam, CommandType.Text);
        }
        public int ExecuteNonQuery(string connectionName, string query)
        {
            return this.ExecuteNonQuery(connectionName, query, null, CommandType.Text);
        }

        public object ExecuteScalar(string connectionName, string query, SqlParameter[] sqlParam)
        {
            return this.ExecuteScalar(connectionName, query, sqlParam, CommandType.Text);
        }
        public object ExecuteScalar(string connectionName, string query)
        {
            return this.ExecuteScalar(connectionName, query, null, CommandType.Text);
        }

        public bool TryBulkCopy(string connectionName, string destTableName, DataTable data, SqlBulkCopyOptions copyOptions)
        {
            return this.TryBulkCopy(connectionName, destTableName, data, copyOptions, DEFAULT_BULKCOPY_TIMEOUT);
        }
        public bool TryBulkCopy(string connectionName, string destTableName, DataTable data, int timeout)
        {
            return this.TryBulkCopy(connectionName, destTableName, data, SqlBulkCopyOptions.Default, timeout);
        }
        public bool TryBulkCopy(string connectionName, string destTableName, DataTable data)
        {
            return this.TryBulkCopy(connectionName, destTableName, data, SqlBulkCopyOptions.Default, DEFAULT_BULKCOPY_TIMEOUT);
        }

        public void BulkCopy(string connectionName, string destTableName, DataTable data, SqlBulkCopyOptions copyOptions)
        {
            this.BulkCopy(connectionName, destTableName, data, copyOptions, DEFAULT_BULKCOPY_TIMEOUT);
        }
        public void BulkCopy(string connectionName, string destTableName, DataTable data, int timeout)
        {
            this.BulkCopy(connectionName, destTableName, data, SqlBulkCopyOptions.Default, timeout);
        }
        public void BulkCopy(string connectionName, string destTableName, DataTable data)
        {
            this.BulkCopy(connectionName, destTableName, data, SqlBulkCopyOptions.Default, DEFAULT_BULKCOPY_TIMEOUT);
        }

        // 인터페이스 구현 [ ICollection<IConnector> ]
        public void Add(IConnector item)
        {
            if (_conn.Where(f => f.Name == item.Name).Count() > 0)
                throw new ArgumentException("중복된 커넥션 이름입니다.");

            _conn.Add(item);
        }
        public void Clear()
        {
            _conn.Clear();
        }
        public bool Contains(IConnector item)
        {
            return _conn.Contains(item);
        }
        public void CopyTo(IConnector[] array, int arrayIndex)
        {
            _conn.CopyTo(array, arrayIndex);
        }
        public int Count
        {
            get { return _conn.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(IConnector item)
        {
            return _conn.Remove(item);
        }
        public IEnumerator<IConnector> GetEnumerator()
        {
            return _conn.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _conn.GetEnumerator();
        }
    }
}

