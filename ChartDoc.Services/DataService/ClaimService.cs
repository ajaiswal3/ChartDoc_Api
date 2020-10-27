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
    public class ClaimService : IClaimService
    {
        #region Instance variables
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        ISharedService _sharedService;
        #endregion

        #region Constructor
        /// <summary>
        /// ClaimService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ClaimService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            db._configaration = configaration;
            this.logService = logService;
            this._sharedService = sharedService;
        }
        #endregion

        #region Public Method
        #region Get Completed Appointment List
        /// <summary>
        /// ClaimService : Get Completed Appointment List
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="providerId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public List<clsClaimHeader> GetAppointmentList(string fromDate, string toDate, string providerId, string statusId, string patientName, string feeTicket)
        {
            List<clsClaimHeader> claimHeaders = new List<clsClaimHeader>();
            DataTable dtAppointments = BindAppointmentList(fromDate, toDate, providerId, statusId, patientName, feeTicket);

            foreach (DataRow item in dtAppointments.Rows)
            {
                using (clsClaimHeader claimHeader = new clsClaimHeader())
                {
                    try
                    {
                        claimHeader.appointmentId = Convert.ToInt32(item["AppointmentID"]);
                        claimHeader.claimBalance = Convert.ToDecimal(item["ClaimBalance"]);
                        claimHeader.date = Convert.ToString(Convert.ToString(item["Date"]));
                        claimHeader.feeTicketNo = Convert.ToString(item["AppointmentNo"]);
                        claimHeader.patientBalance = Convert.ToDecimal(item["PatientBalance"]);
                        claimHeader.patientId = Convert.ToString(item["PatientId"]);
                        claimHeader.patientName =_sharedService.Decrypt( Convert.ToString(item["PatientName"]));
                        claimHeader.status = Convert.ToString(item["Status"]);
                        claimHeader.statusId = Convert.ToInt32(item["StatusID"]);

                        claimHeaders.Add(claimHeader);
                    }
                    catch(Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return claimHeaders;
        }
        #endregion

        #region Get Charge Patient Header
        /// <summary>
        /// ClaimService : Get Charge Patient Header
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public clsClaimHeader GetChargePatientHeader(int appointmentId)
        {
            List<clsClaimHeader> claimHeaders = new List<clsClaimHeader>();
            DataTable dtChargePatientHeader = BindChargePatientHeader(appointmentId);
            foreach (DataRow item in dtChargePatientHeader.Rows)
            {
                using (clsClaimHeader claimHeader = new clsClaimHeader())
                {
                    try
                    {
                        claimHeader.chargeId = Convert.ToInt32(item["ChargeID"]);
                        claimHeader.date = Convert.ToString(Convert.ToString(item["Date"]));
                        claimHeader.appointmentId = Convert.ToInt32(item["AppointmentID"]);
                        claimHeader.modeOfTransaction = Convert.ToInt32(item["ModeofTransaction"]);
                        claimHeader.patientId = Convert.ToString(item["PatientID"]);
                        claimHeader.patientName =_sharedService.Decrypt( Convert.ToString(item["PatientName"]));
                        claimHeader.doctorId = Convert.ToString(item["DoctorID"]);
                        claimHeader.doctorName = Convert.ToString(item["DoctorName"]);
                        claimHeader.insuranceId1 = Convert.ToInt32(item["InsuranceID_1"]);
                        claimHeader.insuranceName1 = Convert.ToString(item["InsuranceID_Name_1"]);
                        claimHeader.insuranceId2 = Convert.ToInt32(item["InsuranceID_2"]);
                        claimHeader.insuranceName2 = Convert.ToString(item["InsuranceID_Name_2"]);
                        claimHeader.insuranceId3 = Convert.ToInt32(item["InsuranceID_3"]);
                        claimHeader.insuranceName3 = Convert.ToString(item["InsuranceID_Name_3"]);
                        claimHeader.locationId = Convert.ToInt32(item["LocationID"]);
                        claimHeader.locationName = Convert.ToString(item["LocationName"]);
                        claimHeader.placeOfId = Convert.ToInt32(item["PlaceofID"]);
                        claimHeader.placeOfName = Convert.ToString(item["PlaceofName"]);
                        claimHeader.serviceId = Convert.ToInt32(item["ServiceID"]);
                        claimHeader.serviceName = Convert.ToString(item["ServiceName"]);
                        claimHeader.referenceId = Convert.ToInt32(item["ReferenceID"]);
                        claimHeader.referenceName = Convert.ToString(item["ReferenceName"]);
                        claimHeader.fileAsId = Convert.ToInt32(item["FileAsID"]);
                        claimHeader.fileAsName = Convert.ToString(item["FileAsName"]);
                        claimHeader.reasonId = Convert.ToInt32(item["REASONID"]);
                        if (Convert.ToString(item["Policy_1"]) != "-1")
                            claimHeader.policy1 =_sharedService.Decrypt( Convert.ToString(item["Policy_1"]));
                        if(Convert.ToString(item["Policy_2"])!="-1")
                               claimHeader.policy2 = _sharedService.Decrypt(Convert.ToString(item["Policy_2"]));
                        if (Convert.ToString(item["Policy_3"]) != "-1")
                            claimHeader.policy3 = _sharedService.Decrypt(Convert.ToString(item["Policy_3"]));
                        claimHeader.status = Convert.ToString(item["Remarks"]);
                        claimHeader.typeEM= Convert.ToString(item["typeEM"]);
                        claimHeaders.Add(claimHeader);
                    }
                    catch(Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return claimHeaders[0];
        }
        #endregion

        #region Get Charge Patient Details
        /// <summary>
        /// ClaimService : Get Charge Patient Details
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public List<clsClaimDetails> GetChargePatientDetails(int appointmentId)
        {
            List<clsClaimDetails> claimDetails = new List<clsClaimDetails>();
            DataTable dtChargePatientDetails = BindChargePatientDetails(appointmentId);

            foreach (DataRow item in dtChargePatientDetails.Rows)
            {
                using (clsClaimDetails claimDetail = new clsClaimDetails())
                {
                    try
                    {
                        claimDetail.date = DateTime.Parse(Convert.ToString(item["Date"]));
                        claimDetail.chargeId = Convert.ToInt32(item["CHARGEID"]);
                        claimDetail.cptId = Convert.ToInt32(item["CPTID"]);
                        claimDetail.cptCode = Convert.ToString(item["CPTCODE"]);
                        claimDetail.cptDesc = Convert.ToString(item["CPTDESC"]);
                        claimDetail.icd1 = Convert.ToInt32(item["ICD_1"]);
                        claimDetail.icdCode1 = Convert.ToString(item["ICD_CODE_1"]);
                        claimDetail.icdDesc1 = Convert.ToString(item["ICD_DESC_1"]);
                        claimDetail.icd2 = Convert.ToInt32(item["ICD_2"]);
                        claimDetail.icdCode2 = Convert.ToString(item["ICD_CODE_2"]);
                        claimDetail.icdDesc2 = Convert.ToString(item["ICD_DESC_2"]);
                        claimDetail.icd3 = Convert.ToInt32(item["ICD_3"]);
                        claimDetail.icdCode3 = Convert.ToString(item["ICD_CODE_3"]);
                        claimDetail.icdDesc3 = Convert.ToString(item["ICD_DESC_3"]);
                        claimDetail.icd4 = Convert.ToInt32(item["ICD_4"]);
                        claimDetail.icdCode4 = Convert.ToString(item["ICD_CODE_4"]);
                        claimDetail.icdDesc4 = Convert.ToString(item["ICD_DESC_4"]);
                        claimDetail.chargeAmount = Convert.ToDecimal(item["CHARGEAMOUNT"]);
                        claimDetail.allowedAmount = Convert.ToDecimal(item["ALLOWEDAMOUNT"]);
                        claimDetail.deduction = Convert.ToDecimal(item["DEDUCTION"]);
                        claimDetail.insAdjustment = Convert.ToDecimal(item["INS_ADJUSTMENT"]);
                        claimDetail.miscAdjustment = Convert.ToDecimal(item["MISC_ADJUSTMENT"]);
                        claimDetail.copay = Convert.ToDecimal(item["COPAY"]);
                        claimDetail.balance = Convert.ToDecimal(item["BALANCE"]);
                        claimDetail.capitated = Convert.ToString(item["CAPITATED"]);
                        claimDetail.modifiedCode = Convert.ToString(item["MODIFIEDCC"]);
                        claimDetail.reasonId = Convert.ToInt32(item["REASONID"]);
                        claimDetail.paymentReceived = Convert.ToInt32(item["PaymentReceived"]);
                        claimDetail.insuranceBalance = Convert.ToInt32(item["InsuranceBalance"]);
                        claimDetails.Add(claimDetail);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }

                }

            }
            return claimDetails;
        }
        #endregion

        #region Get Charge Patient Adjustment
        /// <summary>
        /// ClaimService : Get Charge Patient Adjustment
        /// </summary>
        /// <param name="chargeId"></param>
        /// <returns></returns>
        public List<clsClaimAdjustment> GetChargePatientAdjustment(int chargeId)
        {
            List<clsClaimAdjustment> claimAdjustments = new List<clsClaimAdjustment>();
            DataTable dtChargePatientAdjustment = BindChargePatientAdjustment(chargeId);

            foreach (DataRow item in dtChargePatientAdjustment.Rows)
            {
                using (clsClaimAdjustment claimAdjustment = new clsClaimAdjustment())
                {
                    claimAdjustment.autoId = Convert.ToInt32(item["AutoId"]); 
                    claimAdjustment.chargeId = Convert.ToInt32(item["CHARGEID"]);
                    claimAdjustment.insuranceId = Convert.ToInt32(item["INSURANCEID"]);
                    claimAdjustment.insuranceName = Convert.ToString(item["INSURANCENAME"]);
                    claimAdjustment.groupName = Convert.ToString(item["GROUPNAME"]);
                    claimAdjustment.amount = Convert.ToDecimal(item["AMOUNT"]);
                    claimAdjustment.reasonId = Convert.ToString(item["REASONID"]);

                    claimAdjustments.Add(claimAdjustment);
                }
            }
            return claimAdjustments;
        }
        #endregion

        #region Save Claim
        /// <summary>
        /// ClaimService : Save Claim
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="xmlHeader"></param>
        /// <param name="xmlDetails"></param>
        /// <param name="xmlAdjustment"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public string SaveClaim(int chargeId, string xmlHeader, string xmlDetails, string xmlAdjustment, string isDelete)
        {
            string result = "0";
            string sqlClaims = " EXEC USP_SaveClaim " + chargeId + " ,'" + xmlHeader + "','" + xmlDetails + "','" + xmlAdjustment + "','" + isDelete + "'";
            try
            {
                result = (string)db.GetSingleValue(sqlClaims);
            }
            catch (SqlException sqlEx)
            {
                result = sqlEx.Message;
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                result = nullEx.Message;
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return result;

        }
        #endregion
        #endregion

        #region Private Method
        #region Bind Completed Appointment List
        private DataTable BindAppointmentList(string fromDate, string toDate, string providerId, string statusId,string patientName,string feeTicket)
        {
            DataTable dtAppointment = new DataTable();
            string sqlAppointment = "EXEC USP_GetListAppointmentDetails '" + fromDate + "','" + toDate + "'," + providerId + ",'" + statusId + "','" + patientName + "','" + feeTicket + "'";
            try
            {
                dtAppointment = db.GetData(sqlAppointment);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return dtAppointment;
        }
        #endregion

        #region Bind Charge Patient Header 
        private DataTable BindChargePatientHeader(int appointmentId)
        {
            DataTable dtChargePatientHeader = new DataTable();
            string sqlChargePatientHeader = "EXEC USP_GetChargePatientHeader " + appointmentId;
            try
            {
                dtChargePatientHeader = db.GetData(sqlChargePatientHeader);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return dtChargePatientHeader;
        }
        #endregion

        #region Bind Charge Patient Details 
        private DataTable BindChargePatientDetails(int appointmentId)
        {
            DataTable dthargePatientDetails = new DataTable();
            string sqlhargePatientDetails = "EXEC USP_GetChargePatientDetails " + appointmentId;
            try
            {
                dthargePatientDetails = db.GetData(sqlhargePatientDetails);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return dthargePatientDetails;
        }
        #endregion

        #region Bind Charge Patient Adjustment
        private DataTable BindChargePatientAdjustment(int chargeId)
        {
            DataTable dtChargePatientAdjustment = new DataTable();
            string sqlChargePatientAdjustment = "EXEC USP_GetChargepatientAdjustment " + chargeId;
            try
            {
                dtChargePatientAdjustment = db.GetData(sqlChargePatientAdjustment);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return dtChargePatientAdjustment;
        }
        #endregion
        #endregion
    }
}
