using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Xml;

namespace Dit.Framework.Data.MsSqlMulti
{
    internal class ConnectorBase : IConnector
    {
        public static readonly int DEFAULT_COMMAND_TIMEOUT = 30;    // 단위 (초)

        // 프로퍼티
        public int CommandTimeout { get; set; }
        public SqlConnection Connection { get; set; }

        // 생성자
        public ConnectorBase(string name, string connString, int commandTimeout)
        {
            this.Name = name;
            this.ConnectionString = connString;
            this.Connection = new SqlConnection(connString);
            this.CommandTimeout = commandTimeout;
        }
        public ConnectorBase(string name, string connString) : this(name, connString, DEFAULT_COMMAND_TIMEOUT) { }

        // 프로퍼티 [ IConnector 인터페이스 구현 ]
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        // 메서드 [ IDisposable 인터페이스 구현 ]
        public void Dispose()
        {
            if (Connection != null) Connection.Dispose();
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public DataSet Fill(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            SqlConnection conn = this.Connection;

            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                sqlAdapter.SelectCommand.CommandType = command;
                sqlAdapter.SelectCommand.CommandTimeout = 600;

                if (dataSet == null)
                    dataSet = new DataSet();

                if (sqlParam != null)
                    sqlAdapter.SelectCommand.Parameters.AddRange(sqlParam);

                if (alias != null)
                {
                    sqlAdapter.Fill(dataSet, alias);
                }
                else
                {
                    if (dataTableMappings != null)
                        sqlAdapter.TableMappings.AddRange(dataTableMappings);

                    sqlAdapter.Fill(dataSet);
                }

                sqlAdapter.SelectCommand.Parameters.Clear();
            }
            return dataSet;
        }
        public int ExecuteNonQuery(string query, SqlParameter[] sqlParam, CommandType command)
        {
            SqlConnection conn = this.Connection;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = command;
            cmd.CommandTimeout = CommandTimeout;

            if (sqlParam != null)
                cmd.Parameters.AddRange(sqlParam);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            var result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return result;
        }
        public object ExecuteScalar(string query, SqlParameter[] sqlParam, CommandType command)
        {
            SqlConnection conn = this.Connection;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = command;
            cmd.CommandTimeout = CommandTimeout;

            if (sqlParam != null)
                cmd.Parameters.AddRange(sqlParam);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            var result = cmd.ExecuteScalar();
            cmd.Parameters.Clear();

            return result;
        }
        public bool TryBulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            SqlConnection conn = this.Connection;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, copyOptions, null))
            {
                bulkCopy.DestinationTableName = destTableName;
                bulkCopy.BulkCopyTimeout = timeout;

                try
                {
                    bulkCopy.WriteToServer(data);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    data = null;
                }
            }
        }
        public void BulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            SqlConnection conn = this.Connection;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, copyOptions, null))
            {
                bulkCopy.DestinationTableName = destTableName;
                bulkCopy.BulkCopyTimeout = timeout;
                bulkCopy.WriteToServer(data);
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

        // 메서드 [ 임시 ]
        public SqlDataReader ExecuteReader(string query, SqlParameter[] sqlParam, CommandType command)
        {
            SqlConnection conn = this.Connection;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = command;
            cmd.CommandTimeout = CommandTimeout;

            if (sqlParam != null)
                cmd.Parameters.AddRange(sqlParam);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();

            return result;
        }
        public XmlDocument ExecuteXmlDocument(string commandText, CommandType command, SqlParameter[] sqlParameters)
        {
            XmlDocument xd = null;
            xd = new XmlDocument();
            xd.Load(ExecuteXmlReader(commandText, sqlParameters, command));
            return xd;
        }
        public XmlReader ExecuteXmlReader(string query, SqlParameter[] sqlParam, CommandType command)
        {
            SqlConnection conn = this.Connection;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = command;
            cmd.CommandTimeout = CommandTimeout;

            if (sqlParam != null)
                cmd.Parameters.AddRange(sqlParam);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            var result = cmd.ExecuteXmlReader();
            cmd.Parameters.Clear();

            return result;
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public IEnumerable<IConnector> GetSubConnector()
        {
            yield return this;
        }
    }
}
