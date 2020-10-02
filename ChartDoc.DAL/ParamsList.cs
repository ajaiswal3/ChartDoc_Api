#region Developed By
////Comapany Name :Ontrack System Limited
////Developed By Biman Mitra
/// Date :16/07/2007
#endregion

using System;
using System.Data;


namespace ChartDoc.DAL
{
    /// <summary>
    /// ParamsList : THIS CLASS WILL BE USED FOR STORING STOREPROCEDURE PARAMS RELATED INFO.
    /// </summary>
	public class ParamsList
    {
        #region Notes
        //THIS CLASS WILL BE USED FOR STORING STOREPROCEDURE PARAMS RELATED INFO
        #endregion Notes

        #region Parameterised Constructor**********************************************************************************************************************
        /// <summary>
        /// ParamsList : Parameterised Constructor
        /// </summary>
        /// <param name="paramName">string</param>
        /// <param name="paramvalue">object</param>
        /// <param name="paramType">SqlDbType</param>
        /// <param name="paramDirection">ParameterDirection</param>
        public ParamsList(string paramName, object paramvalue, SqlDbType paramType, ParameterDirection paramDirection)
        {
            this.ParamName = paramName;
            this.ParamValue = paramvalue;
            this.ParamType = paramType;
            this.ParamDirection = paramDirection;
        }
        #endregion

        #region Default Constructor****************************************************************************************************************************
        /// <summary>
        /// ParamsList : Default constructor
        /// </summary>
        public ParamsList()
        {
            this.ParamName = string.Empty;
            this.ParamValue = string.Empty;
            this.ParamType = SqlDbType.VarChar;
            this.ParamDirection = ParameterDirection.Input;
        }
        #endregion

        #region Public Properties******************************************************************************************************************************
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public SqlDbType ParamType { get; set; }
        public ParameterDirection ParamDirection { get; set; }
        #endregion
    }
}
