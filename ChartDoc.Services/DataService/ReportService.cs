using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace ChartDoc.Services.DataService
{
   public class ReportService : IReportService
    {
        #region Instance variables
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        ISharedService _sharedService;
        #endregion

        #region Constructor
        /// <summary>
        /// ReportService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ReportService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            db._configaration = configaration;
            this.logService = logService;
            this._sharedService = sharedService;
        }
        #endregion

        #region Get Party Ledger
        /// <summary>
        /// ReportService : Get Party Ledger
        /// </summary>
        /// <returns></returns>
        public List<clsPartyLedger> GetPartyLedger(string fromDate, string toDate, string patientName)
        {
            List<clsPartyLedger> partyLedgers = new List<clsPartyLedger>();
            DataTable dtpartyLedger = BindPartyLedger(fromDate, toDate, patientName);

            foreach (DataRow item in dtpartyLedger.Rows)
            {
                using (clsPartyLedger clsPartyLedger = new clsPartyLedger())
                {
                    try
                    {
                        clsPartyLedger.date = Convert.ToString(item["Date"]);
                        clsPartyLedger.particulars = Convert.ToString(item["Particulars"]);
                        clsPartyLedger.amount = Convert.ToDecimal(item["Amount"]);
                        clsPartyLedger.txnType = Convert.ToString(item["TxnType"]);
                     
                        partyLedgers.Add(clsPartyLedger);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return partyLedgers;
        }
        #endregion

        #region Get Patient Balance
        /// <summary>
        /// ReportService : Get Patient Balance
        /// </summary>
        /// <returns></returns>
        public List<clsPatientBalance> GetPatientBalance( string patientName)
        {
            List<clsPatientBalance> patientBalances = new List<clsPatientBalance>();
            DataTable dtpatientBalance = BindPatientBalance( patientName);

            foreach (DataRow item in dtpatientBalance.Rows)
            {
                using (clsPatientBalance clsPatientBalance = new clsPatientBalance())
                {
                    try
                    {
                        clsPatientBalance.patientID = Convert.ToString(item["PatientID"]);
                        clsPatientBalance.patientName =_sharedService.Decrypt( Convert.ToString(item["FName"]))+" "+ _sharedService.Decrypt(Convert.ToString(item["LName"]));
                        clsPatientBalance.mobile = _sharedService.Decrypt(Convert.ToString(item["Mobile"]));
                        clsPatientBalance.email = _sharedService.Decrypt(Convert.ToString(item["Email"]));
                        clsPatientBalance.address = _sharedService.Decrypt(Convert.ToString(item["Address"]));
                        clsPatientBalance.totalBillValue = Convert.ToDecimal(item["TotalBillValue"]);
                        clsPatientBalance.alreadyPaid = Convert.ToDecimal(item["AlreadyPaid"]);
                        clsPatientBalance.totalOutstanding = Convert.ToDecimal(item["TotalOutstanding"]); 
                        patientBalances.Add(clsPatientBalance);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return patientBalances;
        }
        #endregion

        #region Private Method
        private DataTable BindPatientBalance( string patientId)
        {
            DataTable dsPatientBalance = new DataTable();
            string sqlPayment = "EXEC USP_GETPATIENTBALANCE '" + patientId + "'";
            try
            {
                dsPatientBalance = db.GetData(sqlPayment);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(ex);
            }
            return dsPatientBalance;
        }

        private DataTable BindPartyLedger(string fromDate, string toDate, string patientId)
        {
            DataTable dtPartyLedger = new DataTable();
            string sqlPayment = "EXEC USP_partyledger '" + fromDate + "','" + toDate + "','" + patientId + "'";
            try
            {
                dtPartyLedger = db.GetData(sqlPayment);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(ex);
            }
            return dtPartyLedger;
        }
        #endregion
    }
}
