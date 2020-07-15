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
    /// public class ReasonService : IReasonService
    /// </summary>
    public class ReasonService : IReasonService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region ReasonService Constructor**********************************************************************************************************************
        /// <summary>
        /// ReasonService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public ReasonService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        #endregion

        #region DeleteReason***********************************************************************************************************************************
        /// <summary>
        /// DeleteReason
        /// </summary>
        /// <param name="sReason">clsReason</param>
        /// <returns>string</returns>
        public string DeleteReason(clsReason sReason)
        {
            string flag = "0";
            string sqlReason = " EXEC USP_DeleteReason   '" + sReason.reasonId + "'";
            flag = (string)context.GetSingleValue(sqlReason);
            return flag;
        }
        #endregion

        #region GetReason**************************************************************************************************************************************
        /// <summary>
        /// GetReason
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>List<clsReason></returns>
        public List<clsReason> GetReason(string type)
        {
            List<clsReason> lstDetails = new List<clsReason>();
            DataTable dtReason = BindReason(type);
            for (int index = 0; index <= dtReason.Rows.Count - 1; index++)
            {
                clsReason objReason = new clsReason();
                objReason.reasonId = Convert.ToInt32(dtReason.Rows[index]["REASONID"]);
                objReason.reasonCode = Convert.ToString(dtReason.Rows[index]["REASONCODE"]);
                objReason.reasonDescription = Convert.ToString(dtReason.Rows[index]["REASONDESCRIPTOON"]);
                lstDetails.Add(objReason);
            }
            return lstDetails;
        }
        #endregion

        #region SaveReason*************************************************************************************************************************************
        /// <summary>
        /// SaveReason
        /// </summary>
        /// <param name="sReason">clsReason</param>
        /// <returns>string</returns>
        public string SaveReason(clsReason sReason)
        {
            string flag = "0";
            string sqlReason = " EXEC USP_SaveReason   '" + sReason.reasonId + "', '" + sReason.reasonCode + "','" + sReason.reasonDescription + "','" + sReason.type + "'";
            flag = (string)context.GetSingleValue(sqlReason);
            return flag;
        }
        #endregion

        #region BindReason*************************************************************************************************************************************
        /// <summary>
        /// BindReason
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindReason(string type)
        {
            DataTable dtReason = new DataTable();
            string sqlReason = "USP_FETCHREASON '" + type + "'";
            dtReason = context.GetData(sqlReason);
            return dtReason;
        }
        #endregion
    }
}
