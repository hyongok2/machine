using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Dit.Framework.Data.MsSqlMulti
{
    public class DBAgentProxy
    {
        // 전역 변수 [ 테스트 ]
        public static Action<string> ReportQuery { get; set; }

        // 전역 변수 [ 예외 처리기 ]
        public static Action<string> ReportError { get; set; }
        public static System.Windows.Forms.Form Handler { get; set; }

        // 필드
        protected IConnector _connector;

        // 생성자
        public DBAgentProxy(IConnector connector)
        {
            _connector = connector;
        }

        // 메서드
        public DataSet Fill(string query, params SqlParameter[] sqlParam)
        {
            try
            {
                return _connector.Fill(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            return null;
        }
        public DataSet FillEach(string query, params SqlParameter[] sqlParam)
        {
            try
            {
                return _connector.FillEach(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SeriesOfUserSqlException ex)
            {
                foreach (var e in ex.InnerException)
                    DoReportError(query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            return null;
        }
        public DataSet FillAny(string query, params SqlParameter[] sqlParam)
        {
            try
            {
                return _connector.FillAny(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SeriesOfUserSqlException ex)
            {
                foreach (var e in ex.InnerException)
                    DoReportError(query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            return null;
        }

        public DataTable FillTable(string query, params SqlParameter[] sqlParam)
        {
            DataSet result = null;

            try
            {
                result = _connector.Fill(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            if (result == null)
                return null;

            if (result.Tables.Count <= 0)
                return null;

            return result.Tables[0];
        }
        public DataTable FillTableEach(string query, params SqlParameter[] sqlParam)
        {
            DataSet result = null;

            try
            {
                result = _connector.FillEach(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            if (result == null)
                return null;

            if (result.Tables.Count <= 0)
                return null;

            return result.Tables[0];
        }
        public DataTable FillTableAny(string query, params SqlParameter[] sqlParam)
        {
            DataSet result = null;

            try
            {
                result = _connector.FillAny(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            if (result == null)
                return null;

            if (result.Tables.Count <= 0)
                return null;

            return result.Tables[0];
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] sqlParam)
        {
            int result = 0;

            try
            {
                result = _connector.ExecuteNonQuery(query, sqlParam, CommandType.Text);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            return result;
        }
        public object ExecuteScalar(string query, params SqlParameter[] sqlParam)
        {
            try
            {
                return _connector.ExecuteScalar(query, sqlParam, CommandType.Text);
            }
            catch (SqlException e)
            {
                DoReportError(_connector.Name, query, e);
            }
            finally
            {
                DoReportQuery(query, sqlParam);
            }

            return null;
        }

        // 메서드 ( 추가 사양 )
        public bool ExistTable(string tableName)
        {
            string query = @"
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID( @TABLE ) AND type in (N'U'))
SELECT 1
ELSE 
SELECT 0
";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter { ParameterName = "@TABLE", Value = tableName, SqlDbType = SqlDbType.NVarChar, });

            object interim = this.ExecuteScalar(query, param.ToArray());
            if (interim == null)
                return false;

            bool result = (interim as int? ?? 0) > 0;

            return result;
        }
        public bool ExistView(string viewName)
        {
            string query = @"
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID( @VIEW ) AND type in (N'V'))
SELECT 1
ELSE 
SELECT 0
";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter { ParameterName = "@VIEW", Value = viewName, SqlDbType = SqlDbType.NVarChar, });

            object interim = this.ExecuteScalar(query, param.ToArray());
            if (interim == null)
                return false;

            bool result = (interim as int? ?? 0) > 0;

            return result;
        }
        public bool ExistStoredProcedure(string procName)
        {
            string query = @"
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID( @PROC ) AND type in (N'P', N'PC'))
SELECT 1
ELSE 
SELECT 0
";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter { ParameterName = "@PROC", Value = procName, SqlDbType = SqlDbType.NVarChar, });

            object interim = this.ExecuteScalar(query, param.ToArray());
            if (interim == null)
                return false;

            bool result = (interim as int? ?? 0) > 0;

            return result;
        }
        public bool ExistUserDefinedTable(string typeName)
        {
            string query = @"
IF EXISTS 
(
	SELECT * 
	FROM sys.Types 
	WHERE name = @TYPENAME 
		AND is_table_type = 1 
		AND is_user_defined = 1
)
SELECT 1
ELSE 
SELECT 0
";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter { ParameterName = "@TYPENAME", Value = typeName, SqlDbType = SqlDbType.NVarChar, });

            object interim = this.ExecuteScalar(query, param.ToArray());
            if (interim == null)
                return false;

            bool result = (interim as int? ?? 0) > 0;

            return result;
        }
        public bool ExistColumn(string tableName, string colName, string colType)
        {
            string query = @"
IF EXISTS 
(
	SELECT * FROM sys.Columns 
	WHERE object_id = OBJECT_ID( @TableName )
		AND system_type_id = TYPE_ID( @DataType )
		AND name in ( @ColumnName )
)
SELECT 1
ELSE 
SELECT 0
";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter { ParameterName = "@TableName", Value = tableName, SqlDbType = SqlDbType.NVarChar, });
            param.Add(new SqlParameter { ParameterName = "@DataType", Value = colType, SqlDbType = SqlDbType.NVarChar, });
            param.Add(new SqlParameter { ParameterName = "@ColumnName", Value = colName, SqlDbType = SqlDbType.NVarChar, });

            object interim = this.ExecuteScalar(query, param.ToArray());
            if (interim == null)
                return false;

            bool result = (interim as int? ?? 0) > 0;

            return result;
        }

        // 메서드 ( 추가 사양 )
        public bool TryFillTable(out DataTable table, string query, params SqlParameter[] sqlParam)
        {
            DataSet result = null;
            table = null;

            try
            {
                result = _connector.Fill(query, null, null, sqlParam, CommandType.Text, null);
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

            if (result == null)
                return false;

            if (result.Tables.Count <= 0)
                return false;

            table = result.Tables[0];
            return true;
        }
        public bool TryExecuteNonQuery(out int rowCount, string query, params SqlParameter[] sqlParam)
        {
            rowCount = 0;

            try
            {
                rowCount = _connector.ExecuteNonQuery(query, sqlParam, CommandType.Text);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // 내부 메서드
        private static void DoReportError(string name, string query, SqlException e)
        {
            if (ReportError == null) throw e;
            if (Handler == null) throw e;
            if (Handler.IsDisposed == true) throw e;

            string details = string.Format("{0} ({1})\r\n{2}\r\n", e.Message, name, query);
            Handler.Invoke(ReportError, details);
        }
        private static void DoReportError(string query, UserSqlException ex)
        {
            if (ReportError == null) throw ex.InnerException;
            if (Handler == null) throw ex.InnerException;
            if (Handler.IsDisposed == true) throw ex.InnerException;

            string details = string.Format("{0} ({1})\r\n{2}\r\n", ex.InnerException.Message, ex.DatabaseName, query);
            Handler.Invoke(ReportError, details);
        }
        private static void DoReportQuery(string query, SqlParameter[] parameters)
        {
            if (ReportQuery == null) return;
            if (Handler == null) return;
            if (Handler.IsDisposed == true) return;

            if (parameters != null && parameters.Length > 0)
            {
                string parametersText = parameters.Select(f => string.Format("DECLARE {0} {1}({2}) = '{3}';", f.ParameterName, f.SqlDbType, f.Size, f.Value))
                    .Aggregate((f, g) => f + Environment.NewLine + g);

                Handler.Invoke(ReportQuery, parametersText + Environment.NewLine + query);
            }
            else
            {
                Handler.Invoke(ReportQuery, query);
            }
        }

        //
        public IEnumerable<IConnector> GetSubConnector()
        {
            return _connector.GetSubConnector();
        }
    }
}
