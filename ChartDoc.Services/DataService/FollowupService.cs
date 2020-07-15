using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// FollowupService : class
    /// </summary>
    public class FollowupService : IFollowupService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region FollowupService Constructor********************************************************************************************************************
        /// <summary>
        /// FollowupService : Parameterized Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public FollowupService(IConfiguration configaration, ILogService logService)
        {
            db._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region GetFollowUp************************************************************************************************************************************
        /// <summary>
        /// GetFollowUp
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>List<clsFollowUp></returns>
        public List<clsFollowUp> GetFollowUp(string appointmentId)
        {
            List<clsFollowUp> lstDetails = new List<clsFollowUp>();
            DataTable dtFollowUp = BindFollowUp(appointmentId);
            for (int index = 0; index <= dtFollowUp.Rows.Count - 1; index++)
            {
                clsFollowUp objFollowUp = new clsFollowUp();
                objFollowUp.pId = Convert.ToString(dtFollowUp.Rows[index]["Id"]);
                objFollowUp.pFollowupDate = Convert.ToString(dtFollowUp.Rows[index]["FollowUpDate"]);
                objFollowUp.pFollowupPeriod = Convert.ToString(dtFollowUp.Rows[index]["FollowUpPeriod"]);
                lstDetails.Add(objFollowUp);
            }
            return lstDetails;
        }
        #endregion

        #region SaveFollowup***********************************************************************************************************************************
        /// <summary>
        /// SaveFollowup
        /// </summary>
        /// <param name="objFollowup">clsFollowUp</param>
        /// <returns>string</returns>
        public string SaveFollowup(clsFollowUp objFollowup)
        {
            string result = string.Empty;
            string sqlFollowUpV2 = " EXEC [USP_UPDATE_FollowUp_V2] '" + objFollowup.patientId + "' ,'" + objFollowup.pFollowupDate +
                                                             "','" + objFollowup.pFollowupPeriod + "'";
            result = (string)db.GetSingleValue(sqlFollowUpV2);
            return result;

        }
        #endregion

        #region BindFollowUp***********************************************************************************************************************************
        /// <summary>
        /// returns all FollowUp details for patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private DataTable BindFollowUp(string patientId)
        {
            DataTable dtFollowUp = new DataTable();
            string sqlFollowUp = "SELECT ID,  [FollowUpDate],[FollowUpPeriod] FROM [t_FollowUp] " + " WHERE [AppointmentID]='" + patientId + "'";
            try
            {
                dtFollowUp = db.GetData(sqlFollowUp);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtFollowUp;
        }
        #endregion
    }
}
