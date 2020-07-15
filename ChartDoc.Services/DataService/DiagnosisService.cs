using System;
using System.Data;
using ChartDoc.DAL;
using ChartDoc.Models;
using System.Collections.Generic;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// DiagnosisService : Public Class
    /// </summary>
    public class DiagnosisService : IDiagnosisService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region DiagnosisService Constructor*******************************************************************************************************************
        /// <summary>
        /// DiagnosisService: Public Constructor
        /// </summary>
        /// <param name="configaration"></param>
        public DiagnosisService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetDiagnosisByPatientId************************************************************************************************************************
        /// <summary>
        /// GetDiagnosisByPatientId : Public Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsDiagnosis></returns>
        public List<clsDiagnosis> GetDiagnosisByPatientId(string patientId)
        {
            List<clsDiagnosis> lstDetails = new List<clsDiagnosis>();           
            DataTable dtDiagnosis = GetDiagnosis(patientId);
            for (int index = 0; index <= dtDiagnosis.Rows.Count - 1; index++)
            {
                clsDiagnosis Details = new clsDiagnosis();
                Details.patientId = Convert.ToString(dtDiagnosis.Rows[index]["PatientId"]);
                Details.diagnosisDate = Convert.ToDateTime(dtDiagnosis.Rows[index]["DiagnosisDate"]);
                Details.diagnosisDesc = Convert.ToString(dtDiagnosis.Rows[index]["DiagnosisDesc"]);
                Details.id = Convert.ToInt32(dtDiagnosis.Rows[index]["Id"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion

        #region SaveDiagnosis**********************************************************************************************************************************
        /// <summary>
        /// SaveDiagnosis : Public Method
        /// </summary>
        /// <param name="xmlDoc">string</param>
        /// <param name="xmlDiagnosis">string</param>
        /// <param name="patientId">string</param>
        /// <returns>string</returns>
        public string SaveDiagnosis(string xmlDoc, string xmlDiagnosis, string patientId)
        {
            string result = string.Empty;
            string sql = " EXEC [USP_SAVE_DIAGNOSIS] '" + patientId + "','" + xmlDiagnosis + "','" + xmlDoc + "'";
            result = (string)db.GetSingleValue(sql);
            return result;
        }
        #endregion

        #region Private Method*********************************************************************************************************************************
        /// <summary>
        /// GetDiagnosis : Private Method
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private DataTable GetDiagnosis(string patientId)
        {
            DataTable dtDiagnosis = new DataTable();
            string sqlDiagnosis = "SELECT [Id], [PatientId] ,[DiagnosisDate] ,[DiagnosisDesc]  FROM [dbo].[T_Diagnosis] Where PatientId = '" + patientId + "'";
            dtDiagnosis = db.GetData(sqlDiagnosis);
            return dtDiagnosis;
        }
        #endregion
    }
}
