using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Threading;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class ConnectorWithSub : IConnector
    {
        // 프로퍼티
        private IConnector Master { get; set; }
        private List<IConnector> Slaves { get; set; }

        // 생성자
        public ConnectorWithSub(IConnector master, List<IConnector> slaves)
        {
            Master = master;
            Slaves = slaves;

            Name = master.Name;
            ConnectionString = master.ConnectionString;
        }
        public ConnectorWithSub() : this(null, null) { }

        // 프로퍼티 [ IConnector 인터페이스 구현 ]
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        // 메서드 [ IDisposable 인터페이스 구현 ]
        public void Dispose()
        {
            if (Master != null) Master.Dispose();

            if (Slaves != null)
                foreach (var slave in Slaves)
                    slave.Dispose();
        }

        // 메서드 [ IConnector 인터페이스 구현 ]
        public DataSet Fill(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            return Master.Fill(query, alias, dataSet, sqlParam, command, dataTableMappings);
        }
        public int ExecuteNonQuery(string query, SqlParameter[] sqlParam, CommandType command)
        {
            return Master.ExecuteNonQuery(query, sqlParam, command);
        }
        public object ExecuteScalar(string query, SqlParameter[] sqlParam, CommandType command)
        {
            return Master.ExecuteScalar(query, sqlParam, command);
        }
        public bool TryBulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            return Master.TryBulkCopy(destTableName, data, copyOptions, timeout);
        }
        public void BulkCopy(string destTableName, DataTable data, SqlBulkCopyOptions copyOptions, int timeout)
        {
            Master.BulkCopy(destTableName, data, copyOptions, timeout);
        }

        // 메서드 [ IConnector 인터페이스 구현 2 ]
        public DataSet FillEach(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            if (Slaves == null || Slaves.Count <= 0)
                return Master.FillEach(query, alias, dataSet, sqlParam, command, dataTableMappings);

            // 지역 변수 초기화
            List<DataSet> dsList = new List<DataSet>();
            List<UserSqlException> useList = new List<UserSqlException>();

            // 쓰레드 메서드 구성
            Action<object> fill = (o) =>
            {
                IConnector ic = o as IConnector;
                if (ic == null) return;

                try
                {
                    dsList.Add(ic.Fill(query, alias, dataSet, CreateClone(sqlParam), command, dataTableMappings));
                }
                catch (SqlException ex)
                {
                    useList.Add(new UserSqlException(ic.Name, ex));
                }
            };

            // 쓰레드 처리
            List<Thread> tList = new List<Thread>();
            List<IConnector> cList = new List<IConnector>();

            cList.Add(Master);
            cList.AddRange(Slaves);

            foreach (IConnector ic in cList)
            {
                Thread t = new Thread((o) => fill(o));
                t.Start(ic);

                tList.Add(t);
            }

            // 쓰레드 처리가 완료될 때까지 대기
            foreach (var t in tList)
                t.Join();

            // 결과값(DataSet) 병합
            var result = dsList.Aggregate((DataSet)null, (prev, next) =>
            {
                if (prev != null && next != null)
                {
                    prev.Merge(next);
                    return prev;
                }
                else
                {
                    return prev ?? next;
                }
            });

            if (useList.Count > 0)
                throw new SeriesOfUserSqlException { InnerException = useList };

            return result;
        }
        public DataSet FillAny(string query, string alias, DataSet dataSet, SqlParameter[] sqlParam, CommandType command, DataTableMapping[] dataTableMappings)
        {
            if (Slaves == null || Slaves.Count <= 0)
                return Master.FillAny(query, alias, dataSet, sqlParam, command, dataTableMappings);

            // 지역 변수 초기화
            List<DataSet> dsList = new List<DataSet>();
            List<UserSqlException> useList = new List<UserSqlException>();

            // 쓰레드 메서드 구성
            Action<object> fill = (o) =>
            {
                IConnector ic = o as IConnector;
                if (ic == null) return;

                try
                {
                    dsList.Add(ic.Fill(query, alias, dataSet, CreateClone(sqlParam), command, dataTableMappings));
                }
                catch (SqlException ex)
                {
                    useList.Add(new UserSqlException(ic.Name, ex));
                }
            };

            // 쓰레드 처리
            List<Thread> tList = new List<Thread>();
            List<IConnector> cList = new List<IConnector>();

            cList.Add(Master);
            cList.AddRange(Slaves);

            foreach (IConnector ic in cList)
            {
                Thread t = new Thread((o) => fill(o));
                t.Start(ic);

                tList.Add(t);
            }

            // 쓰레드 처리가 완료될 때까지 대기
            foreach (var t in tList)
                t.Join();

            // 결과값(DataSet)중 문제가 없는 1번째 결과를 선택
            var result = dsList.Where(f => f != null && f.Tables.Count > 0 && f.Tables[0].Rows.Count > 0).FirstOrDefault();

            // 에이전트 전체에서 모두 에러가 발생하면 오류를 리포팅
            if (useList.Count >= cList.Count)
                throw new SeriesOfUserSqlException { InnerException = useList };

            return result;
        }

        // 메서드 [ IConnector 인터페이스 구현 3 ]
        public IEnumerable<IConnector> GetSubConnector()
        {
            yield return Master;

            foreach (var s in Slaves)
                yield return s;
        }

        // 내부 메서드
        private SqlParameter[] CreateClone(SqlParameter[] items)
        {
            List<SqlParameter> result = new List<SqlParameter>();
            foreach (var p in items)
            {
                SqlParameter @new = new SqlParameter
                {
                    Size = p.Size,
                    SqlDbType = p.SqlDbType,
                    Value = p.Value,
                    ParameterName = p.ParameterName,
                    TypeName = p.TypeName,
                    IsNullable = p.IsNullable,
                };

                result.Add(@new);
            }

            return result.ToArray();
        }
    }
}
