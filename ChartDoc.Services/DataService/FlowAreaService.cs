using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// FlowAreaService
    /// </summary>
    public class FlowAreaService : IFlowAreaService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region FlowAreaService********************************************************************************************************************************
        /// <summary>
        /// FlowAreaService
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public FlowAreaService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region UpdateFlowArea*********************************************************************************************************************************
        /// <summary>
        /// UpdateFlowArea
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="roomNo">string</param>
        /// <param name="flowArea">string</param>
        /// <returns>string</returns>
        public string UpdateFlowArea(string appointmentId, string roomNo, string flowArea)
        {
            string result = "1";
            result = (string)db.GetSingleValue("USP_UpdateFloorArea '" + appointmentId + "','" + roomNo + "','" + flowArea + "'");
            return result;
        }
        #endregion
    }
}
