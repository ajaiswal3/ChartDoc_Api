using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// PatientInformationService : Public Class
    /// </summary>
    public class PatientInformationService : IPatientInformationService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        ISharedService _sharedService;
        #endregion

        #region PatientInformationService Constructor**********************************************************************************************************
        /// <summary>
        /// PatientInformationService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public PatientInformationService(IConfiguration configaration,ISharedService sharedService)
        {
            db._configaration = configaration;
            this._sharedService = sharedService;
        }
        #endregion

        #region CreatePatient**********************************************************************************************************************************

        public string CreatePatient(string patientId, string patientDetails, string patientBilling, string emergencyContact, string employerContact, string insurance, string social, string authorization)
        {
            string sqlPatient = " EXEC [USP_CREATEUPDATE_PATIENT] '" + patientId + "','" + patientDetails + "','" + patientBilling + "','" + emergencyContact + "','" + employerContact + "','" + insurance + "','" + social + "','" + authorization + "'";
            string result = (string)db.GetSingleValue(sqlPatient);
            // Update RecopiaID while creating a new patient.
            //if (patientId == "0")
            //{
                SavePatientDRFirst(result);
            //}
            return result;
           
        }
        #endregion

        #region Validate Duplicate Patient
        public string ValidatePatient(string patientId, string patientFName, string patientMName, string patientLName, string patientAddr1, string patientAddr2, string dob, string ssn, string contact)
        {
            string sql = " EXEC [USP_PATIENT_DUPLICATE] '" + patientId + "','" + patientFName + "','" + patientMName + "','" + patientLName + "','" + patientAddr1 + "','" + patientAddr2 + "','" + dob + "','" + ssn + "','" + contact + "'";
            string res = (string)db.GetSingleValue(sql);

            return res;
        }
        #endregion

        #region CreatePatientDRFirst
        /// <summary>
        /// SavePatientDRFirst
        /// </summary>
        /// <param name="appointmentID">string</param>
        /// <returns>string</patientId>
        private string SavePatientDRFirst(string patientId)
        {
            string result = "1";

            string sql = "Select * from M_DRFIRSTCONFIG where Command='send_patient'";
            DataTable dtconfig = db.GetData(sql);


            sql = "SELECT * FROM T_PatientDetails WHERE PatientId='" + patientId + "'";

            DataTable dtPatientDetails = db.GetData(sql);

            if (dtPatientDetails.Rows.Count != 0 && Convert.ToString(dtPatientDetails.Rows[0]["RecopiaID2"])=="")
            {
                string xmldata = @"xml=<?xml version = ""1.0"" encoding=""UTF-8""?><RCExtRequest version = ""2.36"">" +
                                " <Caller><VendorName>" + Convert.ToString(dtconfig.Rows[0]["VendorName"]) + "</VendorName><VendorPassword>" + Convert.ToString(dtconfig.Rows[0]["VendorPassword"]) + "</VendorPassword> " +
                                " </Caller><SystemName>" + Convert.ToString(dtconfig.Rows[0]["VendorName"]) + "</SystemName><RcopiaPracticeUsername>" + Convert.ToString(dtconfig.Rows[0]["RcopiaPracticeUsername"]) + "</RcopiaPracticeUsername> " +
                                " <Request> <Command>" + Convert.ToString(dtconfig.Rows[0]["Command"]) + "</Command> " +
                                "  <PatientList> <Patient> " +
                                " <ExternalID> " + patientId + " </ExternalID>" +
                                " <FirstName> " + Convert.ToString(dtPatientDetails.Rows[0]["First_Name"]) + " </FirstName> " +
                                " <LastName> " + Convert.ToString(dtPatientDetails.Rows[0]["Last_Name"]) + " </LastName> " +
                                 " <DOB> " + Convert.ToDateTime(dtPatientDetails.Rows[0]["DOB"]).ToString("MM/dd/yyyy 00:00:00", CultureInfo.InvariantCulture) + " </DOB> " +
                                " <Sex> " + Convert.ToString(dtPatientDetails.Rows[0]["Gender"]) + " </Sex> " +
                                " <HomePhone> " + Convert.ToString(dtPatientDetails.Rows[0]["Mob_No"]) + "</HomePhone> " +
                                " <Address1>  " + Convert.ToString(dtPatientDetails.Rows[0]["Add_Line"]) + "</Address1> " +
                                " <Address2>" + Convert.ToString(dtPatientDetails.Rows[0]["Add_Line1"]) + "</Address2> " +
                                " <City> " + Convert.ToString(dtPatientDetails.Rows[0]["Add_City"]) + " </City> " +
                                " <State> " + Convert.ToString(dtPatientDetails.Rows[0]["Add_State"]) + " </State> " +
                                " <Zip> " + Convert.ToString(dtPatientDetails.Rows[0]["Add_PostalCode"]) + " </Zip> " +
                                " </Patient> </PatientList> " +
                                " </Request></RCExtRequest>";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Convert.ToString(dtconfig.Rows[0]["URL"]));
                request.Method = "POST";
                request.ContentType = "application/text";


                request.ContentLength = xmldata.Length;
                using (Stream webStream = request.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                {
                    requestWriter.Write(xmldata);
                }

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();

                        DataSet dsresult = new DataSet();
                        dsresult.ReadXml(new StringReader(response));
                        DataTable dt = new DataTable();

                        dt = dsresult.Tables["Patient"];
                        if (dt != null && dt.Rows.Count > 1 && Convert.ToString(dt.Rows[1]["Status"]) == "created")
                        {
                            sql = "Update  T_PatientDetails set  RecopiaID2='" + Convert.ToString(dt.Rows[1]["RcopiaID"]) + "', RecopiaID='" + patientId + "', RecopiaName='"+ Convert.ToString(dtconfig.Rows[0]["VendorName"]) + "' where PatientId ='" + patientId + "'";
                            int i = db.HandleData(sql);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("-----------------");
                    Console.Out.WriteLine(ex.Message);
                }

            }





            return result;
        }

        #endregion

        #region GetPatientInfo*********************************************************************************************************************************
        /// <summary>
        /// GetPatientInfo
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>clsCreateUpdatePatient</returns>
        public clsCreateUpdatePatient GetPatientInfo(string patientId)
        {
            clsCreateUpdatePatient patientdtl = new clsCreateUpdatePatient();
            clsPatientDetails oPatientDetails = new clsPatientDetails();
            clsPatientBilling oPatientBilling = new clsPatientBilling();
            clsPatientEmergencyContact oPatientEmergencyContact = new clsPatientEmergencyContact();
            clsPatientEmployerContact oPatientEmployerContact = new clsPatientEmployerContact();
            List<clsPatientInsurance> oPatientInsuranceList = new List<clsPatientInsurance>();
            clsPatientSocial oPatientSocial = new clsPatientSocial();
            clsPatientAuthorization oPatientAuthorization = new clsPatientAuthorization();

            string sqlPatientInfo = "EXEC USP_PATIENTINFO '" + patientId + "'";
            DataSet dsPatientInformation = db.GetDataInDataSet(sqlPatientInfo);
            {
                //T_PatientDetails
                if (dsPatientInformation.Tables[0].Rows.Count > 0)
                {
                    oPatientDetails.patientId = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["PatientId"]);
                    oPatientDetails.firstName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["First_Name"]));
                    oPatientDetails.middleName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Middle_Name"]));
                    oPatientDetails.lastName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Last_Name"]));
                    oPatientDetails.addressLine = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_Line"]));
                    oPatientDetails.addressLine1 = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_Line1"]));
                    oPatientDetails.addressCity = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_City"]));
                    oPatientDetails.addressState = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_State"]));
                    oPatientDetails.addressPostalCode = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_PostalCode"]));
                    oPatientDetails.addressCountry = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Add_Country"]));
                    oPatientDetails.dob = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["DOB"]);
                    oPatientDetails.age = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Age"]);
                    oPatientDetails.gender = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Gender"]));
                    oPatientDetails.email = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Email"]));
                    oPatientDetails.mobNo = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Mob_No"]));
                    oPatientDetails.imageName = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Image_Name"]);
                    oPatientDetails.imagePath = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Image_Path"]);
                    oPatientDetails.flag = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Data_Flag"]);
                    oPatientDetails.recopiaId = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["RecopiaID"]);
                    oPatientDetails.recopiaName = Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["RecopiaName"]);
                    oPatientDetails.primaryPhone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Primary_Phone"]));
                    oPatientDetails.secondaryPhone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[0].Rows[0]["Secondary_Phone"]));
                    patientdtl.sPatientDetails = oPatientDetails;
                }
                //T_Patient_Billing
                if (dsPatientInformation.Tables[1].Rows.Count > 0)
                {
                    oPatientBilling.patientId = Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["PatientId"]);
                    oPatientBilling.billingParty = Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Billing_Party"]);
                    oPatientBilling.firstName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["First_Name"]));
                    oPatientBilling.middleName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Middle_Name"]));
                    oPatientBilling.lastName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Last_Name"]));
                    oPatientBilling.dob = Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["DOB"]);
                    oPatientBilling.addLine =_sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Add_Line"]));
                    oPatientBilling.addLine1 = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Add_Line1"]));
                    oPatientBilling.addCity = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Add_City"]));
                    oPatientBilling.addState = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Add_State"]));
                    oPatientBilling.addZip = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Add_Zip"]));
                    oPatientBilling.SSN = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["SSN"]));
                    oPatientBilling.driversLicenseFilePath = Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Drivers_License_FilePath"]);
                    oPatientBilling.primaryPhone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Primary_Phone"]));
                    oPatientBilling.secondaryPhone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["Secondary_Phone"]));
                    oPatientBilling.billingPartyOther = Convert.ToString(dsPatientInformation.Tables[1].Rows[0]["BillingPartyOther"]);
                    patientdtl.sPatientBilling = oPatientBilling;
                }
                //T_Patient_EmergencyContact
                if (dsPatientInformation.Tables[2].Rows.Count > 0)
                {
                    oPatientEmergencyContact.patientId = Convert.ToString(dsPatientInformation.Tables[2].Rows[0]["PatientId"]);
                    oPatientEmergencyContact.contactName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[2].Rows[0]["ContactName"]));
                    oPatientEmergencyContact.contactPhone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[2].Rows[0]["ContactPhone"]));
                    oPatientEmergencyContact.relationship = Convert.ToString(dsPatientInformation.Tables[2].Rows[0]["Relationship"]);
                    patientdtl.sPatientEmergency = oPatientEmergencyContact;
                }
                //T_Patient_EmployerContact
                if (dsPatientInformation.Tables[3].Rows.Count > 0)
                {
                    oPatientEmployerContact.patientId = Convert.ToString(dsPatientInformation.Tables[3].Rows[0]["PatientId"]);
                    oPatientEmployerContact.name = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[3].Rows[0]["Name"]));
                    oPatientEmployerContact.phone = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[3].Rows[0]["Phone"]));
                    oPatientEmployerContact.address = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[3].Rows[0]["Address"]));
                    patientdtl.sPatientEmpContact = oPatientEmployerContact;
                }
                //T_Patient_Insurance
                if (dsPatientInformation.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow item in dsPatientInformation.Tables[4].Rows)
                    {
                        clsPatientInsurance oPatientInsurance = new clsPatientInsurance();
                        oPatientInsurance.patientId = Convert.ToString(item["PatientId"]);
                        oPatientInsurance.providerName = Convert.ToString(item["Provider_Name"]);
                        oPatientInsurance.providerId = Convert.ToString(item["Provider_ID"]);
                        oPatientInsurance.insurancePolicy = _sharedService.Decrypt(Convert.ToString(item["Insurance_Policy"]));
                        oPatientInsurance.policyType = Convert.ToString(item["Policy_Type"]);
                        oPatientInsurance.policyTypeId = Convert.ToString(item["Policy_Type_ID"]);
                        oPatientInsurance.cardImageFilePath = Convert.ToString(item["Card_Image_FilePath"]);
                        oPatientInsurance.effectiveFrom = Convert.ToString(item["Effective_From"]);
                        oPatientInsurance.status = Convert.ToString(item["Status"]);
                        oPatientInsurance.statusId = Convert.ToString(item["Status_id"]);
                        oPatientInsuranceList.Add(oPatientInsurance);
                    }
                    patientdtl.sPatientInsurance = oPatientInsuranceList;
                }
                //T_Patient_Social
                if (dsPatientInformation.Tables[5].Rows.Count > 0)
                {
                    oPatientSocial.patientId = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["PatientId"]);
                    oPatientSocial.maritalStatus = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Marital_Status"]));
                    oPatientSocial.guardianFName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Guardian_FName"]));
                    oPatientSocial.guardianLName = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Guardian_LName"]));
                    oPatientSocial.addLine = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Add_Line"]));
                    oPatientSocial.addCity = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Add_City"]));
                    oPatientSocial.addState = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Add_State"]));
                    oPatientSocial.addZip = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Add_Zip"]));
                    oPatientSocial.DOB = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["DOB"]);
                    oPatientSocial.patientSSN = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Patient_SSN"]));
                    oPatientSocial.phoneNumber = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Phone_Number"]));
                    oPatientSocial.guardianSSN = _sharedService.Decrypt(Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Guardian_SSN"]));
                    oPatientSocial.driversLicenseFilePath = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Drivers_License_FilePath"]);
                    oPatientSocial.race = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Race"]);
                    oPatientSocial.ethicity = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Ethicity"]);
                    oPatientSocial.language = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Language"]);
                    oPatientSocial.commMode = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["Comm_Mode"]);
                    oPatientSocial.socialMaritalStatusOther = Convert.ToString(dsPatientInformation.Tables[5].Rows[0]["SocialMaritalStatusOther"]);
                    patientdtl.sPatientSocial = oPatientSocial;
                }
                //T_Patient_Authorization
                if (dsPatientInformation.Tables[6].Rows.Count > 0)
                {
                    oPatientAuthorization.patientId = Convert.ToString(dsPatientInformation.Tables[6].Rows[0]["PatientId"]);
                    oPatientAuthorization.authorizationFilePath = Convert.ToString(dsPatientInformation.Tables[6].Rows[0]["Authorization_FilePath"]);
                    patientdtl.sPatientAuthorisation = oPatientAuthorization;
                }
            }
            return patientdtl;
        }
        #endregion
    }
}
