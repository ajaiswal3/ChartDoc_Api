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
    public class PaymentService : IPaymentService
    {
        #region Instance variables
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// ClaimService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public PaymentService(IConfiguration configaration, ILogService logService)
        {
            db._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region Public Method
        #region Get Payment Details
        /// <summary>
        /// PaymentService : Get Payment Details
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public clsPayment GetPaymentDetails(string patientId)
        {
            clsPayment claimPayments = new clsPayment();
            DataTable dtPaymentDetails = BindPaymentDetails(patientId);

            using (clsPaymentHeader paymentHeader = new clsPaymentHeader())
            {
                try
                {
                    paymentHeader.alreadyPaid = Convert.ToDecimal(dtPaymentDetails.Rows[0]["AlreadyPaid"]);
                    paymentHeader.email = Convert.ToString(dtPaymentDetails.Rows[0]["Email"]);
                    paymentHeader.mobile = Convert.ToString(dtPaymentDetails.Rows[0]["Mobile"]);
                    paymentHeader.modeOfPayment = Convert.ToString(dtPaymentDetails.Rows[0]["Mode_of_Payment"]);
                    paymentHeader.patientId = Convert.ToString(dtPaymentDetails.Rows[0]["PatientID"]);
                    paymentHeader.patientName = Convert.ToString(dtPaymentDetails.Rows[0]["PatientName"]);
                    paymentHeader.totalBillValue = Convert.ToDecimal(dtPaymentDetails.Rows[0]["TotalBillValue"]);
                    paymentHeader.totalOutstanding = Convert.ToDecimal(dtPaymentDetails.Rows[0]["TotalOutstanding"]);
                    claimPayments.paymentHeader = paymentHeader;
                }
                catch(Exception ex)
                {
                    var logger = logService.GetLogger(typeof(ClaimService));
                    logger.Error(ex);
                }
            }
            foreach (DataRow item in dtPaymentDetails.Rows)
            {
                using (clsPaymentDetails paymentDetails = new clsPaymentDetails())
                {
                    try
                    {
                        paymentDetails.amount = Convert.ToDecimal(item["Amount"]);
                        paymentDetails.instrumentTypeId = Convert.ToInt32(item["INSTRUMENTTYPEID"]);
                        paymentDetails.instrumentTypeName = Convert.ToString(item["INSTRUMENTTYPEName"]);
                        paymentDetails.patientId = Convert.ToString(item["PATIENTID"]);
                        paymentDetails.paymentDate = DateTime.Parse(Convert.ToString(item["PAYMENTDATE"]));
                        paymentDetails.reasonId = Convert.ToInt32(item["REASONID"]);
                        paymentDetails.reasonName = Convert.ToString(item["ReasonName"]);
                        paymentDetails.ref1 = Convert.ToString(item["REF1"]);
                        paymentDetails.ref2 = Convert.ToString(item["REF2"]);
                        paymentDetails.transferId = Convert.ToInt32(item["TRANSFERID"]);
                        paymentDetails.transferName = Convert.ToString(item["TRANSFERName"]);
                        paymentDetails.typeOfTxnId = Convert.ToInt32(item["TYPEOFTXNID"]);
                        paymentDetails.typeOfTxnName = Convert.ToString(item["TYPEOFTXNNAME"]);

                        claimPayments.paymentDetails.Add(paymentDetails);
                    }
                    catch(Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }

                using (clsPaymentBreakup paymentBreakup = new clsPaymentBreakup())
                {
                    try
                    {
                        paymentBreakup.amount = Convert.ToDecimal(item["AMOUNT"]);
                        paymentBreakup.appointmentId = Convert.ToInt32(item["APPOINTMENTID"]);
                        paymentBreakup.id = Convert.ToInt32(item["ID"]);
                        paymentBreakup.payId = Convert.ToInt32(item["PAYID"]);
                        claimPayments.paymentBreakup.Add(paymentBreakup);
                    }
                    catch(Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return claimPayments;
        }
        #endregion

        #region Get Payment List
        /// <summary>
        /// PaymentService : Get Payment List
        /// </summary>
        /// <returns></returns>
        public List<clsPaymentDetails> GetPaymentList()
        {
            List<clsPaymentDetails> paymentDetails = new List<clsPaymentDetails>();
            DataTable dtPayment = BindPaymentList();

            foreach (DataRow item in dtPayment.Rows)
            {
                using (clsPaymentDetails objPaymentDetails = new clsPaymentDetails())
                {
                    try
                    {
                        objPaymentDetails.amount = Convert.ToDecimal(item["AMOUNT"]);
                        objPaymentDetails.instrumentTypeId = Convert.ToInt32(item["INSTRUMENTTYPEID"]);
                        objPaymentDetails.instrumentTypeName = Convert.ToString(item["INSTRUMENTTYPENAME"]);
                        objPaymentDetails.patientId = Convert.ToString(item["PATIENTID"]);
                        objPaymentDetails.paymentDate = DateTime.Parse(Convert.ToString(item["PAYMENTDATE"]));
                        objPaymentDetails.reasonId = Convert.ToInt32(item["REASONID"]);
                        objPaymentDetails.reasonName = Convert.ToString(item["REASONDESCRIPTOON"]);
                        objPaymentDetails.ref1 = Convert.ToString(item["REF1"]);
                        objPaymentDetails.ref2 = Convert.ToString(item["REF2"]);
                        objPaymentDetails.transferId = Convert.ToInt32(item["TRANSFERTO"]);
                        objPaymentDetails.transferName = Convert.ToString(item["TRANSFERTOName"]);
                        objPaymentDetails.typeOfTxnId = Convert.ToInt32(item["TYPEOFTXN"]);
                        objPaymentDetails.typeOfTxnName = Convert.ToString(item["TYPEOFTXNName"]);
                        paymentDetails.Add(objPaymentDetails);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return paymentDetails;
        }
        #endregion

        #region Save Payment Details
        public string SavePaymentDetails(int paymentId, string xmlPaymentDetails, string xmlPaymentBreakup)
        {
            string result = "0";
            string sqlPayment = "EXEC USP_SavePaymentDetails " + paymentId + ",'" + xmlPaymentDetails + "','" + xmlPaymentBreakup + "'";
            try
            {
                result = (string)db.GetSingleValue(sqlPayment);
            }
            catch (SqlException sqlEx)
            {
                result = sqlEx.Message;
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                result = nullEx.Message;
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                var logger = logService.GetLogger(typeof(PaymentService));
                logger.Error(ex);
            }
            return result;
        }
        #endregion

        #endregion

        #region Private Method
        private DataTable BindPaymentDetails(string patientId)
        {
            DataTable dtPayment = new DataTable();
            string sqlPayment = "EXEC USP_GetPaymentDetails '" + patientId + "'";
            try
            {
                dtPayment = db.GetData(sqlPayment);
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
            return dtPayment;
        }

        private DataTable BindPaymentList()
        {
            DataTable dtPayment = new DataTable();
            string sqlPayment = "EXEC USP_GETPAYMENTLIST";
            try
            {
                dtPayment = db.GetData(sqlPayment);
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
            return dtPayment;
        }
        #endregion

    }
}
