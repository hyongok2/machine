using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System.Data.Common;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class ConnectorWithSingle : IConnector
    {
        protected static readonly int DEFAULT_BULKCOPY_TIMEOUT = 30;   // 단위 (초)

        // 프로퍼티
        private Semaphore Semaphore { get; set; }
        private ConnectorBase Agent { get; set; }

        // 생성자
        public ConnectorWithSingle(string name, string connString, int commandTimeout)
        {
            this.Name = name;
            this.ConnectionString = connString;

            this.Semaphore = new Semaphore(1, 1);
            this.Agent = new ConnectorBase(name + ".Single", connString, commandTimeout);
        }
        public ConnectorWithSingle(string name, string connString) : this(name, connString, ConnectorBase.DEFAULT_COMMAND_TIMEOUT) { }

        // 프로퍼티 [ IConnector 인터페이스 구현 ]
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        // 메서드 [ IDisposable 인터페이스 구현 ]
        public void Dispose()
        {
            if (Agent != null) Agent.Dispose();
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public DataSet Fill(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            try
            {
                this.Semaphore.WaitOne();
                return this.Agent.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
            }
            finally
            {
                this.Semaphore.Release();
            }
        }
        public int ExecuteNonQuery(string query, SqlParameter[] sqlParam, CommandType command)
        {
            try
            {
                this.Semaphore.WaitOne();
                return this.Agent.ExecuteNonQuery(query, sqlParam, command);
            }
            finally
            {
                this.Semaphore.Release();
            }
        }
        public object ExecuteScalar(string query, SqlParameter[] sqlParam, CommandType command)
        {
            try
            {
                this.Semaphore.WaitOne();
                return this.Agent.ExecuteScalar(query, sqlParam, command);
            }
            finally
            {
                this.Semaphore.Release();
            }
        }
        public bool TryBulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            try
            {
                this.Semaphore.WaitOne();
                return this.Agent.TryBulkCopy(destTableName, data, copyOptions, timeout);
            }
            finally
            {
                this.Semaphore.Release();
            }
        }
        public void BulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            try
            {
                this.Semaphore.WaitOne();
                this.Agent.BulkCopy(destTableName, data, copyOptions, timeout);
            }
            finally
            {
                this.Semaphore.Release();
            }
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public DataSet FillEach(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            return this.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
        }
        public DataSet FillAny(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            return this.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public IEnumerable<IConnector> GetSubConnector()
        {
            yield return Agent;
        }
    }
}
