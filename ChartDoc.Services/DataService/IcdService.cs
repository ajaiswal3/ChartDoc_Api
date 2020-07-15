using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// IcdService : Public Class
    /// </summary>
    public class IcdService : IIcdService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        private readonly ISharedService sharedService;
        #endregion

        #region IcdService Constructor*************************************************************************************************************************
        /// <summary>
        /// IcdService : Parameterized Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="sharedService">ISharedService</param>
        public IcdService(IConfiguration configaration, ISharedService sharedService)
        {
            db._configaration = configaration;
            this.sharedService = sharedService;
        }
        #endregion

        #region GetAllICD**************************************************************************************************************************************
        /// <summary>
        /// GetAllICD
        /// </summary>
        /// <returns>List<clsICD> </returns>
        public List<clsICD> GetAllICD()
        {
            List<clsICD> lstDetails = new List<clsICD>();
            DataTable dtICD = BindICD("M_ICD");
            for (int index = 0; index <= dtICD.Rows.Count - 1; index++)
            {
                clsICD objDetails = new clsICD();
                objDetails.id = Convert.ToString(dtICD.Rows[index]["id"]);
                objDetails.code = Convert.ToString(dtICD.Rows[index]["Code"]);
                objDetails.desc = Convert.ToString(dtICD.Rows[index]["Desc"]);
                lstDetails.Add(objDetails);
            }
            return lstDetails;
        }
        #endregion

        #region GetSavedICD************************************************************************************************************************************
        /// <summary>
        /// GetSavedICD
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsICD></returns>
        public List<clsICD> GetSavedICD(string patientId)
        {
            List<clsICD> lstDetails = new List<clsICD>();
            DataTable dt = p_GetSavedICD(patientId);
            for (int index = 0; index <= dt.Rows.Count - 1; index++)
            {
                clsICD objDetails = new clsICD();
                objDetails.id = Convert.ToString(dt.Rows[index]["Id"]);
                objDetails.patientId = Convert.ToString(dt.Rows[index]["PatientId"]);
                objDetails.code = Convert.ToString(dt.Rows[index]["code"]);
                objDetails.desc = Convert.ToString(dt.Rows[index]["Desc"]);
                lstDetails.Add(objDetails);
            }
            return lstDetails;
        }
        #endregion

        #region SaveICD****************************************************************************************************************************************
        /// <summary>
        /// SaveICD
        /// </summary>
        /// <param name="appointmentId">int</param>
        /// <param name="icd">string</param>
        /// <returns>string</returns>
        public string SaveICD(int appointmentId, string icd)
        {
            string result = string.Empty;            
            string sqlICD = " EXEC [USP_SAVE_ICD_V2] '" + appointmentId + "' ,'" + icd + "'";
            result = (string)db.GetSingleValue(sqlICD);
            return result;
        }
        #endregion

        #region BindICD****************************************************************************************************************************************
        /// <summary>
        /// BindICD
        /// </summary>
        /// <param name="tableName">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindICD(string tableName)
        {
            DataTable dtICD = new DataTable();
            string sqlICD = "SELECT [Id] ,[Code] ,[Desc]  FROM [dbo].[" + tableName + "] ";
            dtICD = db.GetData(sqlICD);
            return dtICD;
        }
        #endregion

        #region p_GetSavedICD**********************************************************************************************************************************
        /// <summary>
        /// p_GetSavedICD
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable p_GetSavedICD(string patientId)
        {
            DataTable dtICD = new DataTable();
            string sqlICD = "USP_GET_ICD_V2 '" + patientId + "'";
            dtICD = db.GetData(sqlICD);
            return dtICD;
        }
        #endregion
    }
}
