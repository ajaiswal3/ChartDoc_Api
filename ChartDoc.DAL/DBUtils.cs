using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChartDoc.DAL
{
    /// <summary>
    /// BaseDBUtils : THIS ABSTRACT CLASS WILL EXTEND INTERFACE AND DEFINE ABSTRACT FUNCTION.
    /// BASICALLY THIS ABSTRACT CLASS BEHAVE LIKE A ADAPTER CLASS.
    /// IT WILL ALSO MAINTAIN DAL RELATED CONSISTENCY.
    /// </summary>
    public abstract class BaseDBUtils : IDBUtils
    {
        #region Notes
        //THIS ABSTRACT CLASS WILL EXTEND INTERFACE AND DEFINE ABSTRACT FUNCTION.
        //BASICALLY THIS ABSTRACT CLASS BEHAVE LIKE A ADAPTER CLASS.
        //IT WILL ALSO MAINTAIN DAL RELATED CONSISTENCY
        #endregion Notes

        #region Public Abstract Methods************************************************************************************************************************
        public abstract System.Data.SqlClient.SqlDataReader ExecuteDr(string vSql);
        public abstract System.Data.SqlClient.SqlDataReader ExecuteDr(string vSql, string SPName);
        public abstract object GetNull(object Field);
        public abstract System.Collections.ArrayList GetSPParams(string SpName);
        public abstract System.Collections.Hashtable GetTableColumnDetail(System.Data.SqlClient.SqlDataReader reader);
        public abstract int HandleData(string Sql);
        public abstract int HandleData(System.Collections.ArrayList objArr, string SpName);
        public abstract string HandleDataWithReturnParams(ArrayList objArr, string SpName);
        public abstract SqlDataReader HandleDataWithDataReader(ArrayList objArr, string SpName);
        public abstract string GetConnectionString();
        public abstract object GetSingleValue(string vSql, string SPName);
        public abstract object GetSingleValue(string vSql);
        public abstract System.Data.DataSet ExecutePagingSql(string sConStr, string spName, object[] objArr);
        public abstract System.Data.DataSet ExecuteDs(string vSql);
        public abstract System.Data.DataSet ExecuteDs(string sParamnm, string vSql, string SPName);
        public abstract DataTable FillDatatableForCrpt(string vSql, DataTable tmpTbl);
        public abstract object ExecuteData(ArrayList arrLst, string sSpName);
        public abstract System.Data.DataTable ExecutePagingSqlReturnTable(string sConStr, string spName, object[] objArr);
        #endregion
    }

    /// <summary>
    /// DBUtils : Class
    /// </summary>
    public class DBUtils : BaseDBUtils
    {
        private static readonly DBUtils _instance = new DBUtils();
        public  IConfiguration _configaration;

        #region Private constructor****************************************************************************************************************************
        private DBUtils() { }
        #endregion

        #region GetInstance************************************************************************************************************************************
        /// <summary>
        /// GetInstance : Public static method which return the instance of DBUtils.
        /// </summary>
        public static DBUtils GetInstance { get { return _instance; } }

        #endregion

        #region GetConnectionString****************************************************************************************************************************
        /// <summary>
        /// GetConnectionString : THIS FUNCTION WILL RETURN FIXED CONNECTION STRING RELATED INFO.
        /// </summary>
        /// <returns>string</returns>
        public override string GetConnectionString()
        {
            #region Notes
            //THIS FUNCTION WILL RETURN FIXED CONNECTION STRING RELATED INFO.
            #endregion Notes
            try
            {
                string _dbConnection = _configaration.GetConnectionString("constring");
                return _dbConnection;
            }
            catch (Exception ex)
            {
                string lvar_errMsg = ex.Message;
                return lvar_errMsg;
            }
        }
        #endregion

        #region GetNull****************************************************************************************************************************************
        /// <summary>
        /// GetNull : THIS FUNCTION WILL RETURN ONLY NULL VALUE.
        /// </summary>
        /// <param name="field">object</param>
        /// <returns>object</returns>
        public override object GetNull(object field)
        {
            #region Notes
            //THIS FUNCTION WILL RETURN ONLY NULL VALUE
            #endregion Notes

            return DBNull.Value;
        }
        #endregion

        #region GetTableColumnDetail***************************************************************************************************************************
        /// <summary>
        /// GetTableColumnDetail : THIS FUNCTION WILL POPULATE TABLE SCHEMA RELATED INFO INTO HASTABLE AND RETURN HASTABLE.
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns>Hashtable</returns>
        public override Hashtable GetTableColumnDetail(SqlDataReader reader)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE TABLE SCHEMA RELATED INFO INTO HASTABLE AND RETURN HASTABLE
            #endregion Notes

            Hashtable objHtbl = new Hashtable();
            DataTable schemaTable = reader.GetSchemaTable();
            int intCounter = 0;
            objHtbl.Clear();
            for (intCounter = 0; intCounter <= schemaTable.Rows.Count - 1; intCounter++)
            {
                DataRow dataRow = schemaTable.Rows[intCounter];
                objHtbl.Add(((string)(dataRow["ColumnName"])), ((Type)(dataRow["DataType"])));
            }
            return objHtbl;
        }
        #endregion

        #region GetSingleValue*********************************************************************************************************************************
        /// <summary>
        /// GetSingleValue : THIS FUNCTION WILL CAPTURE A SINGLE VALUE PASSED BY STORE PROCEDURE.
        /// </summary>
        /// <param name="vSql">string</param>
        /// <param name="spName">string</param>
        /// <returns>object</returns>
        public override object GetSingleValue(string vSql, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL CAPTURE A SINGLE VALUE PASSED BY STORE PROCEDURE
            #endregion Notes

            object oReturnValue;
            SqlParameter[] arParms = new SqlParameter[0];
            try
            {
                if (vSql.Trim() != "")
                {
                    arParms[0] = new SqlParameter("@sSql", SqlDbType.NText);
                    arParms[0].Value = vSql;
                    oReturnValue = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, spName, arParms);
                }
                else
                {
                    oReturnValue = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oReturnValue;
        }
        #endregion

        #region GetData****************************************************************************************************************************************
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="argQuery">string</param>
        /// <returns>DataTable</returns>
        public DataTable GetData(string argQuery)
        {
            DataTable dtSet = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                SqlCommand command = new SqlCommand(argQuery, connection);

                command.CommandTimeout = 7000;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataAdapter adpt = new SqlDataAdapter(command);

                adpt.Fill(dtSet);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string lvar_errMsg = ex.Message;
            }

            return dtSet;
        }
        #endregion

        #region GetSingleValue*********************************************************************************************************************************
        /// <summary>
        /// GetSingleValue : THIS FUNCTION WILL CAPTURE A SINGLE VALUE AFTER EXECUTING INLINE SQL.
        /// </summary>
        /// <param name="vSql">string</param>
        /// <returns>object</returns>
        public override object GetSingleValue(string vSql)
        {
            #region Notes
            //THIS FUNCTION WILL CAPTURE A SINGLE VALUE AFTER EXECUTING INLINE SQL
            #endregion Notes

            object oReturnValue;
            try
            {
                oReturnValue = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, vSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oReturnValue;
        }
        #endregion

        #region ExecuteDs**************************************************************************************************************************************
        /// <summary>
        /// ExecuteDs : THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING INLINE SQL AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT
        /// </summary>
        /// <param name="vSql">string</param>
        /// <returns>DataSet</returns>
        public override DataSet ExecuteDs(string vSql)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING INLINE SQL
            //AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            DataSet dsResultsets;
            try
            {
                dsResultsets = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.Text, vSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsResultsets;
        }
        #endregion

        #region ExecuteDs**************************************************************************************************************************************
        /// <summary>
        /// ExecuteDs : THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING DYNAMIC STORE PROCEDURE AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT
        /// </summary>
        /// <param name="sParamnm">string</param>
        /// <param name="vSql">string</param>
        /// <param name="spName">string</param>
        /// <returns>DataSet</returns>
        public override DataSet ExecuteDs(string sParamnm, string vSql, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING DYNAMIC STORE PROCEDURE
            //AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            DataSet dsResultsets;
            SqlParameter[] arParms = new SqlParameter[1];
            try
            {
                if (vSql != "")
                {
                    arParms[0] = new SqlParameter(sParamnm, SqlDbType.NText);
                    arParms[0].Value = vSql;

                    dsResultsets = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, spName, arParms);
                }
                else
                {
                    dsResultsets = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsResultsets;
        }
        #endregion

        #region FillDatatableForCrpt***************************************************************************************************************************
        /// <summary>
        /// FillDatatableForCrpt : THIS FUNCTION WILL POPULATE DATATABLE AFTER EXECUTING DYNAMIC STORE PROCEDURE AND ALSO RETURN DATATABLE TO IT'S CALLING ENVIRONMENT IT WILL BE USED FOR BINDING CRYSTAL REPORT
        /// </summary>
        /// <param name="vSql">string</param>
        /// <param name="retDatatbl">DataTable</param>
        /// <returns>DataTable</returns>
        public override DataTable FillDatatableForCrpt(string vSql, DataTable retDatatbl)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE DATATABLE AFTER EXECUTING DYNAMIC STORE PROCEDURE
            //AND ALSO RETURN DATATABLE TO IT'S CALLING ENVIRONMENT
            //IT WILL BE USED FOR BINDING CRYSTAL REPORT
            #endregion Notes

            try
            {
                retDatatbl = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.Text, vSql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retDatatbl;
        }
        #endregion

        #region ExecuteDr**************************************************************************************************************************************
        /// <summary>
        /// ExecuteDr : THIS FUNCTION WILL POPULATE SQLDATAREADER AFTER EXECUTING DYNAMIC STORE PROCEDURE AND ALSO RETURN SQLDATAREADER TO IT'S CALLING ENVIRONMENT.
        /// </summary>
        /// <param name="vSql">string</param>
        /// <param name="spName">string</param>
        /// <returns>SqlDataReader</returns>
        public override SqlDataReader ExecuteDr(string vSql, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE SQLDATAREADER AFTER EXECUTING DYNAMIC STORE PROCEDURE
            //AND ALSO RETURN SQLDATAREADER TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            SqlDataReader objSqlDataReader;
            SqlParameter[] arParms = new SqlParameter[0];
            try
            {
                if (vSql.Trim() != "")
                {
                    arParms[0] = new SqlParameter("@sSql", SqlDbType.NText);
                    arParms[0].Value = vSql;
                    objSqlDataReader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, spName, arParms);
                }
                else
                {
                    objSqlDataReader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSqlDataReader;
        }
        #endregion

        #region ExecuteDr**************************************************************************************************************************************
        /// <summary>
        /// ExecuteDr : THIS FUNCTION WILL POPULATE SQLDATAREADER AFTER EXECUTING STORE PROCEDURE AND ALSO RETURN SQLDATAREADER TO IT'S CALLING ENVIRONMENT.
        /// </summary>
        /// <param name="vSql">string</param>
        /// <returns>SqlDataReader</returns>
        public override SqlDataReader ExecuteDr(string vSql)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE SQLDATAREADER AFTER EXECUTING STORE PROCEDURE
            //AND ALSO RETURN SQLDATAREADER TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            SqlDataReader objSqlDataReader;
            try
            {
                objSqlDataReader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.Text, vSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSqlDataReader;
        }
        #endregion

        #region ExecutePagingSql*******************************************************************************************************************************
        /// <summary>
        /// ExecutePagingSql : THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING STORE PROCEDURE AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT. IT WILL BE USED ONLY FOR HAVING PAGING FUNCTIONALITY.
        /// </summary>
        /// <param name="sConStr">string</param>
        /// <param name="spName">string</param>
        /// <param name="objArr">object[]</param>
        /// <returns>DataSet</returns>
        public override DataSet ExecutePagingSql(string sConStr, string spName, object[] objArr)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING STORE PROCEDURE
            //AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT.
            //IT WILL BE USED ONLY FOR HAVING PAGING FUNCTIONALITY
            #endregion Notes

            DataSet dsResultset = new DataSet();
            dsResultset = SqlHelper.ExecuteDataset(sConStr, spName, objArr);
            return dsResultset;
        }
        #endregion

        #region GetSPParams************************************************************************************************************************************
        /// <summary>
        /// GetSPParams : THIS FUNCTION WILL FETCH PROCEDURE'S PARAM RELATED INFO ONE BY ONE LIKE PARAM NAME,PARAM TYPE, 
        /// PARAM DIRECTION AND SIDE-BY-SIDE IT WILL POPULATED A ARRAYLIST WITH EACH PARAM RELATED INFO.
        /// </summary>
        /// <param name="SpName">string</param>
        /// <returns>ArrayList</returns>
        public override ArrayList GetSPParams(string SpName)
        {
            #region Notes
            //THIS FUNCTION WILL FETCH PROCEDURE'S PARAM RELATED INFO ONE BY ONE LIKE PARAM NAME,PARAM TYPE,
            //PARAM DIRECTION AND SIDE-BY-SIDE IT WILL POPULATED A ARRAYLIST WITH EACH PARAM RELATED INFO
            #endregion Notes

            ArrayList objArrParams = new ArrayList();
            int i = 0;
            SqlParameter[] objParams = SqlHelperParameterCache.GetSpParameterSet(GetConnectionString(), SpName);
            objArrParams.Clear();
            for (i = 0; i <= objParams.Length - 1; i++)
            {
                objArrParams.Add(new ParamsList(objParams[i].ParameterName, objParams[i].Value, objParams[i].SqlDbType, objParams[i].Direction));
            }
            return objArrParams;
        }
        #endregion

        #region HandleData*************************************************************************************************************************************
        /// <summary>
        /// HandleData : THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
        /// FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND RETURN HOW MANY ROWS ARE AFFECTED TO IT'S CALLING ENVIRONMENT.
        /// </summary>
        /// <param name="objArr">ArrayList</param>
        /// <param name="spName">string</param>
        /// <returns>int</returns>
        public override int HandleData(ArrayList objArr, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
            //FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND RETURN HOW MANY ROWS ARE AFFECTED
            //TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            int index = 0;
            SqlParameter[] arParms = new SqlParameter[objArr.Count];
            for (index = 0; index <= objArr.Count - 1; index++)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ((ParamsList)(objArr[index])).ParamName;
                param.SqlDbType = ((ParamsList)(objArr[index])).ParamType;
                if (((ParamsList)(objArr[index])).ParamType == SqlDbType.DateTime)
                {
                    if (!(((ParamsList)(objArr[index])).ParamValue == DBNull.Value))
                    {
                        param.Value = System.Convert.ToDateTime(((ParamsList)(objArr[index])).ParamValue);
                    }
                    else
                    {
                        param.Value = DBNull.Value;
                    }
                }
                else
                {
                    param.Value = ((ParamsList)(objArr[index])).ParamValue;
                }
                arParms[index] = param;
            }
            try
            {
                index = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, spName, arParms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return index;
        }
        #endregion

        #region HandleDataWithReturnParams*********************************************************************************************************************
        /// <summary>
        /// HandleDataWithReturnParams : THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
        /// FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND CAPTURE SINGLE RETURN VALUE AND RETURN THAT VALUE TO IT'S CALLING ENVIRONMENT.
        /// </summary>
        /// <param name="objArr">ArrayList</param>
        /// <param name="spName">string</param>
        /// <returns>string</returns>
        public override string HandleDataWithReturnParams(ArrayList objArr, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
            //FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND CAPTURE SINGLE RETURN VALUE AND
            //RETURN THAT VALUE TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            string sRetVal = null;
            int index = 0;
            SqlParameter[] arParms = new SqlParameter[objArr.Count];
            for (index = 0; index <= objArr.Count - 1; index++)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ((ParamsList)(objArr[index])).ParamName;
                param.SqlDbType = ((ParamsList)(objArr[index])).ParamType;
                if (((ParamsList)(objArr[index])).ParamDirection == ParameterDirection.Input)
                {
                    if (((ParamsList)(objArr[index])).ParamType == SqlDbType.DateTime)
                    {
                        if (!(((ParamsList)(objArr[index])).ParamValue == DBNull.Value))
                        {
                            param.Value = System.Convert.ToDateTime(((ParamsList)(objArr[index])).ParamValue);
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        param.Value = ((ParamsList)(objArr[index])).ParamValue;
                    }
                }
                arParms[index] = param;
            }
            try
            {
                sRetVal = Convert.ToString(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, spName, arParms));
            }
            catch (Exception ex)
            {
                throw ex;
            }



            return sRetVal;
        }
        #endregion

        #region HandleDataWithDataReader***********************************************************************************************************************
        /// <summary>
        /// HandleDataWithDataReader : THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
        /// FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND CAPTURE RETURN VALUES AND POPULATE
        /// DATAREADER WITH THE RETURN VALUE AND RETURN POPULATED DATAREADER TO IT'S CALLING ENVIRONMENT.
        /// </summary>
        /// <param name="objArr">ArrayList</param>
        /// <param name="spName">string</param>
        /// <returns>SqlDataReader</returns>
        public override SqlDataReader HandleDataWithDataReader(ArrayList objArr, string spName)
        {
            #region Notes
            //THIS FUNCTION WILL TAKE STOTRE PROCEDURE NAME AND STORE PROCEDURE PARAMS RELATED VALUES
            //FROM USER AND DYNAMICALLY EXCUTE STOTRE PROCEDURE AND CAPTURE RETURN VALUES AND POPULATE
            //DATAREADER WITH THE RETURN VALUE AND RETURN POPULATED DATAREADER TO IT'S CALLING ENVIRONMENT
            #endregion Notes

            SqlDataReader oSqlDataReader = null;
            int index = 0;
            SqlParameter[] arParms = new SqlParameter[objArr.Count];
            for (index = 0; index <= objArr.Count - 1; index++)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ((ParamsList)(objArr[index])).ParamName.ToString();
                param.SqlDbType = ((ParamsList)(objArr[index])).ParamType;
                if (((ParamsList)(objArr[index])).ParamDirection == ParameterDirection.Input)
                {
                    if (((ParamsList)(objArr[index])).ParamType == SqlDbType.DateTime)
                    {
                        if (!(((ParamsList)(objArr[index])).ParamValue == DBNull.Value))
                        {
                            param.Value = System.Convert.ToDateTime(((ParamsList)(objArr[index])).ParamValue);
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        param.Value = ((ParamsList)(objArr[index])).ParamValue;
                    }
                }
                arParms[index] = param;
            }
            try
            {
                oSqlDataReader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, spName, arParms);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oSqlDataReader;
        }
        #endregion

        #region HandleData*************************************************************************************************************************************
        /// <summary>
        /// HandleData : THIS FUNCTION WILL TAKE SQL FROM USER AND DYNAMICALLY EXCUTE IT.
        /// </summary>
        /// <param name="sql">string</param>
        /// <returns>int</returns>
        public override int HandleData(string sql)
        {
            #region Notes
            //THIS FUNCTION WILL TAKE SQL FROM USER AND DYNAMICALLY EXCUTE IT
            #endregion Notes

            int iOutput;
            try
            {
                iOutput = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iOutput;
        }
        #endregion

        #region Add Delete Modify******************************************************************************************************************************
        /// <summary>
        /// ExecuteData : THIS FUNCTION WILL CALL STORE PROCEDURE AND PASS USER ID AND PWD THERE FOR CHECKING USER CREDENTIAL.
        /// </summary>
        /// <param name="arrLst">ArrayList</param>
        /// <param name="spName">string</param>
        /// <returns>object</returns>
        public override object ExecuteData(ArrayList arrLst, string spName)
        {
            object objObject = null;
            ArrayList objArrFields = new ArrayList();
            ArrayList objArrParams = new ArrayList();

            objArrParams = DBUtils.GetInstance.GetSPParams(spName);
            for (short i = 0; i <= objArrParams.Count - 1; i++)
            {
                objArrFields.Add(new ParamsList(((ParamsList)(objArrParams[i])).ParamName, arrLst[i], ((ParamsList)(objArrParams[i])).ParamType
                    , ((ParamsList)(objArrParams[i])).ParamDirection));
            }
            objObject = DBUtils.GetInstance.HandleDataWithReturnParams(objArrFields, spName);
            return objObject;
        }
        #endregion

        #region FetchData**************************************************************************************************************************************
        /// <summary>
        /// FetchData
        /// </summary>
        /// <param name="objSqlDataReader">SqlDataReader</param>
        /// <returns>Hashtable</returns>
        public Hashtable FetchData(SqlDataReader objSqlDataReader)
        {
            Hashtable arrHashtable = new Hashtable();
            if (objSqlDataReader.HasRows)
            {
                if (objSqlDataReader.Read())
                {
                    for (int index = 0; index < objSqlDataReader.FieldCount; index++)
                    {
                        string sColumnName = objSqlDataReader.GetName(index).ToUpper();
                        string sCurrent = objSqlDataReader[index].ToString();
                        arrHashtable.Add(objSqlDataReader.GetName(index).ToUpper(), objSqlDataReader[index].ToString());
                    }
                }
            }
            objSqlDataReader.Close();
            return arrHashtable;
        }
        #endregion

        #region ExecutePagingSql*******************************************************************************************************************************
        /// <summary>
        /// ExecutePagingSqlReturnTable : THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING STORE PROCEDURE
        /// AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT. IT WILL BE USED ONLY FOR HAVING PAGING FUNCTIONALITY.
        /// </summary>
        /// <param name="sConStr">string</param>
        /// <param name="spName">string</param>
        /// <param name="objArr">object[]</param>
        /// <returns>DataTable</returns>
        public override DataTable ExecutePagingSqlReturnTable(string sConStr, string spName, object[] objArr)
        {
            #region Notes
            //THIS FUNCTION WILL POPULATE DATASET AFTER EXECUTING STORE PROCEDURE
            //AND ALSO RETURN DATASET TO IT'S CALLING ENVIRONMENT.
            //IT WILL BE USED ONLY FOR HAVING PAGING FUNCTIONALITY
            #endregion Notes

            DataSet dsPaging = new DataSet();
            DataTable dtPaging = new DataTable();
            dsPaging = SqlHelper.ExecuteDataset(sConStr, spName, objArr);
            dtPaging = dsPaging.Tables[0];
            return dtPaging;
        }
        #endregion

        #region GetDataInDataSet*******************************************************************************************************************************
        /// <summary>
        /// GetDataInDataSet
        /// </summary>
        /// <param name="argQuery">string</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataInDataSet(string argQuery)
        {
            DataSet dtSet = new DataSet();
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                SqlCommand command = new SqlCommand(argQuery, connection);

                command.CommandTimeout = 700;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataAdapter adpt = new SqlDataAdapter(command);

                adpt.Fill(dtSet);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                string lvar_errMsg = ex.Message;
            }
            return dtSet;
        }
        #endregion

    }


}