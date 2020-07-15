using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// OthersPopulateService : Public Class
    /// </summary>
    public class OthersPopulateService : IOthersPopulateService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region OthersPopulateService Constructor**************************************************************************************************************
        /// <summary>
        /// OthersPopulateService : Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public OthersPopulateService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region DeleteOthersDetails****************************************************************************************************************************
        /// <summary>
        /// DeleteOthersDetails
        /// </summary>
        /// <param name="sOthers">clsOtherPopulate</param>
        /// <returns>string</returns>
        public string DeleteOthersDetails(clsOtherPopulate sOthers)
        {
            string flag = "0";
            string sqlOthersDetails = " EXEC USP_DeleteOthersDetails '" + sOthers.id + "'";
            flag = (string)db.GetSingleValue(sqlOthersDetails);
            return flag;
        }
        #endregion

        #region OtherPopulate**********************************************************************************************************************************
        /// <summary>
        /// OtherPopulate
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>List<clsOtherPopulate></returns>
        public List<clsOtherPopulate> OtherPopulate(string id)
        {
            List<clsOtherPopulate> lstDetails = new List<clsOtherPopulate>();
            DataTable dtOtherPopulate = BindOtherPopulate(id);
            for (int index = 0; index <= dtOtherPopulate.Rows.Count - 1; index++)
            {
                clsOtherPopulate Details = new clsOtherPopulate();
                Details.id = Convert.ToString(dtOtherPopulate.Rows[index]["id"]);
                Details.name = Convert.ToString(dtOtherPopulate.Rows[index]["Name"]);
                Details.type = Convert.ToString(dtOtherPopulate.Rows[index]["TYPE"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion

        #region BindOtherPopulate******************************************************************************************************************************
        /// <summary>
        /// BindOtherPopulate
        /// </summary>
        /// <param name="ID">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindOtherPopulate(string id)
        {
            DataTable dtOtherPopulate = new DataTable();
            string sqlOtherPopulate = "SELECT o.[ID] ,o.[NAME], cat.[NAME] AS [TYPE] from M_OTHERSPOPULATE o " +
                        "INNER JOIN M_CATEGORY cat ON o.TYPE=cat.ID where o.TYPE=" + id;
            dtOtherPopulate = db.GetData(sqlOtherPopulate);
            return dtOtherPopulate;
        }
        #endregion

        #region SaveOthersDetails******************************************************************************************************************************
        /// <summary>
        /// SaveOthersDetails
        /// </summary>
        /// <param name="sOthers">clsOtherPopulate</param>
        /// <returns>string</returns>
        public string SaveOthersDetails(clsOtherPopulate sOthers)
        {
            string flag = "0";
            string sqlOtherPopulate = " EXEC USP_SaveOthersDetails '" + sOthers.id + "' ,'" + sOthers.name + "','" + sOthers.type + "'";
            flag = (string)db.GetSingleValue(sqlOtherPopulate);
            return flag;
        }
        #endregion
    }
}
