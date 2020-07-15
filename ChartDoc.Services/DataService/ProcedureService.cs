using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// public class ProcedureService 
    /// </summary>
    public class ProcedureService : IProcedureService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region ProcedureService Constructor*******************************************************************************************************************
        /// <summary>
        /// ProcedureService
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public ProcedureService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetProcedureByPatientId************************************************************************************************************************
        /// <summary>
        /// GetProcedureByPatientId
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsProcedures></returns>
        public List<clsProcedures> GetProcedureByPatientId(string patientId)
        {
            List<clsProcedures> lstDetails = new List<clsProcedures>();           
            DataTable dtPatient = GetProcedures(patientId);
            for (int index = 0; index <= dtPatient.Rows.Count - 1; index++)
            {
                clsProcedures objProcedures = new clsProcedures();
                objProcedures.patientId = Convert.ToString(dtPatient.Rows[index]["PatientId"]);
                objProcedures.procedureDate = Convert.ToDateTime(dtPatient.Rows[index]["ProcedureDate"]);
                objProcedures.drName = Convert.ToString(dtPatient.Rows[index]["Dr_Name"]);
                objProcedures.drProfile = Convert.ToString(dtPatient.Rows[index]["Dr_Profile"]);
                objProcedures.procedureDesc = Convert.ToString(dtPatient.Rows[index]["ProcedureDesc"]);
                objProcedures.id = Convert.ToInt32(dtPatient.Rows[index]["Id"]);
                lstDetails.Add(objProcedures);
            }
            return lstDetails;
        }
        #endregion

        #region SaveProcedure**********************************************************************************************************************************
        /// <summary>
        /// SaveProcedure
        /// </summary>
        /// <param name="xmlDoc">string</param>
        /// <param name="xmlProcedures">string</param>
        /// <param name="patientId">string</param>
        /// <returns>string</returns>
        public string SaveProcedure(string xmlDoc, string xmlProcedures, string patientId)
        {
            string result = string.Empty;
            string sqlProcedures = " EXEC [USP_SAVE_Procedure] '" + patientId + "','" + xmlProcedures + "','" + xmlDoc + "'";
            result = (string)db.GetSingleValue(sqlProcedures);
            return result;
        }
        #endregion

        #region GetDoctorProfile*******************************************************************************************************************************
        /// <summary>
        /// GetDoctorProfile
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="procedures">clsProcedures</param>
        /// <returns>clsProcedures</returns>
        public clsProcedures GetDoctorProfile(string appointmentId, clsProcedures procedures)
        {
            string res = "";
            string sql = "select ser.SERVICENAME FROM T_Appointment app INNER JOIN M_SERVICE ser ON app.ServiceId=ser.SERVICEID WHERE app.AppointmentId='" + appointmentId + "'";
            res = (string)db.GetSingleValue(sql);
            procedures.drProfile = res;
            return procedures;
        }
        #endregion

        #region Private Method*********************************************************************************************************************************
        /// <summary>
        /// GetProcedures
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable GetProcedures(string patientId)
        {
            DataTable dtProcedures = new DataTable();
            string sqlProcedures = "SELECT [Id],[PatientId] ,[ProcedureDate], [Dr_Name], [Dr_Profile] ,[ProcedureDesc]  FROM [dbo].[T_Procedure] Where PatientId = '" + patientId + "'";
            dtProcedures = db.GetData(sqlProcedures);
            return dtProcedures;
        }
        #endregion
    }
}
