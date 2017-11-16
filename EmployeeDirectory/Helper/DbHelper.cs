using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Helper
{
    public class DbHelper
    {
        private static Providers _factory = Providers.SqlClient;
        private static Providers _factory_OlaDB = Providers.OleDb;
        private static DbTransaction _transaction = null;
        //private static string _connectionString = GetConnectionStringByProvider(GetFactoryByProvider(_factory), "HSGWEBAPPUSERSdb");

        #region "Enumerations(2)"

        #region "Enum:Providers"
        public enum Providers : int
        {
            Odbc = 1,
            OleDb = 2,
            SqlClient = 3,
            OracleClient = 4,
            MySql = 5
        }
        #endregion

        #region "Enum:DbConnectionTo"
        public enum DbConnectTo
        {
            ProductionDb,
            SyncDb,
            ImportEdata2
        }
        #endregion

        #endregion

        #region "Property(3)"

        #region "Property:ConnectionString(0)"
        /// <summary>
        /// Get or Sets the Connection String
        /// </summary>
        /// <value></value>
        /// <returns>String</returns>
        /// <remarks></remarks>
        public static string ConnectionString
        {
            get;// { return _connectionString; }
            set;// { _connectionString = value; }
        }
        #endregion

        #endregion

        #region "Function(31)"

        #region "Function:GetConnectionStringByProvider(2)"
        // Retrieve a connection string by specifying the providerName. 
        // Assumes one connection string per provider in the config file. 
        private static string GetConnectionStringByProvider(string providerName, string connName)
        {

            //Return Nothing on failure. 
            string returnValue = null;

            // Get the collection of connection strings. 
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            // Walk through the collection and return the first  
            // connection string matching the providerName. 
            if ((settings != null))
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.Name.Equals(connName))
                    {
                        returnValue = cs.ConnectionString;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            return returnValue;
        }
        #endregion

        #region "Function:GetFactoryByProvider(1)"
        /// <summary>
        /// Get Factory By Provider
        /// </summary>
        /// <param name="oGetFactory"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string GetFactoryByProvider(Providers oGetFactory)
        {
            switch ((Providers)oGetFactory)
            {
                case Providers.Odbc:

                    return "System.Data.Odbc";
                case Providers.OleDb:

                    return "System.Data.OleDb";
                case Providers.SqlClient:

                    return "System.Data.SqlClient";
                case Providers.OracleClient:

                    return "System.Data.OracleClient";
                case Providers.MySql:
                    return "CorLab.MySql.MySqlClient";
            }
            return "";
        }
        #endregion

        #region "Function :CreateParameter(3)"
        /// <summary>
        /// Creates a new instance of a System.Data.Commom.dbParameter object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>A System.Data.Commom.dbParameter object</returns>
        /// <remarks></remarks>
        public static DbParameter CreateParameter(string name, DbType type, object value)
        {
            return CreateParameter(name, type, value, ParameterDirection.Input);
        }
        #endregion

        #region "Function:CreateParameter(4)"
        /// <summary>
        /// Creates a new instance of a System.Data.Commom.dbParameter object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns>A System.Data.Commom.dbParameter object</returns>
        /// <remarks></remarks>
        public static DbParameter CreateParameter(string name, DbType type, object value, ParameterDirection direction)
        {

            DbParameter param = null;

            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();

            param = cmd.CreateParameter();

            if ((param != null))
            {
                param.ParameterName = name;
                param.DbType = type;
                param.Direction = direction;
                param.Value = value;
            }

            return param;
        }
        #endregion

        #region "Function:BulkInsertUpdateDelete(1)"
        /// <summary>
        /// Bulks the insert update delete.
        /// </summary>
        /// <param name="pDBBulkTransactions">The p database bulk transactions.</param>
        /// <returns></returns>
        public static bool BulkInsertUpdateDelete(List<DBBulkTransaction> pDBBulkTransactions)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();
            DbTransaction trans = null;
            bool flg = true;
            try
            {
                Con.ConnectionString = ConnectionString;
                cmd.Connection = Con;
                // cmd.CommandText = cmdText
                Con.Open();
                trans = Con.BeginTransaction();
                cmd.Transaction = trans;
                foreach (DBBulkTransaction oBulkTran in pDBBulkTransactions)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = oBulkTran.CmdText;
                    cmd.CommandType = oBulkTran.CmdType;
                    if (!((oBulkTran.CmdParms == null)))
                    {
                        //DbParameter param;
                        foreach (DbParameter param in oBulkTran.CmdParms)
                        {
                            cmd.Parameters.Add(param);
                        }
                        //int val = cmd.ExecuteNonQuery();
                        object val = 0;
                        val = cmd.ExecuteScalar();
                        if (val != null)
                        {
                            if (int.Parse(val.ToString()) == 0)
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                    }
                }
                trans.Commit();
                return flg;

            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                trans.Rollback();
                throw new Exception("DB Exception " + ex.Message);

            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                trans.Rollback();
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
            }
        }
        #endregion

        #region "Function:BulkInsertUpdateDelete(1)"
        /// <summary>
        /// Bulks the insert update delete.
        /// </summary>
        /// <param name="pDBBulkTransactions">The p database bulk transactions.</param>
        /// <returns></returns>
        public static int BulkInsertUpdateDeleteCampaignManager(List<DBBulkTransaction> pDBBulkTransactions)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();
            DbTransaction trans = null;
            int flg = 1;
            try
            {
                Con.ConnectionString = ConnectionString;
                cmd.Connection = Con;
                // cmd.CommandText = cmdText
                Con.Open();
                trans = Con.BeginTransaction();
                cmd.Transaction = trans;
                int prCount = 1;
                foreach (DBBulkTransaction oBulkTran in pDBBulkTransactions)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = oBulkTran.CmdText;
                    cmd.CommandType = oBulkTran.CmdType;
                    if (!((oBulkTran.CmdParms == null)))
                    {
                        //DbParameter param;
                        foreach (DbParameter param in oBulkTran.CmdParms)
                        {
                            cmd.Parameters.Add(param);
                        }
                        object val = 0;
                        if (prCount == 1)
                        {
                            //val = cmd.ExecuteScalar();
                            val = cmd.ExecuteNonQuery();
                            if (val != null)
                            {
                                if (int.Parse(val.ToString()) == -1)
                                {
                                    trans.Rollback();
                                    return -1;
                                }
                                else if (int.Parse(val.ToString()) <= 0)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                            }
                            //foreach (DBBulkTransaction oDummyBulkTran in pDBBulkTransactions)
                            //{
                            //    foreach (DbParameter oDbPara in oDummyBulkTran.CmdParms)
                            //    {
                            //        if (oDbPara.ParameterName.ToLower() == "@wallassignedto")
                            //        {
                            //            if (val != null)
                            //            {
                            //                oDbPara.Value = int.Parse(val.ToString());
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                            //val = cmd.ExecuteScalar();
                            val = cmd.ExecuteNonQuery();
                        }
                        //int val = cmd.ExecuteNonQuery();
                        if (val != null)
                        {
                            if (int.Parse(val.ToString()) == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }
                    }
                    prCount = prCount + 1;
                }
                trans.Commit();
                return flg;

            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                trans.Rollback();
                throw new Exception("DB Exception " + ex.Message);

            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                trans.Rollback();
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
            }
        }
        #endregion

        //        #Region "Function:CopyArticle(1)"
        //        ''' <summary>
        //        ''' Copies the article.
        //        ''' </summary>
        //        ''' <param name="pDBBulkTransactions">The p database bulk transactions.</param>
        //        ''' <returns></returns>
        //        Public Shared Function CopyArticle(ByVal pDBBulkTransactions As List(Of DBBulkTransaction), Optional ByVal pDbConnectTo As DbConnectTo = DbConnectTo.ProductionDb) As Boolean
        //            Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        //            Dim Con As DbConnection = oProviderFactory.CreateConnection
        //            Dim cmd As DbCommand = Con.CreateCommand
        //            Dim trans As DbTransaction = Nothing
        //            Dim flg As Boolean = True
        //            Try
        //                If pDbConnectTo = DbConnectTo.SyncDb Then
        //                    Con.ConnectionString = ConnectionString_Sync
        //                Else
        //                    Con.ConnectionString = ConnectionString
        //                End If
        //                'Con.ConnectionString = ConnectionString
        //                cmd.Connection = Con
        //                ' cmd.CommandText = cmdText
        //                Con.Open()
        //                trans = Con.BeginTransaction
        //                cmd.Transaction = trans
        //                Dim prCount As Integer = 1
        //                For Each oBulkTran As DBBulkTransaction In pDBBulkTransactions
        //                    cmd.Parameters.Clear()
        //                    cmd.CommandText = oBulkTran.CmdText
        //                    cmd.CommandType = oBulkTran.CmdType
        //                    If Not (IsNothing(oBulkTran.CmdParms)) Then
        //                        Dim param As DbParameter
        //                        For Each param In oBulkTran.CmdParms
        //                            cmd.Parameters.Add(param)
        //                        Next
        //                        Dim val As Integer
        //                        If prCount = 1 Then
        //                            val = cmd.ExecuteScalar()
        //                            For Each oDummyBulkTran As DBBulkTransaction In pDBBulkTransactions
        //                                For Each oDbPara As DbParameter In oDummyBulkTran.CmdParms
        //                                    If oDbPara.ParameterName.ToLower = "@artnr" Then
        //                                        oDbPara.Value = val
        //                                    End If
        //                                Next
        //                            Next
        //                        Else
        //                            val = cmd.ExecuteNonQuery()
        //                        End If

        //                        If val = 0 Then
        //                            trans.Rollback()
        //                            Return False
        //                        End If
        //                    End If
        //                    prCount += 1
        //                Next
        //                trans.Commit()
        //                Return flg

        //            Catch ex As DbException
        //                trans.Rollback()
        //                Throw New Exception("DB Exception " & ex.Message)

        //            Catch exx As Exception
        //                trans.Rollback()
        //                Throw New Exception("ExecuteNonQuery Function", exx)
        //            Finally
        //                Con.Close()
        //                cmd = Nothing
        //            End Try
        //        End Function
        //#End Region


        #region "Function :ExecuteNonQuery(2)"
        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <returns></returns>
        /// <remarks>The number of rows affected.</remarks>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(cmdType, cmdText, null);
        }
        #endregion

        #region "Function :ExecuteNonQuery(3)"
        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="cmdParms">Set Array of Parameter</param>
        /// <returns>The number of rows affected.</returns>
        /// <remarks></remarks>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();
            DbTransaction trans = null;
            try
            {
                Con.ConnectionString = ConnectionString;

                cmd.Connection = Con;
                cmd.CommandText = cmdText;
                cmd.Parameters.Clear();
                cmd.CommandType = cmdType;

                if (!((cmdParms == null)))
                {
                    //DbParameter param = default(DbParameter);

                    foreach (DbParameter param in cmdParms)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                //Con.Open();
                if (Con != null && Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                trans = Con.BeginTransaction();
                cmd.Transaction = trans;

                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;

            }
            catch (SqlException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                trans.Rollback();
                throw new Exception("DB Exception " + ex.Message);

            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                trans.Rollback();
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
                cmdParms = null;
            }
        }



        public static int ExecuteNonQueryWithTransaction(DbConnection Con, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            //DbProviderFactory oProviderFactory = default(DbProviderFactory);
            //DbConnection Con = default(DbConnection);
            //oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));

            //Con = oProviderFactory.CreateConnection();

            //Con.ConnectionString = ConnectionString;

            //Con.Open();


            //DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            //DbConnection Con = oProviderFactory.CreateConnection();

            //DbTransaction trans = null;
            //Con.ConnectionString = ConnectionString;
            DbCommand cmd = Con.CreateCommand();
            try
            {

                cmd.Connection = Con;
                cmd.CommandText = cmdText;
                cmd.Parameters.Clear();
                cmd.CommandType = cmdType;

                if (!((cmdParms == null)))
                {
                    //DbParameter param = default(DbParameter);

                    foreach (DbParameter param in cmdParms)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                //Con.Open();
                if (Con != null && Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                //trans = Con.BeginTransaction();
                cmd.Transaction = _transaction;

                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                //trans.Commit();
                return val;

            }
            catch (SqlException ex)
            {
                //ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                //trans.Rollback();
                throw new Exception("DB Exception " + ex.Message);

            }
            catch (Exception exx)
            {
                //ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                //trans.Rollback();
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                //Con.Close();
                cmd.Dispose();
                cmdParms = null;
            }
        }
        #endregion

        #region "Function:ExecuteBackUpRestore(3)"
        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <returns>The number of rows affected.</returns>
        /// <remarks></remarks>
        public static bool ExecuteBackUpRestore(CommandType cmdType, string FilePath, string BackupPath)
        {

            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();
            string sMasterConnectionString = null;


            try
            {
                SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder(ConnectionString);

                //BACKUP DATABASE AdventureWorks2012 TO DISK='d:\adw.bak'
                if (BackupDB(ConnectionString, conBuilder.InitialCatalog, BackupPath))
                {
                    KillAllConnection(ConnectionString, conBuilder.InitialCatalog);
                    sMasterConnectionString = ConnectionString.Replace(conBuilder.InitialCatalog, "master");
                    Con.ConnectionString = sMasterConnectionString;
                    //SqlCommand.CommandText = String.Format("USE MASTER RESTORE DATABASE  {0} FROM DISK='{2}' With REPLACE ", "VPC", FilePath)
                    cmd.Connection = Con;
                    cmd.CommandText = string.Format("USE MASTER RESTORE DATABASE  {0} FROM DISK='{1}' With REPLACE ", "VPC", FilePath);
                    cmd.Parameters.Clear();
                    cmd.CommandType = cmdType;
                    Con.Open();
                    int val = cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return false;
                throw new Exception("DB Exception " + ex.Message);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                return false;
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;

            }
        }
        #endregion

        #region "Function:ExecuteRestore(3)"
        /// <summary>
        /// Executes the restore.
        /// </summary>
        /// <param name="cmdType">Type of the command.</param>
        /// <param name="FilePath">The file path.</param>
        /// <param name="NewDbName">New name of the database.</param>
        /// <returns></returns>
        public static bool ExecuteRestore(CommandType cmdType, string FilePath, string NewDbName)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = Con.CreateCommand();
            string sMasterConnectionString = null;
            try
            {
                string sConnStr = null;
                sConnStr = ConnectionString;

                SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder(sConnStr);
                KillAllConnection(sConnStr, conBuilder.InitialCatalog);
                sMasterConnectionString = sConnStr.Replace(conBuilder.InitialCatalog, "master");
                Con.ConnectionString = sMasterConnectionString;
                cmd.Connection = Con;
                //CreateDatabase(conBuilder.InitialCatalog)
                cmd.CommandText = string.Format("USE MASTER RESTORE DATABASE  {0} FROM DISK='{1}' With REPLACE ", NewDbName, FilePath);
                cmd.Parameters.Clear();
                cmd.CommandType = cmdType;
                Con.Open();
                int val = cmd.ExecuteNonQuery();
                return true;
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return false;
                throw new Exception("DB Exception " + ex.Message);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                return false;
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
            }
        }
        #endregion

        #region "Function:BackupDB(3)"
        /// <summary>
        /// Backups the database.
        /// </summary>
        /// <param name="newConnectionString">The new connection string.</param>
        /// <param name="dbName">Name of the database.</param>
        /// <param name="BackupPath">The backup path.</param>
        /// <returns></returns>
        private static bool BackupDB(string newConnectionString, string dbName, string BackupPath)
        {
            SqlConnection sqlConn = new SqlConnection(newConnectionString);
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.Connection = sqlConn;
                sqlConn.Open();
                sqlCommand.CommandText = "BACKUP DATABASE " + dbName + " TO DISK='" + BackupPath + dbName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak'";
                sqlCommand.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                return false;
                throw new Exception("DB Exception " + ex.Message);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                return false;
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                sqlConn.Close();
                sqlCommand = null;
            }
        }
        #endregion

        #region "Function:KillAllConnection(2)"
        /// <summary>
        /// Kills all connection.
        /// </summary>
        /// <param name="newConnectionString">The new connection string.</param>
        /// <param name="dbName">Name of the database.</param>

        private static void KillAllConnection(string newConnectionString, string dbName)
        {
            SqlConnection sqlConn = new SqlConnection(newConnectionString);
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = sqlConn;
            sqlConn.Open();

            sqlCommand.CommandText = "" + "USE MASTER " + "SET NOCOUNT ON " + "DECLARE @DBName varchar(50) " + "DECLARE @spidstr varchar(8000) " + "DECLARE @ConnKilled smallint " + "SET @ConnKilled=0 " + "SET @spidstr = '' " + "Set @DBName = '" + dbName + "' " + "IF db_id(@DBName) < 4 " + "BEGIN " + "PRINT 'Connections to system databases cannot be killed' " + "RETURN " + "END " + "SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; ' " + "FROM master..sysprocesses WHERE dbid=db_id(@DBName) " + "IF LEN(@spidstr) > 0 " + "BEGIN " + "EXEC(@spidstr) " + "SELECT @ConnKilled = COUNT(1) " + "FROM master..sysprocesses WHERE dbid=db_id(@DBName) " + "END";
            Console.WriteLine(string.Format("Killing connections for database {0}...", dbName));
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
            Console.WriteLine(string.Format("All connections to {0} killed.", dbName));
        }
        #endregion

        #region "Function:ExecuteScalar(2)"
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query.
        /// </summary>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <returns></returns>
        /// <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(cmdType, cmdText, null);
        }
        #endregion

        #region "Function:ExecuteScalar(3)"
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query.
        /// </summary>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="cmdParms">Set Array of Parameter</param>
        /// <returns></returns>
        /// <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            DbProviderFactory oProviderFactory = default(DbProviderFactory);
            DbConnection Con = default(DbConnection);
            DbCommand cmd = default(DbCommand);
            DbTransaction trans = null;
            oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));


            Con = oProviderFactory.CreateConnection();
            cmd = Con.CreateCommand();

            try
            {
                Con.ConnectionString = ConnectionString;

                cmd.Connection = Con;
                cmd.CommandText = cmdText;
                cmd.Parameters.Clear();
                cmd.CommandType = cmdType;

                if (!((cmdParms == null)))
                {
                    //DbParameter param = default(DbParameter);

                    foreach (DbParameter param in cmdParms)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                //Con.Open();
                if (Con != null && Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                trans = Con.BeginTransaction();
                cmd.Transaction = trans;

                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;

            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                trans.Rollback();
                throw new Exception("DB Exception " + ex.Message);

            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                trans.Rollback();
                throw new Exception("ExecuteNonQuery Function", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
                cmdParms = null;
            }
        }
        #endregion

        #region "Function:ExecuteTable(2)"
        /// <summary>
        /// ExecuteTable Return DataTable
        /// </summary>
        /// <param name="cmdType">The command Type</param>
        /// <param name="cmdText">The command text to execute</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText)
        {
            return ExecuteTable(cmdType, cmdText, null);
        }
        #endregion

        #region "Function:ExecuteTable(4)"
        /// <summary>
        /// Executes the table.
        /// </summary>
        /// <param name="cmdType">Type of the command.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="cmdParms">The command parms.</param>
        /// <param name="pDbConnectTo">The p database connect to.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// DB Exception 
        /// or
        /// ExecuteTable Exception :
        /// </exception>
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbDataAdapter oDataAdapter = default(DbDataAdapter);
            DbConnection Con = oProviderFactory.CreateConnection();
            DbCommand cmd = default(DbCommand);
            try
            {
                Con.ConnectionString = ConnectionString;

                cmd = Con.CreateCommand();
                PrepareCommand(ref cmd, ref Con, ref cmdType, ref cmdText, ref cmdParms);
                oDataAdapter = oProviderFactory.CreateDataAdapter();
                DataTable oDataTable = new DataTable();
                oDataAdapter.SelectCommand = cmd;
                oDataAdapter.Fill(oDataTable);
                cmd.Parameters.Clear();
                return oDataTable;
            }
            //catch (DbException ex)
            catch (SqlException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DB Exception ", ex);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                throw new Exception("ExecuteTable Exception :", exx);
            }
            finally
            {
                Con.Close();
                cmd = null;
                oDataAdapter = null;
            }
        }
        #endregion

        #region "Function:ExecuteDataSet(2)"
        /// <summary>
        /// <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText">The command text to execute.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(cmdType, cmdText, null);
        }
        #endregion

        #region "Function:ExecuteDataSet(3)"
        /// <summary>
        /// <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
        /// </summary>
        /// <param name="cmdType">One of the <see cref="CommandType"/> values.</param>
        /// <param name="cmdText">The command text to execute.</param>
        /// <param name="cmdParms"></param>
        /// <returns>DataSet</returns>
        /// <remarks></remarks>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection con = oProviderFactory.CreateConnection();
            DbDataAdapter oDataAdapter = oProviderFactory.CreateDataAdapter();
            DbCommand cmd = con.CreateCommand();

            try
            {
                con.ConnectionString = ConnectionString;
                cmd = con.CreateCommand();
                PrepareCommand(ref cmd, ref con, ref cmdType, ref cmdText, ref cmdParms);
                oDataAdapter = oProviderFactory.CreateDataAdapter();
                DataSet oDataSet = new DataSet();
                oDataAdapter.SelectCommand = cmd;
                oDataAdapter.Fill(oDataSet);
                cmd.Parameters.Clear();

                return oDataSet;


            }
            catch (DbException ex)
            {
                throw new Exception("SQL Exception ", ex);
            }
            catch (Exception exx)
            {
                throw new Exception("Execute DataSet", exx);
            }
            finally
            {
                con.Close();
                cmd = null;
                oDataAdapter = null;
            }
        }
        #endregion

        #region "Function:ExecuteReader(3)"
        /// <summary>
        /// Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
        /// </summary>
        /// <param name="conn">A System.Data.Common.DbConnection that represents the connection to an instance of DataSource.</param>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <returns>A System.Data.Common.DbDataReader object.</returns>
        /// <remarks></remarks>
        public static DbDataReader ExecuteReader(ref DbConnection conn, CommandType cmdType, string cmdText)
        {
            return ExecuteReader(ref conn, cmdType, cmdText, null);
        }
        #endregion

        #region "Function:ExecuteReader(4)"
        /// <summary>
        /// Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
        /// </summary>
        /// <param name="conn">A System.Data.Common.DbConnection that represents the connection to an instance of DataSource.</param>
        /// <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="cmdParms">Set Array of Parameter</param>
        /// <returns>A System.Data.Common.DbDataReader object.</returns>
        /// <remarks></remarks>
        public static DbDataReader ExecuteReader(ref DbConnection conn, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            conn = oProviderFactory.CreateConnection();

            DbDataAdapter oDataAdapter = oProviderFactory.CreateDataAdapter();
            DbCommand cmd = conn.CreateCommand();

            DbDataReader rdr = default(DbDataReader);

            try
            {
                PrepareCommand(ref cmd, ref conn, ref cmdType, ref cmdText, ref cmdParms);
                rdr = cmd.ExecuteReader();
                cmd.Parameters.Clear();

                if (!((cmdParms == null)))
                {
                    //DbParameter param = default(DbParameter);

                    foreach (DbParameter param in cmdParms)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                return rdr;


            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("SQL Exception ", ex);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                throw new Exception("ExecuteReader", exx);
            }
            finally
            {
                cmd = null;
            }
        }
        #endregion

        #region "Function:ExecuteRow(2)"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DataRow ExecuteRow(CommandType cmdType, string cmdText)
        {
            return ExecuteRow(cmdType, cmdText, null);
        }
        #endregion

        #region "Function:ExecuteRow(3)"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DataRow ExecuteRow(CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection Con = oProviderFactory.CreateConnection();
            Con.ConnectionString = ConnectionString;
            DbCommand cmd = Con.CreateCommand();
            DbDataAdapter oDataAdapter = oProviderFactory.CreateDataAdapter();
            DataRow oDataRow = null;
            DataTable oDataTable = new DataTable();
            try
            {
                PrepareCommand(ref cmd, ref Con, ref cmdType, ref cmdText, ref cmdParms);
                oDataAdapter.SelectCommand = cmd;
                oDataAdapter.Fill(oDataTable);
                cmd.Parameters.Clear();
                if (oDataTable.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    DataRow oRow = oDataTable.Rows[0];
                    return oRow;
                }
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DB Exception ", ex);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                throw new Exception("ExecuteRow", exx);
            }
            finally
            {
                Con.Close();
                oDataTable = null;
                cmd = null;
                oDataAdapter = null;
            }
        }
        #endregion

        #region "Function:ExcuteAdapter(3)"
        /// <summary>
        /// Excute Adapter
        /// </summary>
        /// <param name="oTable"></param>
        /// <param name="cmdText"></param>
        /// <param name="lngMaxID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool ExcuteAdapter(DataTable oTable, string cmdText, long lngMaxID = 0)
        {

            DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
            DbConnection conn = oProviderFactory.CreateConnection();
            conn.ConnectionString = ConnectionString;
            DbCommand oSqlCmd = conn.CreateCommand();
            DbDataAdapter oDataAdapter = oProviderFactory.CreateDataAdapter();
            DbCommandBuilder oCmdBuilder = oProviderFactory.CreateCommandBuilder();
            DbTransaction trans = null;

            try
            {
                if (!(conn.State == ConnectionState.Open))
                {
                    conn.Open();
                }

                trans = conn.BeginTransaction();
                oSqlCmd.Transaction = trans;

                oSqlCmd.Connection = conn;
                oSqlCmd.CommandText = cmdText;
                oSqlCmd.CommandType = CommandType.Text;

                oDataAdapter.SelectCommand = oSqlCmd;
                oCmdBuilder.DataAdapter = oDataAdapter;
                oCmdBuilder.GetUpdateCommand();
                oCmdBuilder.GetInsertCommand();
                oCmdBuilder.GetDeleteCommand();
                oDataAdapter.Update(oTable);
                oDataAdapter.SelectCommand.CommandText = "SELECT @@IDENTITY";
                trans.Commit();

                //  lngMaxID = CType(oDataAdapter.SelectCommand.ExecuteScalar(), Long)
                return true;
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                trans.Rollback();
                throw new Exception("DB Exception ", ex);

            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                trans.Rollback();
                throw new Exception("ExeculateAdapter", exx);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                oSqlCmd = null;
                oDataAdapter = null;
                oCmdBuilder = null;
            }
            return false;
        }
        #endregion

        //#region "Function:NewExcuteAdapter(5)"
        ///// <summary>
        ///// News the excute adapter.
        ///// </summary>
        ///// <param name="oTable">The o table.</param>
        ///// <param name="cmdText">The command text.</param>
        ///// <param name="ColumnCompare">The column compare.</param>
        ///// <param name="ColumnUpdate">The column update.</param>
        ///// <param name="lngMaxID">The LNG maximum identifier.</param>
        ///// <returns></returns>
        ///// <exception cref="System.Exception">
        ///// DB Exception 
        ///// or
        ///// ExeculateAdapter
        ///// </exception>
        //public static bool NewExcuteAdapter(DataTable oTable, string cmdText, string ColumnCompare, string ColumnUpdate, long lngMaxID = 0)
        //{
        //    DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
        //    SqlConnection conn = oProviderFactory.CreateConnection;
        //    conn.ConnectionString = ConnectionString;
        //    SqlCommand oSqlCmd = conn.CreateCommand();
        //    SqlDataAdapter oDataAdapter = new SqlDataAdapter(cmdText, conn);
        //    SqlCommandBuilder oCmdBuilder = new SqlCommandBuilder(oDataAdapter);
        //    try
        //    {
        //        if (!(conn.State == ConnectionState.Open))
        //        {
        //            conn.Open();
        //        }
        //        DataTable tempdt = new DataTable();
        //        oDataAdapter.Fill(tempdt);

        //        foreach (DataRow drExcel in oTable.Rows)
        //        {
        //            foreach (DataRow dr in tempdt.Rows)
        //            {
        //                if (drExcel[ColumnCompare].ToString() == dr[ColumnCompare].ToString())
        //                {
        //                    dr[ColumnUpdate] = drExcel[ColumnUpdate];
        //                }
        //            }
        //        }
        //        //   tempdt = oTable.Copy()
        //        int result = oDataAdapter.Update(tempdt);
        //        return true;

        //    }
        //    catch (DbException ex)
        //    {
        //        throw new Exception("DB Exception ", ex);


        //    }
        //    catch (Exception exx)
        //    {
        //        throw new Exception("ExeculateAdapter", exx);

        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //        oSqlCmd = null;
        //        oDataAdapter = null;
        //        oCmdBuilder = null;
        //    }
        //    return false;
        //}
        //#endregion

        #region "Function:PrepareCommand(5)"
        /// <summary>
        /// Prepare Command
        /// </summary>
        /// <param name="cmd">Assigns a <paramref name="connection"/> to the <paramref name="command"/> and discovers parameters if needed.</param>
        /// <param name="conn">The connection to assign to the command.</param>
        /// <param name="cmdType">The command that contains the query to prepare.</param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PrepareCommand(ref DbCommand cmd, ref DbConnection conn, ref CommandType cmdType, ref string cmdText, ref DbParameter[] cmdParms)
        {
            if (!(conn.State == ConnectionState.Open))
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.Parameters.Clear();
                cmd.CommandType = cmdType;

                if (!((cmdParms == null)))
                {
                    //DbParameter param = default(DbParameter);

                    foreach (DbParameter param in cmdParms)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                return true;
            }
            catch (DbException ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DB Exception ", ex);
            }
            catch (Exception exx)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                throw new Exception("PrepareCommand : ", exx);
            }
            return false;
        }
        #endregion

        #region "Function:ExecuteStoredProcedure(3)"
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="ProcName">Name of the proc.</param>
        /// <param name="ParaValues">The para values.</param>
        /// <param name="IncludeReturnPara">if set to <c>true</c> [include return para].</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// DB Exception 
        /// or
        /// PrepareCommand : 
        /// </exception>
        public int ExecuteStoredProcedure(string ProcName, Hashtable ParaValues, bool IncludeReturnPara)
        {
            int result = -1;
            using (DbConnection dbConnection = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory)).CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                DbCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = ProcName;

                try
                {
                    dbCommand.Connection.Open();
                    if ((ParaValues != null) && (ParaValues.Count > 0))
                    {
                        SqlCommandBuilder.DeriveParameters((SqlCommand)dbCommand);

                        foreach (DbParameter p in dbCommand.Parameters)
                        {
                            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                            {
                                p.Value = DBNull.Value;

                            }
                            else
                            {
                                p.Value = ParaValues[p.ParameterName];
                            }
                        }
                        result = dbCommand.ExecuteNonQuery();
                    }
                }
                catch (DbException ex)
                {
                    ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                    throw new Exception("DB Exception ", ex);
                }
                catch (Exception exx)
                {
                    ExceptionHandling.ExceptionHandling.ManageErrorLog(exx);
                    throw new Exception("PrepareCommand : ", exx);
                }
                finally
                {
                    if (dbCommand.Connection.State != ConnectionState.Closed)
                    {
                        dbCommand.Connection.Close();
                        dbCommand.Dispose();
                    }
                }
            }
            return result;
        }
        #endregion

        public static void BeginTransaction()
        {
            DbProviderFactory oProviderFactory = default(DbProviderFactory);
            DbConnection Con = default(DbConnection);
            oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));

            Con = oProviderFactory.CreateConnection();

            Con.ConnectionString = ConnectionString;

            Con.Open();

            _transaction = Con.BeginTransaction();
        }

        public static DbConnection BeginTransactionConn()
        {
            DbProviderFactory oProviderFactory = default(DbProviderFactory);
            DbConnection Con = default(DbConnection);
            oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));

            Con = oProviderFactory.CreateConnection();

            Con.ConnectionString = ConnectionString;

            Con.Open();

            _transaction = Con.BeginTransaction();
            return Con;
        }

        public static void CommintTransaction()
        {
            _transaction.Commit();
        }

        public static void CommintTransactionWithCloseConnection(DbConnection Con)
        {

            _transaction.Commit();
            Con.Close();
        }

        public static void RollBackTransaction()
        {
            _transaction.Rollback();
        }

        public static void RollBackTransactionCloseConnection(DbConnection Con)
        {

            _transaction.Rollback();
            Con.Close();
        }

        #endregion

        #region "Method(2)"

        #region "Method: OnRowsCopied (2)"
        /// <summary>
        /// Called when [rows Copied].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Data.SqlRowsCopiedEventArgs" /> instance containing the event data.</param>
        private void OnRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (RowCopied != null)
            {
                RowCopied(sender, e);
            }
        }
        #endregion

        //#region "Method: SQLBulkCopy (2)"
        ///// <summary>
        ///// SQLs the bulk Copy.
        ///// </summary>
        ///// <param name="pTableName">Name of the p table.</param>
        ///// <param name="pDataTable">The p data table.</param>
        //public bool SQLBulkCopy(string pTableName, DataTable pDataTable, int pNotifyAfter, ref string pErrorMessage, DbConnectTo pDbConnectTo = DbConnectTo.ProductionDb)
        //{

        //    DbProviderFactory oProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory));
        //    DbConnection Con = oProviderFactory.CreateConnection();
        //    DbTransaction trans = null;

        //    if (pDbConnectTo == DbConnectTo.SyncDb)
        //    {
        //        Con.ConnectionString = ConnectionString_Sync;
        //    }
        //    else
        //    {
        //        Con.ConnectionString = ConnectionString;
        //    }
        //    //Con.ConnectionString = ConnectionString

        //    Con.Open();
        //    trans = Con.BeginTransaction();

        //    StringBuilder oMessageList = new StringBuilder();

        //    using (SqlBulkCopy oSqlBulkCopy = new SqlBulkCopy(Con, SqlBulkCopyOptions.Default, trans))
        //    {
        //        try
        //        {
        //            oSqlBulkCopy.DestinationTableName = pTableName;
        //            oSqlBulkCopy.SqlRowsCopied += OnRowsCopied;
        //            oSqlBulkCopy.NotifyAfter = pNotifyAfter;
        //            oSqlBulkCopy.WriteToServer(pDataTable);
        //            trans.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            trans.Rollback();
        //            Con.Close();
        //            oMessageList.AppendLine(ex.Message);
        //            if (ex.InnerException != null && ex.InnerException.Message != null)
        //            {
        //                if (ex.InnerException.Message != string.Empty)
        //                {
        //                    oMessageList.AppendLine(ex.InnerException.Message);
        //                }
        //            }
        //            pErrorMessage = oMessageList.ToString;
        //            return false;
        //        }
        //        finally
        //        {
        //            Con.Close();
        //        }
        //    }
        //    return true;
        //}

        //#endregion


        /// <summary>
        /// Performs the bulk copy.
        /// </summary>
        /// <param name="dtTable">The dt table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public static bool PerformBulkCopy(DataTable dtTable, string tableName)
        {
            try
            {
                using (SqlConnection destinationConnection = new SqlConnection(ConnectionString))
                {
                    //// open the connection
                    destinationConnection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(dtTable);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Event(1)"

        #region "Event: RowCopied (2)"
        /// <summary>
        /// Occurs when [row copied].
        /// </summary>
        public event RowCopiedEventHandler RowCopied;
        public delegate void RowCopiedEventHandler(object sender, SqlRowsCopiedEventArgs e);
        #endregion

        #endregion

    }
}