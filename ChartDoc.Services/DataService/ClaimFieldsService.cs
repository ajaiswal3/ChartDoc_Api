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
    /// <summary>
    /// public class ClaimFieldsService : IClaimFieldsService
    /// </summary>
    public class ClaimFieldsService : IClaimFieldsService
    {
        #region Instance Variable
        DBUtils context = DBUtils.GetInstance;
        private readonly ILogService logService;
        #endregion

        #region ClaimFieldsService Constructor************************************************************************************************************************
        /// <summary>
        /// RoleService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService"></param>
        public ClaimFieldsService(IConfiguration configaration, ILogService logService)
        {
            context._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region GetClaimFieldsHeader****************************************************************************************************************************************
        /// <summary>
        /// GetClaimFieldsDetails
        /// </summary>
        /// <returns>List<clsClaimFieldsHeader></returns>
        public List<clsClaimFieldsHeader> GetClaimFieldsHeader()
        {
            List<clsClaimFieldsHeader> lstDetails = new List<clsClaimFieldsHeader>();
            DataTable dtClaimFieldsHeader = BindClaimFieldsHeader();
            for (int index = 0; index <= dtClaimFieldsHeader.Rows.Count - 1; index++)
            {
                clsClaimFieldsHeader objclsClaimFieldsHeader = new clsClaimFieldsHeader();
                objclsClaimFieldsHeader.id= Convert.ToInt32(dtClaimFieldsHeader.Rows[index]["ID"]);
                objclsClaimFieldsHeader.name = Convert.ToString(dtClaimFieldsHeader.Rows[index]["name"]);
                objclsClaimFieldsHeader.status = Convert.ToString(dtClaimFieldsHeader.Rows[index]["status"]);
                objclsClaimFieldsHeader.type = Convert.ToString(dtClaimFieldsHeader.Rows[index]["type"]);
                objclsClaimFieldsHeader.value = Convert.ToString(dtClaimFieldsHeader.Rows[index]["value"]);
                lstDetails.Add(objclsClaimFieldsHeader);
            }
            return lstDetails;
        }
        #endregion

        #region BindClaimFieldsHeader***************************************************************************************************************************************
        /// <summary>
        /// BindClaimFieldsHeader
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindClaimFieldsHeader()
        {
            DataTable dtClaimFieldsHeader = new DataTable();
            string sqlClaimFieldsHeader = "exec [dbo].[USP_GetFieldList]";
            dtClaimFieldsHeader = context.GetData(sqlClaimFieldsHeader);
            return dtClaimFieldsHeader;
        }
        #endregion

        #region GetClaimFieldsDetails****************************************************************************************************************************************
        /// <summary>
        /// GetClaimFieldsDetails
        /// </summary>
        /// <returns>List<clsClaimFieldsHeader></returns>
        public List<clsClaimFieldsDetails> GetClaimFieldsDetails(string id)
        {
            List<clsClaimFieldsDetails> lstDetails = new List<clsClaimFieldsDetails>();
            DataTable dtClaimFieldsDetails = BindClaimFieldsDetails(id);
            for (int index = 0; index <= dtClaimFieldsDetails.Rows.Count - 1; index++)
            {
                clsClaimFieldsDetails objclsClaimFieldsDetails = new clsClaimFieldsDetails();
                objclsClaimFieldsDetails.id = Convert.ToInt32(dtClaimFieldsDetails.Rows[index]["id"]);
                objclsClaimFieldsDetails.value = Convert.ToString(dtClaimFieldsDetails.Rows[index]["value"]);
                objclsClaimFieldsDetails.status = Convert.ToString(dtClaimFieldsDetails.Rows[index]["status"]);
                objclsClaimFieldsDetails.name = Convert.ToString(dtClaimFieldsDetails.Rows[index]["name"]);
                objclsClaimFieldsDetails.type = Convert.ToString(dtClaimFieldsDetails.Rows[index]["type"]);
                lstDetails.Add(objclsClaimFieldsDetails);
            }
            return lstDetails;
        }
        #endregion

        #region GetInsuranceClaimFields****************************************************************************************************************************************
        /// <summary>
        /// GetInsuranceClaimFields
        /// </summary>
        /// <returns>List<clsPatientChargeClaimFields></returns>
        public List<clsPatientChargeClaimFields> GetInsuranceClaimFields(string chargeId)
        {
            List<clsPatientChargeClaimFields> lstDetails = new List<clsPatientChargeClaimFields>();
            DataTable dtClaimFieldsDetails = BindInsuranceClaimFields(chargeId);
            for (int index = 0; index <= dtClaimFieldsDetails.Rows.Count - 1; index++)
            {
                clsPatientChargeClaimFields objclsPatientChargeClaimFields = new clsPatientChargeClaimFields();
                objclsPatientChargeClaimFields.fieldid = Convert.ToInt32(dtClaimFieldsDetails.Rows[index]["fieldid"]);
                objclsPatientChargeClaimFields.value = Convert.ToString(dtClaimFieldsDetails.Rows[index]["value"]);
                lstDetails.Add(objclsPatientChargeClaimFields);
            }
            return lstDetails;
        }
        #endregion
        #region BindInsuranceClaimFields***************************************************************************************************************************************
        /// <summary>
        /// BindInsuranceClaimFields
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindInsuranceClaimFields(string chargeId)
        {
            DataTable dtInsuranceClaimFields = new DataTable();
            string sqInsuranceClaimFields = "exec [dbo].[USP_GetInsuranceClaimFields] " + chargeId + "";
            dtInsuranceClaimFields = context.GetData(sqInsuranceClaimFields);
            return dtInsuranceClaimFields;
        }
        #endregion

        #region GetClaimFieldsDetails****************************************************************************************************************************************
        /// <summary>
        /// GetClaimFieldsDetails
        /// </summary>
        /// <returns>List<clsClaimFieldsHeader></returns>
        public List<clsClaimFieldsDetails> GetClaimFieldsMasterDetails(int id)
        {
            List<clsClaimFieldsDetails> lstDetails = new List<clsClaimFieldsDetails>();
            DataTable dtClaimFieldsDetails = BindClaimFieldsMasterDetails(id);
            for (int index = 0; index <= dtClaimFieldsDetails.Rows.Count - 1; index++)
            {
                clsClaimFieldsDetails objclsClaimFieldsDetails = new clsClaimFieldsDetails();
                objclsClaimFieldsDetails.id = Convert.ToInt32(dtClaimFieldsDetails.Rows[index]["id"]);
                objclsClaimFieldsDetails.slNo = Convert.ToInt32(dtClaimFieldsDetails.Rows[index]["SLNO"]);
                objclsClaimFieldsDetails.value = Convert.ToString(dtClaimFieldsDetails.Rows[index]["value"]);
                objclsClaimFieldsDetails.status = Convert.ToString(dtClaimFieldsDetails.Rows[index]["status"]);
                objclsClaimFieldsDetails.name = Convert.ToString(dtClaimFieldsDetails.Rows[index]["name"]);
                objclsClaimFieldsDetails.type = Convert.ToString(dtClaimFieldsDetails.Rows[index]["type"]);

                lstDetails.Add(objclsClaimFieldsDetails);
            }
            return lstDetails;
        }
        #endregion


        #region BindClaimFieldsDetails***************************************************************************************************************************************
        /// <summary>
        /// BindClaimFieldsDetails
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindClaimFieldsDetails(string id)
        {
            DataTable dtClaimFieldsDetails = new DataTable();
            string sqClaimFieldsDetails = "exec [dbo].[USP_GetFieldDetails] "+id+"";
            dtClaimFieldsDetails = context.GetData(sqClaimFieldsDetails);
            return dtClaimFieldsDetails;
        }
        #endregion

        #region BindClaimFieldsDetails***************************************************************************************************************************************
        /// <summary>
        /// BindClaimFieldsDetails
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindClaimFieldsMasterDetails(int id)
        {
            DataTable dtClaimFieldsDetails = new DataTable();
            string sqClaimFieldsDetails = "exec [dbo].[USP_GETCLAIMFIELDS] " + id + "";
            dtClaimFieldsDetails = context.GetData(sqClaimFieldsDetails);
            return dtClaimFieldsDetails;
        }
        #endregion


        #region Save PatientCharge Claim Fields
        /// <summary>
        /// PatientChargeClaimFields : Save Claim
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="xlmPatientChargeClaimFields"></param>

        /// <param name="typeEM"></param>
        /// <returns></returns>
        public string SavePatientChargeClaimFields(int chargeId, string xlmPatientChargeClaimFields, string typeEM)
        {
            string result = "0";
            string sqlClaims = " EXEC USP_saveInsuranceClaimfields " + chargeId + " ,'" + xlmPatientChargeClaimFields + "','" + typeEM + "'";
            try
            {
                result = (string)context.GetSingleValue(sqlClaims);
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

        #region Save Claim Field Master
        /// <summary>
        /// ClaimService : Save Claim
        /// </summary>
        /// <param name="claimFieldId"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="xmlDetails"></param>
        /// <returns></returns>
        public string SaveClaimFieldMaster(int claimFieldId, string name, char type, string isDeleted, string xmlDetails)
        {
            string result = "0";
            string sqlClaims = " EXEC USP_SAVECLAIMFIELDS " + claimFieldId + " ,'" + name + "','" + type + "','" + xmlDetails + "','" + isDeleted + "'" ;
            try
            {
                result = (string)context.GetSingleValue(sqlClaims);
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
    }
}
