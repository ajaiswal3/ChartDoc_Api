using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// ImpressionPlanService : Public Class
    /// </summary>
    public class ImpressionPlanService : IImpressionPlanService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region ImpressionPlanService Constructor**************************************************************************************************************
        /// <summary>
        /// ImpressionPlanService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public ImpressionPlanService(IConfiguration configaration, ILogService logService)
        {
            context._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region GetImpressionPlan******************************************************************************************************************************
        /// <summary>
        /// GetImpressionPlan
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsImpression></returns>
        public List<clsImpression> GetImpressionPlan(string patientId)
        {
            List<clsImpression> lstDetails = new List<clsImpression>();
            DataTable dtImpressionPlan = BindImpressionPlan(patientId);
            for (int index = 0; index <= dtImpressionPlan.Rows.Count - 1; index++)
            {
                clsImpression Details = new clsImpression();
                Details.patientId = Convert.ToString(dtImpressionPlan.Rows[index]["PATIENTID"]);
                Details.description = Convert.ToString(dtImpressionPlan.Rows[index]["IMPRESSION"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion

        #region BindImpressionPlan*****************************************************************************************************************************
        /// <summary>
        /// BindImpressionPlan
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindImpressionPlan(string patientId)
        {
            DataTable dtImpressionPlan = new DataTable();
            string sqlImpressionPlan = "SELECT [PATIENTID],  [IMPRESSION] FROM [T_IMPRESSION] " +
                " WHERE [AppointmentID]='" + patientId + "'";
            try
            {
                dtImpressionPlan = context.GetData(sqlImpressionPlan);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtImpressionPlan;
        }
        #endregion

        #region SaveImpressionPlan*****************************************************************************************************************************
        /// <summary>
        /// SaveImpressionPlan
        /// </summary>
        /// <param name="impression">clsImpression</param>
        /// <returns>string</returns>
        public string SaveImpressionPlan(clsImpression impression)
        {
            string result = string.Empty;
            string sqlImpressionPlan = " EXEC [USP_SaveImpressionPlan_V2] '" + impression.patientId + "' ,'" + impression.description + "'";
            result = (string)context.GetSingleValue(sqlImpressionPlan);
            return result;
        }
        #endregion
    }
}
