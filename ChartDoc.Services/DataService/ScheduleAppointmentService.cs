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
    /// public class ScheduleAppointmentService : IScheduleAppointmentService
    /// </summary>
    public class ScheduleAppointmentService : IScheduleAppointmentService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region ScheduleAppointmentService Constructor*********************************************************************************************************
        /// <summary>
        /// ScheduleAppointmentService : Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ScheduleAppointmentService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region UpdateMarkReady********************************************************************************************************************************
        /// <summary>
        /// UpdateMarkReady
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="flag">string</param>
        /// <returns>string</returns>
        public string UpdateMarkReady(string appointmentId, string flag)
        {
            string result = "1";
            result = (string)db.GetSingleValue("USP_UPDATEMARKREADY '" + appointmentId + "','" + flag + "'");
            return result;
        }
        #endregion

    }
}
