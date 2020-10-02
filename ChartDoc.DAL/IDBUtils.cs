#region Developed By
 ////Comapany Name :Ontrack System Limited
 ////Developed By Biman Mitra
 /// Date :03/08/2007
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ChartDoc.DAL
{
    /// <summary>
    /// IDBUtils : THIS INTERFACE WILL MAINTAIN DAL RELATED CONSISTENCY, BASICALLY HERE WE ARE DEFINING RULES FOR OUR DAL.
    /// </summary>
	interface IDBUtils
	{
        #region Notes
        //THIS INTERFACE WILL MAINTAIN DAL RELATED CONSISTENCY
        //BASICALLY HERE WE ARE DEFINING RULES FOR OUR DAL
        #endregion Notes

		string GetConnectionString();
		object GetNull(object Field);
		Hashtable GetTableColumnDetail(SqlDataReader reader);
		object GetSingleValue(string vSql, string SPName);
		object GetSingleValue(string vSql);
		DataSet ExecuteDs(string sParamnm, string vSql, string SPName);
		DataSet ExecuteDs(string vSql);
		SqlDataReader ExecuteDr(string vSql, string SPName);
		SqlDataReader ExecuteDr(string vSql);
		DataSet ExecutePagingSql(string sConStr, string spName, object[] objArr);
		ArrayList GetSPParams(string SpName);
		int HandleData(ArrayList objArr, string SpName);
		int HandleData(string Sql);
        string HandleDataWithReturnParams(ArrayList objArr, string SpName);
        SqlDataReader HandleDataWithDataReader(ArrayList objArr, string SpName);
		DataTable FillDatatableForCrpt(string vSql, DataTable tmpTbl);
        object ExecuteData(ArrayList arrLst, string sSpName);
    }

    public interface Interface1{}
}
