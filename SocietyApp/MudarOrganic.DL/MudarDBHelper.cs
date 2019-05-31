using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace MudarOrganic.DL
{
    public class MudarDBHelper
    {
        #region Private Fields

        private static volatile MudarDBHelper _instance;
        private static object _syncRoot = new Object();

        #endregion

        #region Public Properties

        /// <summary>
        /// Locking Mechanism to ensure only one instance of Logger is created
        /// </summary>
        public static MudarDBHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new MudarDBHelper();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Private Constructor

        private MudarDBHelper()
        {

        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public SqlParameter AddParameter(string ParameterName, SqlDbType dbType, int size, object value, bool direction)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = dbType;
            Param.ParameterName = "@" + ParameterName;
            Param.Size = size;
            Param.Value = value;
            Param.Direction = ParameterDirection.Output;
            return Param;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ParameterName"></param>
        ///// <param name="dbType"></param>
        ///// <param name="size"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public SqlParameter AddParameter(string ParameterName, SqlDbType dbType, int size, object value)
        //{
        //    SqlParameter Param = new SqlParameter();
        //    Param.SqlDbType = dbType;
        //    Param.ParameterName = "@" + ParameterName;
        //    Param.Size = size;
        //    Param.Value = value;
        //    Param.Direction = ParameterDirection.Input;
            
        //    return Param;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public SqlParameter AddParameter(string ParameterName, SqlDbType dbType, object value, bool direction)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = dbType;
            Param.ParameterName = "@" + ParameterName;
            Param.Value = value;
            Param.Direction = ParameterDirection.Output;
            return Param;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlParameter AddParameter(string ParameterName, SqlDbType dbType, object value)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = dbType;
            Param.ParameterName = "@" + ParameterName;
            Param.Value = value;
            Param.Direction = ParameterDirection.Input;            
            return Param;
        }

        public DataTable ExecuteDataTable(string sSql)
        {
            DataTable dt = new DataTable();
            DataSet ds = ExecuteDataSet(sSql);

            if (ds.Tables.Count > 0)
                dt = ds.Tables[0].Copy();

            return dt;
        }
        public DataSet ExecuteDataSet(string sSql)
        {
           
            Database db = DatabaseFactory.CreateDatabase();
            DataSet oDs;
            DbCommand oCommand;

            try
            {
                oCommand = db.GetSqlStringCommand(sSql);
                oCommand.CommandTimeout = 100;
                oDs = db.ExecuteDataSet(oCommand);
                return oDs;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(DbCommand oCommand)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet oDs;

            try
            {
                oDs = db.ExecuteDataSet(oCommand);
               // oCommand.CommandTimeout= 50;
                return oDs;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(string sDbObject, string sWhereClause, string sGroupBy, string sOrderBy, string sSelectFields)
        {
            try
            {
                string sSql = "SELECT {0} FROM {1} {2} {3} {4} ";
                sSql = String.Format(sSql, sSelectFields, sDbObject, sWhereClause, sGroupBy, sOrderBy);
                //DataSet dsData = WebProducts.Data.DataObject.ReturnDataSet(sDbConnection, eDbConnectionType, sSql);
                DataSet dsData = ExecuteDataSet(sSql);
                return dsData;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(string sSPName, List<SqlParameter> sParamNames)
        {
            //Inits/Decs
            Database db = DatabaseFactory.CreateDatabase();
            DataSet oDs = new DataSet();
            DbCommand oCommand;

            try
            {
                oCommand = db.GetStoredProcCommand(sSPName);
                foreach (SqlParameter param in sParamNames)
                    oCommand.Parameters.Add(param);
                oDs = db.ExecuteDataSet(oCommand);
                return oDs;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IDataReader ExecuteDataReader(string sSQL)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand oCommand;
            IDataReader reader;

            try
            {
                oCommand = db.GetSqlStringCommand(sSQL);
                reader = db.ExecuteReader(oCommand);
                return reader;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteNonQuery(DbCommand oCommand)
        {
            Database db = DatabaseFactory.CreateDatabase();

            try
            {
                db.ExecuteNonQuery(oCommand);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ExecuteDataTable(string strStoredProcName,  string outDataTableName)
        {
            DataSet dsResult = null;
            DataTable dtToBeReturned = null;
            DbCommand dbCommand = null;
            Database db = null;
            try
            {
                dtToBeReturned = new DataTable(outDataTableName);
                dtToBeReturned.Clear();
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand(strStoredProcName);
                dsResult = db.ExecuteDataSet(dbCommand);
                if (dsResult.Tables.Count > 0)
                {
                    dtToBeReturned = dsResult.Tables[0];
                }
                return dtToBeReturned;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // This is the generic method to get a datatable passing the stored procedure, parameter collection and output datatable name
        public DataTable ExecuteDataTable(string strStoredProcName, List<SqlParameter> parameterCollection, string outDataTableName)
        {
            DataSet dsResult = null;
            DataTable dtToBeReturned = null;
            DbCommand dbCommand = null;
            Database db = null;
            try
            {
                dtToBeReturned = new DataTable(outDataTableName);
                dtToBeReturned.Clear();
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand(strStoredProcName);
                foreach (SqlParameter parm in parameterCollection)
                {
                    dbCommand.Parameters.Add(parm);
                }
                dsResult = db.ExecuteDataSet(dbCommand);
                if (dsResult.Tables.Count > 0)
                {
                    dtToBeReturned = dsResult.Tables[0];
                }

                return dtToBeReturned;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExecuteStoredProcWithSingleOutParam(string sStoredProcName, ArrayList paramCollection)
        {
            Database db = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;

            string sOutParamName = string.Empty; 
            string sOutParamValue = string.Empty;
            db = DatabaseFactory.CreateDatabase();
            using (DbConnection con = db.CreateConnection())
            {
                try
                {
                    con.Open();
                    dbTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    dbCommand = db.GetStoredProcCommand(sStoredProcName);
                    for (int i = 0; i < paramCollection.Count; i++)
                    {
                        SqlParameter parm = (SqlParameter)paramCollection[i];
                        if (parm.Direction == ParameterDirection.Output)
                        {
                            db.AddOutParameter(dbCommand, parm.ParameterName, parm.DbType, parm.Size);
                            sOutParamName = parm.ParameterName;
                        }
                        else
                            db.AddInParameter(dbCommand, parm.ParameterName, parm.DbType, parm.Value);
                    }
                    //dbCommand.Transaction = dbTransaction;
                    db.ExecuteNonQuery(dbCommand, dbTransaction);

                    sOutParamValue = db.GetParameterValue(dbCommand, sOutParamName).ToString();

                    dbTransaction.Commit();

                    return sOutParamValue;
                }
                catch (ApplicationException ex)
                {
                    dbTransaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    throw ex;
                }
            }
        }
        public DataSet SqlDataSet(string sStoredProcName, object[] paramvals, string paramlist)
        {
            Database db = null;
            DbTransaction dbTransaction = null;
            DbCommand dbCommand = null;
            DataSet dsResult = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                using (DbConnection con = db.CreateConnection())
                {
                    db = DatabaseFactory.CreateDatabase();
                    dbCommand = db.GetStoredProcCommand(sStoredProcName);
                    if (paramvals != null)
                    {
                        string[] paramnames = paramlist.Split('|');
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        int i = 0;

                        foreach (object o in paramvals)
                        {
                            dbCommand.Parameters.Add(new SqlParameter(paramnames[i], o));
                            dbCommand.Parameters[i].Value = o;

                            ++i;
                        }
                       
                    }
                    dsResult = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (ApplicationException ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            return dsResult;
        }
        /// <summary>
        /// CreatedBy : Shaik Aslam
        /// CreatedDate : 7/2/2012
        /// </summary>
        /// <param name="oCommand"></param>
        public object ExecuteNonQuery(string sStoredProcName, List<SqlParameter> paramCollection)
        {
            Database db = null;
            DbCommand dbCommand = null;
            object returnValue = null;
            db = DatabaseFactory.CreateDatabase();
            using (DbConnection con = db.CreateConnection())
            {
                try
                {
                    con.Open();
                    dbCommand = db.GetStoredProcCommand(sStoredProcName);
                    foreach (SqlParameter parm in paramCollection)
                    {
                        dbCommand.Parameters.Add(parm);
                    }
                    db.ExecuteNonQuery(dbCommand);
                    returnValue = GetReturnValue(dbCommand);
                }
                catch (ApplicationException ex)
                {

                    throw ex;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return returnValue;
        }
        /// <summary>
        /// CreatedBy : Shaik Aslam
        /// CreatedDate : 7/16/2012
        /// </summary>
        /// <param name="oCommand"></param>
        public void ExecuteNonQuery(string sStoredProcName, List<SqlParameter> paramCollection, ref List<string> returnValue)
        {
            Database db = null;
            DbCommand dbCommand = null;
            List<string> result = new List<string>();
            db = DatabaseFactory.CreateDatabase();
            using (DbConnection con = db.CreateConnection())
            {
                try
                {
                    con.Open();
                    dbCommand = db.GetStoredProcCommand(sStoredProcName);
                    foreach (SqlParameter parm in paramCollection)
                    {
                        dbCommand.Parameters.Add(parm);
                    }
                    db.ExecuteNonQuery(dbCommand);
                    GetReturnValue(dbCommand, result);
                    foreach (string str in result)
                        returnValue.Add(str);
                }
                catch (ApplicationException ex)
                {

                    throw ex;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public void ExecuteDataSet(string storedprocedure, ArrayList paramCollection, ref object returnValue, ref DataSet dsout)
        {
            Database db = null;
            DbCommand dbCommand = null;
            returnValue = null;
            db = DatabaseFactory.CreateDatabase();
            using (DbConnection con = db.CreateConnection())
            {
                try
                {
                    con.Open();
                    dbCommand = db.GetStoredProcCommand(storedprocedure);
                    for (int i = 0; i < paramCollection.Count; i++)
                    {
                        SqlParameter parm = (SqlParameter)paramCollection[i];
                        dbCommand.Parameters.Add(parm);
                    }
                    dsout = db.ExecuteDataSet(dbCommand);
                    returnValue = GetReturnValue(dbCommand);
                }
                catch (ApplicationException ex)
                {

                    throw ex;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }
        /// <summary>
        /// Created By:Aslam
        /// Created Date: 7/2/2012
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private object GetReturnValue(DbCommand cmd)
        {
            foreach (SqlParameter param in cmd.Parameters)
            {
                if (param.Direction == ParameterDirection.Output && param.Value != null)
                    return param.Value;
            }
            return null;
        }
        /// <summary>
        /// Created By:Aslam
        /// Created Date: 7/12/2012
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private void GetReturnValue(DbCommand cmd, List<string> result)
        {
            foreach (SqlParameter param in cmd.Parameters)
            {
                if (param.Direction == ParameterDirection.Output && param.Value != null)
                    result.Add(param.Value.ToString());
            }
        }
        #endregion
    }
}
