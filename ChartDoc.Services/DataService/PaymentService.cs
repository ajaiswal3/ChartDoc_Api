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
        public clsPayment GetPaymentDetails(string paymentId, string patientId)
        {
            clsPayment claimPayments = new clsPayment();
            DataSet dsPaymentDetails = BindPaymentDetails(paymentId, patientId);

            using (clsPaymentHeader paymentHeader = new clsPaymentHeader())
            {
                try
                {
                    paymentHeader.alreadyPaid = Convert.ToDecimal(dsPaymentDetails.Tables[0].Rows[0]["AlreadyPaid"]);
                    paymentHeader.email = Convert.ToString(dsPaymentDetails.Tables[0].Rows[0]["Email"]);
                    paymentHeader.mobile = Convert.ToString(dsPaymentDetails.Tables[0].Rows[0]["Mobile"]);
                    paymentHeader.address = Convert.ToString(dsPaymentDetails.Tables[0].Rows[0]["address"]); 
                    // paymentHeader.modeOfPayment = Convert.ToString(dtPaymentDetails.Rows[0]["Mode_of_Payment"]);
                    paymentHeader.patientId = Convert.ToString(dsPaymentDetails.Tables[0].Rows[0]["PatientID"]);
                    paymentHeader.patientName = Convert.ToString(dsPaymentDetails.Tables[0].Rows[0]["PatientName"]);
                    paymentHeader.totalBillValue = Convert.ToDecimal(dsPaymentDetails.Tables[0].Rows[0]["TotalBillValue"]);
                    paymentHeader.totalOutstanding = Convert.ToDecimal(dsPaymentDetails.Tables[0].Rows[0]["TotalOutstanding"]);
                    claimPayments.paymentHeader = paymentHeader;
                }
                catch (Exception ex)
                {
                    var logger = logService.GetLogger(typeof(ClaimService));
                    logger.Error(ex);
                }
            }
            if (dsPaymentDetails.Tables[1].Rows.Count > 0)
            {
                claimPayments.paymentDetails = new List<clsPaymentDetails>();
                foreach (DataRow item in dsPaymentDetails.Tables[1].Rows)
                {
                    using (clsPaymentDetails paymentDetails = new clsPaymentDetails())
                    {
                        try
                        {
                            paymentDetails.paymentDetailId = Convert.ToString(item["id"]); 
                            paymentDetails.amount = Convert.ToDecimal(item["Amount"]);
                            paymentDetails.instrumentTypeId = Convert.ToInt32(item["INSTRUMENTTYPEID"]);
                            paymentDetails.instrumentTypeName = Convert.ToString(item["INSTRUMENTTYPEName"]);
                            paymentDetails.patientId = Convert.ToString(item["PATIENTID"]);
                            paymentDetails.paymentDate = Convert.ToString(item["PAYMENTDATE"]);
                            paymentDetails.reasonId = Convert.ToInt32(item["REASONID"]);
                            paymentDetails.reasonName = Convert.ToString(item["ReasonName"]);
                            paymentDetails.ref1 = Convert.ToString(item["REF1"]);
                            paymentDetails.ref2 = Convert.ToString(item["REF2"]);
                            paymentDetails.transferId = Convert.ToString(item["TRANSFERID"]);
                            paymentDetails.transferName = Convert.ToString(item["TRANSFERName"]);
                            paymentDetails.typeOfTxnId = Convert.ToInt32(item["TYPEOFTXNID"]);
                            paymentDetails.typeOfTxnName = Convert.ToString(item["TYPEOFTXNNAME"]);

                            claimPayments.paymentDetails.Add(paymentDetails);
                        }
                        catch (Exception ex)
                        {
                            var logger = logService.GetLogger(typeof(ClaimService));
                            logger.Error(ex);
                        }
                    }
                }
            }
            if (dsPaymentDetails.Tables[2].Rows.Count > 0)
            {
                claimPayments.paymentBreakup = new List<clsPaymentBreakup>();
                foreach (DataRow item in dsPaymentDetails.Tables[2].Rows)
                {
                    using (clsPaymentBreakup paymentBreakup = new clsPaymentBreakup())
                    {
                        try
                        {
                            paymentBreakup.id = Convert.ToInt32(item["ID"]);
                            paymentBreakup.payId = Convert.ToInt32(item["PAYID"]);
                            paymentBreakup.freeTicketNo=Convert.ToString(item["FreeTicketNo"]);
                            paymentBreakup.appointmentDate = Convert.ToString(item["AppointmentDate"]);
                            paymentBreakup.appointmentId = Convert.ToInt32(item["APPOINTMENTID"]);
                            paymentBreakup.amount = Convert.ToDecimal(item["AMOUNT"]);//due amount  
                            paymentBreakup.paidAmount = Convert.ToDecimal(item["paidAmount"]);//due amount  
                            claimPayments.paymentBreakup.Add(paymentBreakup);
                        }
                        catch (Exception ex)
                        {
                            var logger = logService.GetLogger(typeof(ClaimService));
                            logger.Error(ex);
                        }
                    }
                }

            }
            return claimPayments;
        }
        #endregion

        #region Get Payment Details
        /// <summary>
        /// PaymentService : Get Payment Details
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public List<clsPaymentBreakup> GetPaymentBreakUp(string paymentId, string patientId)
        {
            List<clsPaymentBreakup> claimPaymentsBreakUp = new List<clsPaymentBreakup>();
            DataTable dtPaymentBreakUp = BindPaymentBreakUp(paymentId, patientId);

            
            if (dtPaymentBreakUp != null && dtPaymentBreakUp.Rows.Count > 0)
            {
                foreach (DataRow item in dtPaymentBreakUp.Rows)
                {
                    using (clsPaymentBreakup paymentBreakup = new clsPaymentBreakup())
                    {
                        try
                        {
                            paymentBreakup.id = Convert.ToInt32(item["ID"]);
                            paymentBreakup.payId = Convert.ToInt32(item["PAYID"]);
                            paymentBreakup.freeTicketNo = Convert.ToString(item["FreeTicketNo"]);
                            paymentBreakup.appointmentDate = Convert.ToString(item["AppointmentDate"]);
                            paymentBreakup.appointmentId = Convert.ToInt32(item["APPOINTMENTID"]);
                            paymentBreakup.amount = Convert.ToDecimal(item["AMOUNT"]);//due amount  
                            paymentBreakup.paidAmount = Convert.ToDecimal(item["paidAmount"]);//due amount  
                            claimPaymentsBreakUp.Add(paymentBreakup);
                        }
                        catch (Exception ex)
                        {
                            var logger = logService.GetLogger(typeof(ClaimService));
                            logger.Error(ex);
                        }
                    }
                }

            }
            return claimPaymentsBreakUp;
        }
        #endregion

        #region Get Payment List
        /// <summary>
        /// PaymentService : Get Payment List
        /// </summary>
        /// <returns></returns>
        public List<clsPaymentDetails> GetPaymentList(string fromDate, string toDate, string patientName)
        {
            List<clsPaymentDetails> paymentDetails = new List<clsPaymentDetails>();
            DataTable dtPayment = BindPaymentList(fromDate, toDate, patientName);

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
                        objPaymentDetails.PatientName = Convert.ToString(item["PatientName"]); 
                        objPaymentDetails.paymentDate = Convert.ToString(item["PAYMENTDATE"]);
                        objPaymentDetails.reasonId = Convert.ToInt32(item["REASONID"]);
                        objPaymentDetails.reasonName = Convert.ToString(item["REASONDESCRIPTOON"]);
                        objPaymentDetails.ref1 = Convert.ToString(item["REF1"]);
                        objPaymentDetails.ref2 = Convert.ToString(item["REF2"]);
                        objPaymentDetails.transferId = Convert.ToString(item["TRANSFERTO"]);
                        objPaymentDetails.transferName = Convert.ToString(item["TRANSFERTOName"]);
                        objPaymentDetails.typeOfTxnId = Convert.ToInt32(item["TYPEOFTXN"]);
                        objPaymentDetails.typeOfTxnName = Convert.ToString(item["TYPEOFTXNName"]);
                        objPaymentDetails.paymentId = Convert.ToString(item["PAYMENTID"]); 
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
        public string SavePaymentDetails(int paymentId, string xmlPaymentDetails, string xmlPaymentBreakup,string isdelete)
        {
            string result = "0";
            string sqlPayment = "EXEC USP_SavePaymentDetails " + paymentId + ",'" + xmlPaymentDetails + "','" + xmlPaymentBreakup + "','" + isdelete + "'";
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
        private DataSet BindPaymentDetails(string paymentId, string patientId)
        {
            DataSet dsPayment = new DataSet();
            string sqlPayment = "EXEC USP_GetPaymentDetails " + paymentId + ",'" + patientId + "'";
            try
            {
                dsPayment = db.GetDataInDataSet(sqlPayment);
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
            return dsPayment;
        }

        private DataTable BindPaymentList(string fromDate,string toDate,string patientId)
        {
            DataTable dtPayment = new DataTable();
            string sqlPayment = "EXEC USP_GETPAYMENTLIST '" + fromDate + "','" + toDate + "','" + patientId + "'";
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

        private DataTable BindPaymentBreakUp(string paymentId, string patientId)
        {
            DataTable dtPaymentBreakUp = new DataTable();
            string sqlPayment = "EXEC USP_GetPaymentBreakUp " + paymentId + ",'" + patientId + "'";
            try
            {
                dtPaymentBreakUp = db.GetData(sqlPayment);
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
            return dtPaymentBreakUp;
        }
        #endregion

    }
}
