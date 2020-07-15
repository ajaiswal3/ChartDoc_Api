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
    /// CoPayService : Public Class
    /// </summary>
    public class CoPayService : ICoPayService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region CoPayService Constructor***********************************************************************************************************************
        public CoPayService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetCOPay***************************************************************************************************************************************
        /// <summary>
        /// GetCOPay : Public Method
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>List<clsCOPay></returns>
        public List<clsCOPay> GetCOPay(string appointmentId)
        {
            List<clsCOPay> cOPayDetailsList = new List<clsCOPay>();
            DataTable dtCOPay = BindCoPAy(appointmentId);
            for (int index = 0; index <= dtCOPay.Rows.Count - 1; index++)
            {
                clsCOPay Details = new clsCOPay();
                Details.amount = Convert.ToString(dtCOPay.Rows[index]["AMOUNT"]);
                Details.paymentDate = Convert.ToString(dtCOPay.Rows[index]["PAYMENTDATE"]);
                Details.refNo1 = Convert.ToString(dtCOPay.Rows[index]["REFNO1"]);
                Details.refNo2 = Convert.ToString(dtCOPay.Rows[index]["REFNO2"]);
                Details.paymentType = Convert.ToString(dtCOPay.Rows[index]["NAME"]);
                Details.patientId = Convert.ToString(dtCOPay.Rows[index]["PatientId"]);
                cOPayDetailsList.Add(Details);
            }
            return cOPayDetailsList;
        }
        #endregion

        #region BindCoPAy**************************************************************************************************************************************
        /// <summary>
        /// BindCoPAy : Private Method
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindCoPAy(string appointmentId)
        {
            DataTable dtCoPAy = new DataTable();
            string sqlCoPAy = "SELECT  [NAME],[REFNO1],[PatientID],[REFNO2],[PAYMENTDATE],[AMOUNT] FROM[Vw_COPay] where AppointmentId=" + appointmentId;
            dtCoPAy = db.GetData(sqlCoPAy);
            return dtCoPAy;
        }
        #endregion

        #region SaveCoPay**************************************************************************************************************************************
        /// <summary>
        /// SaveCoPay : Public Method
        /// </summary>
        /// <param name="objCpay">clsCOPay</param>
        /// <returns>string</returns>
        public string SaveCoPay(clsCOPay objCpay)
        {
            string result = string.Empty;
            string sql = "EXEC   USP_SAVE_COPAY '" + objCpay.patientId + "','" 
                + objCpay.paymentType + "','" 
                + objCpay.refNo1 + "','" 
                + objCpay.refNo2 + "','" 
                + objCpay.paymentDate + "','" 
                + objCpay.amount + "','" 
                + objCpay.appointmentId + "'";
            result = (string)db.GetSingleValue(sql);
            return result;
        }
        #endregion
    }
}
