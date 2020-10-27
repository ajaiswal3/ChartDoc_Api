using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// PatientDetailsService : Public Class
    /// </summary>
    public class PatientDetailsService : IPatientDetailsService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        DBUtils dB = DBUtils.GetInstance;
        ISharedService _sharedService;
        #endregion

        #region PatientDetailsService Constructor**************************************************************************************************************
        /// <summary>
        /// PatientDetailsService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public PatientDetailsService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            dB._configaration = configaration;
            this.logService = logService;
            this._sharedService = sharedService;
        }
        #endregion

        #region SearchPatient**********************************************************************************************************************************
        /// <summary>
        /// SearchPatient
        /// </summary>
        /// <param name="firstName">string</param>
        /// <param name="lastName">string</param>
        /// <param name="dob">string</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        /// <returns>List<clsPatientDetails></returns>
        public List<clsPatientDetails> SearchPatient(string firstName, string lastName, string dob, string mobile, string email, string gender,string isactivated)
        {
            List<clsPatientDetails> lstPatientDetails = new List<clsPatientDetails>();
            try
            {
                if(!string.IsNullOrEmpty(firstName))
                    firstName = _sharedService.Encrypt(firstName.Replace("{", "").Replace("}", "").Trim());
                if (!string.IsNullOrEmpty(lastName))
                    lastName = _sharedService.Encrypt(lastName.Replace("{", "").Replace("}", "").Trim());
                dob = dob.Replace("{", "").Replace("}", "").Trim();
                if (!string.IsNullOrEmpty(mobile))
                    mobile = _sharedService.Encrypt(mobile.Replace("{", "").Replace("}", "").Trim());
                if (!string.IsNullOrEmpty(email))
                    email = _sharedService.Encrypt(email.Replace("{", "").Replace("}", "").Trim());
                isactivated = isactivated.Replace("{", "").Replace("}", "").Trim();
                DataTable dtPatientDetails = GetPatient(firstName, lastName, dob, mobile, email, gender, isactivated);

                foreach (DataRow rowPatientDetails in dtPatientDetails.Rows)
                {
                    clsPatientDetails patient = new clsPatientDetails();
                    patient.patientId = Convert.ToString(rowPatientDetails["PatientId"]);
                    patient.firstName = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["First_Name"]));
                    patient.middleName = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Middle_Name"]));
                    patient.lastName = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Last_Name"]));
                    patient.addressLine = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_Line"]));
                    patient.addressLine1 = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_Line1"]));
                    patient.addressCity = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_City"])).Trim();
                    patient.addressState = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_State"]));
                    patient.addressPostalCode = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_PostalCode"]));
                    patient.addressCountry = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Add_Country"]));
                    patient.dob = DateTime.Parse(Convert.ToString(rowPatientDetails["DOB"])).ToString("MM-dd-yyyy");
                    patient.gender = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Gender"]));
                    patient.email = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Email"]));
                    patient.mobNo = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Mob_No"]));
                    patient.imageName = Convert.ToString(rowPatientDetails["Image_Name"]);
                    patient.imagePath = Convert.ToString(rowPatientDetails["Image_Path"]);
                    patient.flag = Convert.ToString(rowPatientDetails["Data_Flag"]);
                    patient.age = Convert.ToString(rowPatientDetails["Age"]);
                    patient.recopiaId = Convert.ToString(rowPatientDetails["RecopiaID"]);
                    patient.recopiaName = Convert.ToString(rowPatientDetails["RecopiaName"]);
                    patient.primaryPhone = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Primary_Phone"]));
                    patient.secondaryPhone = _sharedService.Decrypt(Convert.ToString(rowPatientDetails["Secondary_Phone"]));
                    patient.providerName = Convert.ToString(rowPatientDetails["Provider_Name"]);
                    patient.policyNo = Convert.ToString(rowPatientDetails["Insurance_Policy"]);
                    if (!String.IsNullOrEmpty(rowPatientDetails["Effective_From"].ToString()))
                    {
                        patient.effectiveFrom = DateTime.Parse(Convert.ToString(rowPatientDetails["Effective_From"])).ToString("MM-dd-yyyy");
                    }
                    lstPatientDetails.Add(patient);
                }
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return lstPatientDetails;
        }
        #endregion

        #region GetPatient*************************************************************************************************************************************
        /// <summary>
        /// GetPatient
        /// </summary>
        /// <param name="firstName">string</param>
        /// <param name="lastName">string</param>
        /// <param name="dob">string</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        /// <returns>DataTable</returns>
        private DataTable GetPatient(string firstName = null, string lastName = null, string dob = null, string mobile = null, string email = null, string gender = null,string isactivated=null)
        {

            DataTable dtPatient = new DataTable();
            string where = string.Empty;
            string sqlPatient = "SELECT pd.[PatientId] ,pd.[First_Name], pd.[Middle_Name] ,pd.[Last_Name], pd.[Add_Line], pd.[Add_Line1], pd.[Add_City],pd.[Add_State],pd.[Add_PostalCode]," +
                " pd.[Add_Country], pd.[DOB],pd.[Gender],pd.[Email],pd.[Mob_No],pd.[Image_Name],pd.[Image_Path], pd.[Data_Flag] ,pd.age,pd.RecopiaID,pd.RecopiaName,pd.Primary_Phone,pd.Secondary_Phone, " +
                "isnull((SELECT Provider_Name FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') AS Provider_Name, " +
                "isnull((SELECT Insurance_Policy FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') Insurance_Policy," +
                "isnull((SELECT Effective_From FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') Effective_From" +
                " FROM [dbo].[T_PatientDetails] pd ";

            if (!String.IsNullOrEmpty(firstName))
            {
                firstName = " AND [First_Name] like  '%" + firstName + "%' ";
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                lastName = " AND [Last_Name] like '%" + lastName + "%' ";
            }
            if (!String.IsNullOrEmpty(dob))
            {
                dob = " AND [DOB] = CONVERT( VARCHAR(20), CAST('" + dob + "' AS DATE),101) ";
            }
            if (!String.IsNullOrEmpty(mobile))
            {
                mobile = " AND [Mob_No] LIKE '%" + mobile + "%'";
            }
            if (!String.IsNullOrEmpty(email))
            {
                email = " AND [Email] LIKE '%" + email + "%'";
            }
            if (!String.IsNullOrEmpty(gender))
            {
                gender = " AND [Gender] ='" + gender + "'";
            }
            if (String.IsNullOrEmpty(isactivated))
            {
                where = "WHERE Acitivated in ('Y','N') " + firstName + lastName + mobile + email + dob + gender;
            }
            else
            {
                where = "WHERE Acitivated='Y' " + firstName + lastName + mobile + email + dob + gender;
            }
            sqlPatient = sqlPatient + where + " ORDER BY pd.Last_Name";
            try
            {
                dtPatient = dB.GetData(sqlPatient);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtPatient;
        }
        #endregion

        #region GetPatient*************************************************************************************************************************************
        /// <summary>
        /// GetPatient
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>DataTable</returns>
        private DataTable GetPatient(string id)
        {

            DataTable dtPatient = new DataTable();
            string where = string.Empty;
            string sqlPatient = "SELECT pd.[PatientId] ,pd.[First_Name], pd.[Middle_Name] ,pd.[Last_Name], pd.[Add_Line], pd.[Add_Line1], pd.[Add_City],pd.[Add_State],pd.[Add_PostalCode]," +
                " pd.[Add_Country], pd.[DOB],pd.[Gender],pd.[Email],pd.[Mob_No],pd.[Image_Name],pd.[Image_Path], pd.[Data_Flag] ,pd.age,pd.RecopiaID,pd.RecopiaName,pd.Primary_Phone,pd.Secondary_Phone, " +
                "isnull((SELECT Provider_Name FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') AS Provider_Name, " +
                "isnull((SELECT Insurance_Policy FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') Insurance_Policy," +
                "isnull((SELECT Effective_From FROM T_Patient_Insurance WHERE PatientId = pd.[PatientId] AND Policy_Type=1 AND status=1),'') Effective_From" +
                " FROM [dbo].[T_PatientDetails] pd ";

            where = "WHERE Acitivated='Y' AND PatientId='" + id + "'";
            sqlPatient = sqlPatient + where + " ORDER BY pd.Last_Name";
            try
            {
                dtPatient = dB.GetData(sqlPatient);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtPatient;
        }
        #endregion

        #region SearchPatientbyID******************************************************************************************************************************
        /// <summary>
        /// SearchPatientbyID
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>clsPatientDetails</returns>
        public clsPatientDetails SearchPatientbyID(string id)
        {
            clsPatientDetails patient = new clsPatientDetails();
            try
            {
                DataTable dtPatient = GetPatient(id);
                foreach (DataRow rowPatient in dtPatient.Rows)
                {
                    patient.patientId = Convert.ToString(rowPatient["PatientId"]);
                    patient.firstName = _sharedService.Decrypt(Convert.ToString(rowPatient["First_Name"]));
                    patient.middleName = _sharedService.Decrypt(Convert.ToString(rowPatient["Middle_Name"]));
                    patient.lastName = _sharedService.Decrypt(Convert.ToString(rowPatient["Last_Name"]));
                    patient.addressLine = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_Line"]));
                    patient.addressLine1 = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_Line1"]));
                    patient.addressCity = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_City"])).Trim();
                    patient.addressState = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_State"]));
                    patient.addressPostalCode = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_PostalCode"]));
                    patient.addressCountry = _sharedService.Decrypt(Convert.ToString(rowPatient["Add_Country"]));
                    patient.dob = DateTime.Parse(Convert.ToString(rowPatient["DOB"])).ToString("MM-dd-yyyy");
                    patient.gender = _sharedService.Decrypt(Convert.ToString(rowPatient["Gender"]));
                    patient.email = _sharedService.Decrypt(Convert.ToString(rowPatient["Email"]));
                    patient.mobNo = _sharedService.Decrypt(Convert.ToString(rowPatient["Mob_No"]));
                    patient.imageName = Convert.ToString(rowPatient["Image_Name"]);
                    patient.imagePath = Convert.ToString(rowPatient["Image_Path"]);
                    patient.flag = Convert.ToString(rowPatient["Data_Flag"]);
                    patient.age = Convert.ToString(rowPatient["Age"]);
                    patient.recopiaId = Convert.ToString(rowPatient["RecopiaID"]);
                    patient.recopiaName = Convert.ToString(rowPatient["RecopiaName"]);
                    patient.primaryPhone = _sharedService.Decrypt(Convert.ToString(rowPatient["Primary_Phone"]));
                    patient.secondaryPhone = _sharedService.Decrypt(Convert.ToString(rowPatient["Secondary_Phone"]));
                    patient.providerName = Convert.ToString(rowPatient["Provider_Name"]);
                    patient.policyNo = Convert.ToString(rowPatient["Insurance_Policy"]);
                    if (!String.IsNullOrEmpty(rowPatient["Effective_From"].ToString()))
                    {
                        patient.effectiveFrom = DateTime.Parse(Convert.ToString(rowPatient["Effective_From"])).ToString("MM-dd-yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return patient;
        }
        #endregion

    }
}
