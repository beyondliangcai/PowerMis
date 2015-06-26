//************************************************************************************
//* COPYRIGHT 2008 SJZU. ALL RIGHTS RESERVED
//*     College of Information and Engineering
//* Class Information
//*	FileName        : SQLUtl.cs
//*	Description     : 数据库操作的工具类，为底层数据操作提供方法和函数
//*	Function List   :  
//*			1、ColumnExists()       : 判断某表的某个字段是否存在
//*			2、TableExists()        : 判断表在数据库中是否存在
//*			3、GetTableMaxID()      : 取得表的当前最大自增ID号(当前最大ID号+1)
//*			4、ExecuteSql()         : A 执行SQL语句，返回影响的记录数
//*			                          B 执行带有参数的SQL语句
//*			5、Query()              : A 执行查询语句，返回DataSet
//*                                   B 执行带有参数的查询语句，返回DataSet
//*			6、ExecuteNonQuery()    : 用一个已经存在的数据库连接执行一个SQL命令
//*			7、RunProcedure()       : 执行一个存储过程，并且存储过程返回一个结果集
//*			8、ExecuteSqlTran()     : 执行多条SQL语句，实现数据库事务
//*			9、MakeInParam()        : 构造存储过程的一个输入参数
//*			10、MakeOutParam()      : 构造存储过程的一个输出参数
//*			11、MakeParam()         : 构造存储过程的参数
//*			12、GetSingle()         : 执行一条计算查询结果语句，返回查询结果（object）
//*			13、BuildQueryCommand() : 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
//*			14、PrepareCommand()    : 为SQLCOMMAND对象赋值
//************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Common;
using BusinessModel;

namespace DBUtility
{
    public class SQLUtl
    {
        // 数据库连接字符串         
       // public static readonly string _connectionString = "server =192.168.1.106;uid=sa; pwd=sa;database=POWERMIS";
        public static readonly string _connectionString = "server ='" + Constant.DB.ServerIP + "';uid='" + Constant.DB.LoginID + "'; pwd='" + Constant.DB.Password + "';database='" + Constant.DB.DataBaseName + "'";

        static SQLUtl() 
        {     }
         /// <summary>   
        /// 判断某表的某个字段是否存在   
        /// </summary>   
        /// <param name="tableName">表名称</param>   
        /// <param name="columnName">列名称</param>   
        /// <returns>
        ///     True: 存在该列 
        ///     False: 不存在该列
        /// </returns>   
        public static bool ColumnExists(string tableName, string columnName)
        {
            string strSql = "SELECT COUNT(1) "
                + "FROM SYSCOLUMNS "
                + "WHERE [id] = object_id('" + tableName + "') "
                    + "AND [name]='" + columnName + "'";
            try
            {
                object res = GetSingle(strSql);
                if (res == null)
                {
                    return false;
                }
                return Convert.ToInt32(res) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>   
        /// 判断表在数据库中是否存在   
        /// </summary>   
        /// <param name="tableName">表名</param>   
        /// <returns>true: 存在该表 false: 不存在该表</returns>   
        public static bool TableExists(string tableName)
        {
            string strSql = "SELECT COUNT(*) "
                + "FROM sysobjects "
                + "WHERE id = object_id(N'[" + tableName + "]') "
                    + "AND OBJECTPROPERTY(id, N'IsUserTable') = 1";

            try
            {
                // SQL语句执行结果
                object obj = SQLUtl.GetSingle(strSql);

                // 对执行结果进行判断
                int iCmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    iCmdresult = 0;
                }
                else
                {
                    iCmdresult = int.Parse(obj.ToString());
                }

                // 返回函数值
                if (iCmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得表的当前最大自增ID号(当前最大ID号+1)
        /// </summary>
        /// <param name="fieldName">表的流水号字段名称</param>
        /// <param name="tableName">表名</param>
        /// <returns>当前最大自增ID号</returns>
        public static int GetTableMaxID(string fieldName, string tableName)
        {
            string strSql = "SELECT MAX(" + fieldName + ") + 1 FROM " + tableName;
            try
            {
                object obj = SQLUtl.GetSingle(strSql);
                if (obj == null)
                {
                    return 1;
                }
                else
                {
                    return int.Parse(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>   
        /// 执行SQL语句，返回影响的记录数   
        /// </summary>   
        /// <param name="SQLString">SQL语句</param>   
        /// <returns>
        ///     0:SQL语句执行影响的行数为0
        ///     大于0:SQL语句执行成功
        ///     小于0:SQL语句执行失败
        /// </returns>   
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection))
                {
                    try
                    {
                        // 打开连接并执行查询
                        sqlConnection.Open();
                        int iRows = sqlCommand.ExecuteNonQuery();

                        // 返回函数值
                        return iRows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        // 关闭数据库连接，释放资源
                        sqlCommand.Dispose();
                        if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行带有参数的SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="cmdParms">SQL语句的参数</param>
        /// <returns>
        ///     0:SQL语句执行影响的行数为0
        ///     大于0:SQL语句执行成功
        ///     小于0:SQL语句执行失败
        /// </returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] commandParms)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    try
                    {
                        // 填充SQL命令对象
                        PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, SQLString, commandParms);
                        int iRows = sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();

                        // 返回函数值
                        return iRows;
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        // 释放资源
                        sqlCommand.Dispose();
                        if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>   
        /// 执行查询语句，返回DataSet   
        /// </summary>   
        /// <param name="SQLString">查询语句</param>   
        /// <returns>DataSet数据集</returns>   
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SQLString, sqlConnection);
                    sqlDataAdapter.Fill(dataSet, "dataSet");
                    sqlDataAdapter.Dispose();       // 释放资源

                    return dataSet;
                } 
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行带有参数的查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">查询语句的参数</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] commandParms)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    // 生成SQLCOMMAND对象，并为其赋值
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, SQLString, commandParms);

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataSet, "dataSet");
                        sqlDataAdapter.Dispose();       // 释放资源
                        sqlCommand.Parameters.Clear();

                        return dataSet;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源                    
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public static int GetCountQuery(string SQLString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand Cmd = new SqlCommand(SQLString, sqlConnection);
                    int Count = (int)Cmd.ExecuteScalar();// 返回结果行数

                    return Count;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 用一个已经存在的数据库连接执行一个SQL命令
        /// 例如：int result = ExecuteNonQuery(oracleConnection,
        ///                     CommandType.StoredProcedure,
        ///                     "PublishOrders",
        ///                     new OracleParameter(":prodid", 24));
        /// </summary>
        /// <param name="commandType">执行命令类型(存储过程、SQL语句等)</param>
        /// <param name="commandText">存储过程名称或者SQL语句</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>执行命令对数据库影响的行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType,
            params SqlParameter[] commandParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    // 给SQL执行对象赋值
                    PrepareCommand(sqlCommand, sqlConnection, null, commandType, commandText, commandParameters);

                    // 执行SQL语句并返回受影响的行数
                    int intReturnVal = sqlCommand.ExecuteNonQuery();

                    // 清除SQL执行对象的属性值
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
                    return intReturnVal;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源 
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行一个存储过程，并且存储过程返回一个结果集
        /// </summary>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="commandType">执行命令类型</param>
        /// <param name="dataSetName">返回DataSet的名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static DataSet RunProcedure(string commandText, CommandType commandType,
            string dataSetName, params SqlParameter[] commandParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    sqlConnection.Open();

                    // 构建SqlDataAdapter对象
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                   
                    sqlDataAdapter.SelectCommand = BuildQueryCommand(sqlConnection, commandText, commandParameters);
                    sqlDataAdapter.SelectCommand.CommandTimeout = 0;
                    // 填充DataSet对象
                    sqlDataAdapter.Fill(dataSet, dataSetName);

                    // 释放SqlDataAdapter对象资源
                    sqlDataAdapter.Dispose();

                    return dataSet;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源 
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }


        public static string ExecuteProcedure(string commandText, SqlParameter[] commandParameters, string outParameterName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand MyCommand = new SqlCommand(commandText, sqlConnection);
                    MyCommand.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < commandParameters.Length; i++ )
                        MyCommand.Parameters.Add(commandParameters[i]);
                    MyCommand.ExecuteNonQuery();
                    return MyCommand.Parameters[outParameterName].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // 释放资源 
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                // 打开数据库连接
                sqlConnection.Open();
                // 为执行命令对象赋值
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    foreach (string sql in SQLStringList)
                    {
                        if (!string.IsNullOrEmpty(sql))
                        {
                            sqlCommand.CommandText = sql;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    sqlTransaction.Commit();

                    // 清除SQL执行对象的属性值
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
                }
                catch (SqlException ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    // 释放资源
                    if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 构造存储过程的一个输入参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数的数据类型</param>
        /// <param name="Size">参数的长度</param>
        /// <param name="Value">参数的值</param>
        /// <returns>构造完成的参数</returns>
        public static SqlParameter MakeInParam(string paramName, SqlDbType DbType, int size, object value)
        {
            return MakeParam(paramName, DbType, size, ParameterDirection.Input, value);
        }

        /// <summary>
        /// 构造存储过程的一个输入参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数的数据类型</param>
        /// <param name="Value">参数的值</param>
        /// <returns>构造完成的参数</returns>
        public static SqlParameter MakeInParam(string paramName, SqlDbType DbType, object value)
        {
            return MakeParam(paramName, DbType, 0, ParameterDirection.Input, value);
        }

        /// <summary>
        /// 构造存储过程的一个输出参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数数据类型.</param>
        /// <param name="Size">参数长度</param>
        /// <returns>构造完成的参数</returns>
        public static SqlParameter MakeOutParam(string paramName, SqlDbType DbType, int size)
        {
            return MakeParam(paramName, DbType, size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 构造存储过程的参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数的数据类型</param>
        /// <param name="Size">参数长度</param>
        /// <param name="Direction">输出或输入</param>
        /// <param name="Value">参数的值</param>
        /// <returns>构造完成的参数</returns>
        private static SqlParameter MakeParam(string paramName, SqlDbType DbType, Int32 size, ParameterDirection direction, object value)
        {
            SqlParameter param;

            if (size > 0)
            {
                param = new SqlParameter(paramName, DbType, size);
            }
            else
            {
                param = new SqlParameter(paramName, DbType);
            }

            param.Direction = direction;
            if (!(direction == ParameterDirection.Output && value == null))
            {
                param.Value = value;
            }

            return param;
        }

        /// <summary>   
        /// 执行一条计算查询结果语句，返回查询结果（object）。   
        /// </summary>   
        /// <param name="SQLString">计算查询结果语句</param>   
        /// <returns>查询结果（object）</returns>   
        private static object GetSingle(string SQLString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection))
                {
                    try
                    {
                        // 打开连接，并执行查询
                        sqlConnection.Open();
                        object obj = sqlCommand.ExecuteScalar();

                        // 判断查询结果并返回函数值
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        // 释放资源
                        sqlCommand.Dispose();
                        if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="commandParms">SQL语句的参数</param>
        /// <returns>查询结果（object）</returns>
        private static object GetSingle(string SQLString, params SqlParameter[] commandParms)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    try
                    {
                        // 填充SQL命令对象
                        PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, SQLString, commandParms);

                        // 执行查询
                        object obj = sqlCommand.ExecuteScalar();
                        sqlCommand.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        // 释放资源
                        sqlCommand.Dispose();
                        if (sqlConnection.State != ConnectionState.Closed && sqlConnection != null)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>   
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)   
        /// </summary>   
        /// <param name="connection">数据库连接</param>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <returns>SqlCommand</returns>   
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, params SqlParameter[] commandParms)
        {
            SqlCommand sqlCommand = new SqlCommand(storedProcName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in commandParms)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.   
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    sqlCommand.Parameters.Add(parameter);
                }
            }
            return sqlCommand;
        }

        /// <summary>
        /// 为SQLCOMMAND对象赋值
        /// </summary>
        /// <param name="sqlCommand">SqlCommand对象</param>
        /// <param name="sqlConnection">SqlConnection对象</param>
        /// <param name="sqlTransaction">SqlTransaction对象</param>
        /// <param name="commandType">执行命令类型(存储过程、SQL语句等)</param>
        /// <param name="commandText">SQL语句或者存储过程名称</param>
        /// <param name="commandParms">参数</param>
        private static void PrepareCommand(SqlCommand sqlCommand, SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, CommandType commandType,
            string commandText, params SqlParameter[] commandParms)
        {
            try
            {
                // 打开连接
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                // 为SQL命令赋值
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = commandText;
                if (sqlTransaction != null)
                {
                    sqlCommand.Transaction = sqlTransaction;
                }
                sqlCommand.CommandType = commandType;

                if (commandParms != null)
                {
                    foreach (SqlParameter parameter in commandParms)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        sqlCommand.Parameters.Add(parameter);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
  
    }

    }


}
