using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Xml;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class ConnectorWithMulti : IConnector
    {
        public static readonly int DEFAULT_CONNECTION_COUNT = 2;
        public static readonly int DEFAULT_LOOP_TIMEOUT = 100;      // 단위 (밀리초)

        // 프로퍼티
        public int LoopTimeout { get; set; }
        private List<Semaphore> SemaphoreList { get; set; }
        private List<ConnectorBase> AgentList { get; set; }
        private int ConnectionCount { get; set; }

        // 생성자
        public ConnectorWithMulti(string name, string connString, int count, int commandTimeout, int loopTimeout)
        {
            this.Name = name;
            this.ConnectionString = connString;
            this.ConnectionCount = count;

            this.AgentList = new List<ConnectorBase>();
            this.SemaphoreList = new List<Semaphore>();
            this.LoopTimeout = loopTimeout;

            for (int i = 0; i < count; i++)
            {
                string name2 = string.Format("{0}.Multi{1}", name, i);

                this.AgentList.Add(new ConnectorBase(name2, connString, commandTimeout));
                this.SemaphoreList.Add(new Semaphore(1, 1));
            }
        }
        public ConnectorWithMulti(string name, string connString, int count, int commandTimeout)
            : this(name, connString, count, commandTimeout, ConnectorWithMulti.DEFAULT_LOOP_TIMEOUT) { }
        public ConnectorWithMulti(string name, string connString, int count)
            : this(name, connString, count, ConnectorBase.DEFAULT_COMMAND_TIMEOUT, DEFAULT_LOOP_TIMEOUT) { }
        public ConnectorWithMulti(string name, string connString)
            : this(name, connString, DEFAULT_CONNECTION_COUNT, ConnectorBase.DEFAULT_COMMAND_TIMEOUT, DEFAULT_LOOP_TIMEOUT) { }

        // 프러퍼티 [ IConnector 인터페이스 구현 ]
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        // 메서드 [ IDisposable 인터페이스 구현 ]
        public void Dispose()
        {
            if (AgentList != null)
                foreach (var agent in AgentList)
                    agent.Dispose();
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public DataSet Fill(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            if (AgentList == null) return null;
            if (AgentList.Count <= 0) return null;

            int index = -1;
            ConnectorBase agent = null;

            try
            {
                while (true)
                {
                    bool result = false;

                    for (int i = 0; i < this.AgentList.Count; i++)
                    {
                        agent = this.AgentList[i];
                        if (agent == null) continue;

                        result = SemaphoreList[i].WaitOne(LoopTimeout);
                        if (result)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (result) break;
                }

                if (index < 0) return null;

                return agent.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
            }
            finally
            {
                if (agent != null) this.SemaphoreList[index].Release();
            }
        }
        public int ExecuteNonQuery(string query, SqlParameter[] sqlParam, CommandType command)
        {
            if (AgentList == null) return -1;
            if (AgentList.Count <= 0) return -1;

            int index = -1;
            ConnectorBase agent = null;

            try
            {
                while (true)
                {
                    bool result = false;

                    for (int i = 0; i < this.AgentList.Count; i++)
                    {
                        agent = this.AgentList[i];
                        if (agent == null) continue;

                        result = SemaphoreList[i].WaitOne(LoopTimeout);
                        if (result)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (result) break;
                }

                if (index < 0) return -1;

                return agent.ExecuteNonQuery(query, sqlParam, command);
            }
            finally
            {
                if (agent != null) this.SemaphoreList[index].Release();
            }
        }
        public object ExecuteScalar(string query, SqlParameter[] sqlParam, CommandType command)
        {
            if (AgentList == null) return null;
            if (AgentList.Count <= 0) return null;

            int index = -1;
            ConnectorBase agent = null;

            try
            {
                while (true)
                {
                    bool result = false;

                    for (int i = 0; i < this.AgentList.Count; i++)
                    {
                        agent = this.AgentList[i];
                        if (agent == null) continue;

                        result = SemaphoreList[i].WaitOne(LoopTimeout);
                        if (result)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (result) break;
                }

                if (index < 0) return null;

                return agent.ExecuteScalar(query, sqlParam, command);
            }
            finally
            {
                if (agent != null) this.SemaphoreList[index].Release();
            }
        }
        public bool TryBulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            if (AgentList == null) return false;
            if (AgentList.Count <= 0) return false;

            int index = -1;
            ConnectorBase agent = null;

            try
            {
                while (true)
                {
                    bool result = false;

                    for (int i = 0; i < this.AgentList.Count; i++)
                    {
                        agent = this.AgentList[i];
                        if (agent == null) continue;

                        result = SemaphoreList[i].WaitOne(LoopTimeout);
                        if (result)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (result) break;
                }

                if (index < 0) return false;

                return agent.TryBulkCopy(destTableName, data, copyOptions, timeout);
            }
            finally
            {
                if (agent != null) this.SemaphoreList[index].Release();
            }
        }
        public void BulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            if (AgentList == null) throw new Exception("데이터베이스 커넥션 목록이 초기화되지 않았습니다.");
            if (AgentList.Count <= 0) throw new Exception("데이터베이스 커넥션 목록에 참조할 항목이 없습니다.");

            int index = -1;
            ConnectorBase agent = null;

            try
            {
                while (true)
                {
                    bool result = false;

                    for (int i = 0; i < this.AgentList.Count; i++)
                    {
                        agent = this.AgentList[i];
                        if (agent == null) continue;

                        result = SemaphoreList[i].WaitOne(LoopTimeout);
                        if (result)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (result) break;
                }

                if (index < 0) throw new Exception("해당 데이터베이스 커넥션 항목이 없습니다.");

                agent.BulkCopy(destTableName, data, copyOptions, timeout);
            }
            finally
            {
                if (agent != null) this.SemaphoreList[index].Release();
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
            if (AgentList.Count <= 0)
                yield break;

            yield return AgentList.First();
        }
    }
}
