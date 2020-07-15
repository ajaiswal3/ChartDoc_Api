using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// CptService : Public Class
    /// </summary>
    public class CptService : ICptService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region CptService Constructor*************************************************************************************************************************
        public CptService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetCPT*****************************************************************************************************************************************
        /// <summary>
        /// GetCPT : Public Method
        /// </summary>
        /// <returns>List<clsCPT></returns>
        public List<clsCPT> GetCPT()
        {
            List<clsCPT> lstDetails = new List<clsCPT>();
            DataTable dtCPT = BindICD("M_CPT");
            for (int index = 0; index <= dtCPT.Rows.Count - 1; index++)
            {
                using (clsCPT details = new clsCPT())
                {
                    details.id = Convert.ToString(dtCPT.Rows[index]["id"]);
                    details.code = Convert.ToString(dtCPT.Rows[index]["Code"]);
                    details.desc = Convert.ToString(dtCPT.Rows[index]["Desc"]);
                    lstDetails.Add(details);
                }
            }
            return lstDetails;
        }
        /// <summary>
        /// GetCPT : Public Method
        /// </summary>
        /// <returns>List<clsCPT></returns>
        public List<clsCPT> GetCPTWITHChargeAmount()
        {
            List<clsCPT> lstDetails = new List<clsCPT>();
            DataTable dtCPT = BindCPTWITHChargeAmount();
            for (int index = 0; index <= dtCPT.Rows.Count - 1; index++)
            {
                using (clsCPT details = new clsCPT())
                {
                    details.id = Convert.ToString(dtCPT.Rows[index]["id"]);
                    details.code = Convert.ToString(dtCPT.Rows[index]["Code"]);
                    details.desc = Convert.ToString(dtCPT.Rows[index]["Desc"]);
                    details.chargeAmount = Convert.ToDecimal(dtCPT.Rows[index]["ChargeAmount"]);
                    lstDetails.Add(details);
                }
            }
            return lstDetails;
        }
        #endregion

        #region GetSavedCPT************************************************************************************************************************************
        /// <summary>
        /// GetSavedCPT : Public Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsCPT></returns>
        public List<clsCPT> GetSavedCPT(string patientId)
        {
            List<clsCPT> lstDetails = new List<clsCPT>();
            DataTable dtCPT = GetPatientSavedCPT(patientId);
            for (int index = 0; index <= dtCPT.Rows.Count - 1; index++)
            {
                using (clsCPT details = new clsCPT())
                {
                    details.id = Convert.ToString(dtCPT.Rows[index]["Id"]);
                    details.patientId = Convert.ToString(dtCPT.Rows[index]["PatientId"]);
                    details.code = Convert.ToString(dtCPT.Rows[index]["code"]);
                    details.desc = Convert.ToString(dtCPT.Rows[index]["Desc"]);
                    lstDetails.Add(details);
                }
            }
            return lstDetails;
        }
        #endregion

        #region SaveCPT****************************************************************************************************************************************
        /// <summary>
        /// SaveCPT : Public Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="cpt">string</param>
        /// <returns>string</returns>
        public string SaveCPT(string patientId, string cpt)
        {
            string result = string.Empty;
            string sql = " EXEC [USP_SAVE_CPT_V2] '" + patientId + "' ,'" + cpt + "'";
            result = (string)db.GetSingleValue(sql);
            return result;
        }
        #endregion

        #region BindICD****************************************************************************************************************************************
        /// <summary>
        /// BindICD : Private Method
        /// </summary>
        /// <param name="tableName">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindICD(string tableName)
        {
            DataTable dtICD = new DataTable();
            string sql = "SELECT [Id] ,[Code] ,[Desc]  FROM [dbo].[" + tableName + "] ";
            dtICD = db.GetData(sql);
            return dtICD;
        }
        #endregion

        #region GetPatientSavedCPT*****************************************************************************************************************************
        /// <summary>
        /// GetPatientSavedCPT : Private Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable GetPatientSavedCPT(string patientId)
        {
            DataTable dtCPT = new DataTable();
            string sql = " USP_GET_CPT_V2 '" + patientId + "'";
            dtCPT = db.GetData(sql);
            return dtCPT;
        }
        #endregion

        #region CPTWITHChargeAmount****************************************************************************************************************************************
        /// <summary>
        /// CPTWITHChargeAmount : Private Method
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindCPTWITHChargeAmount()
        {
            DataTable dtICD = new DataTable();
            string sql = "exec [USP_GetCPTWITHChargeAmount]";
            dtICD = db.GetData(sql);
            return dtICD;
        }
        #endregion
    }
}
