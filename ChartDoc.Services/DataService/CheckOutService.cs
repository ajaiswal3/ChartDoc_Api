using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// CheckOutService : Public Class
    /// </summary>
    public class CheckOutService : ICheckOutService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region CheckOutService Constructor********************************************************************************************************************
        /// <summary>
        /// CheckOutService : Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public CheckOutService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region UpdateCheckOut*********************************************************************************************************************************
        /// <summary>
        /// UpdateCheckOut : Public Method
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="flg">string</param>
        /// <returns>string</returns>
        public string UpdateCheckOut(string appointmentId, string flg)
        {
            string result = string.Empty;
            result = (string)db.GetSingleValue("USP_UPDATECheckOut '" + appointmentId + "','" + flg + "'");
            return result;
        }
        #endregion
    }
}
