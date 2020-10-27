using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// ObservationService : Public Class 
    /// </summary>
    public class ObservationService : IObservationService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        ISharedService _sharedService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region ObservationService Constructor*****************************************************************************************************************
        /// <summary>
        /// ObservationService : Constructor
        /// </summary>
        /// <param name="context">ChartDocDBContext</param>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public ObservationService(ChartDocDBContext context, IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            db._configaration = configaration;
            this.logService = logService;
            this._sharedService = sharedService;
        }
        #endregion

        #region GetObservation*********************************************************************************************************************************
        /// <summary>
        /// GetObservation
        /// </summary>
        /// <param name="PatientId">string</param>
        /// <returns>List<clsObservation></returns>
        public List<clsObservation> GetObservation(string patientId)
        {
            List<clsObservation> lstDetails = new List<clsObservation>();
            DataTable dtObservation = BindObservation(patientId);

            for (int index = 0; index <= dtObservation.Rows.Count - 1; index++)
            {
                clsObservation objObservation = new clsObservation();
                objObservation.pId = Convert.ToString(dtObservation.Rows[index]["Id"]);
                objObservation.pBloodPressureL =_sharedService.Decrypt( Convert.ToString(dtObservation.Rows[index]["BloodPressure_L"]));
                objObservation.pBloodPressureR = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["BloodPressure_R"]));
                objObservation.pTemperature = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Temperature"]));
                objObservation.pHeightL = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Height_L"]));
                objObservation.pHeightR = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Height_R"]));
                objObservation.pWeight = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Weight"]));
                objObservation.pPulse = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Pulse"]));
                objObservation.pRespiratory = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Respiratory"]));
                lstDetails.Add(objObservation);
            }
            return lstDetails;
        }
        #endregion

        #region BindObservation********************************************************************************************************************************
        /// <summary>
        /// BindObservation
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindObservation(string patientId)
        {
            DataTable dtObservation = new DataTable();
            string sqlObservation = "SELECT ID,  [BloodPressure_L],[BloodPressure_R],[Temperature],[Height_L],[Height_R],[Weight],[Pulse],[Respiratory] FROM [t_Observation] " +
                " WHERE [APPOINTMENTID]='" + patientId + "'";
            try
            {
                dtObservation = db.GetData(sqlObservation);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtObservation;
        }
        #endregion

        #region SaveObservation********************************************************************************************************************************
        /// <summary>
        /// SaveObservation
        /// </summary>
        /// <param name="objObservation">clsObservation</param>
        /// <returns>string</returns>
        public string SaveObservation(clsObservation objObservation)
        {
            string result = string.Empty;
            string sqlObservation = " EXEC [USP_UPDATE_OBSERVATION_V2] '" + objObservation.patientId + "' ,'" + objObservation.pBloodPressureL + "','"
                + objObservation.pBloodPressureR + "','" + objObservation.pTemperature + "','" + objObservation.pHeightL
                + "','" + objObservation.pHeightR + "','" + objObservation.pWeight + "','" + objObservation.pPulse + "','" + objObservation.pRespiratory + "'";

            result = (string)db.GetSingleValue(sqlObservation);
            SavePrescripTion(objObservation.patientId);
            return result;
        }
        #endregion

        #region SavePrescripTion*******************************************************************************************************************************
        /// <summary>
        /// SavePrescripTion
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>string</returns>
        private string SavePrescripTion(string appointmentId)
        {
            string result = "1";
            string recopiaId2 = string.Empty;
            string patientId = string.Empty;
            string recopiaId = string.Empty;

            string sqlPrescripTion = "Select * from M_DRFIRSTCONFIG where Command='update_prescription'";
            DataTable dtconfig = db.GetData(sqlPrescripTion);

            sqlPrescripTion = "SELECT RecopiaID,ISNULL(RecopiaID2,'') as RecopiaID2,PatientID FROM T_PatientDetails WHERE PatientId=(SELECT [PatientId] FROM [T_Appointment] where AppointmentId='" + appointmentId + "')";
            //   RecopiaID2 = (string)db.GetSingleValue(sql);


            DataTable dtPrescripTion = db.GetData(sqlPrescripTion);

            if (dtPrescripTion.Rows.Count != 0)
            {
                recopiaId = Convert.ToString(dtPrescripTion.Rows[0]["RecopiaID"]);
                recopiaId2 = Convert.ToString(dtPrescripTion.Rows[0]["RecopiaID2"]);
                patientId = Convert.ToString(dtPrescripTion.Rows[0]["PatientID"]);


                string xmlData = @"xml=<?xml version = ""1.0"" encoding=""UTF-8""?><RCExtRequest version = ""2.36"">" +
                            " <Caller><VendorName> " + Convert.ToString(dtconfig.Rows[0]["VendorName"]) + " </VendorName><VendorPassword> " + Convert.ToString(dtconfig.Rows[0]["VendorPassword"]) + " </VendorPassword> " +
                            " </Caller><SystemName> " + Convert.ToString(dtconfig.Rows[0]["VendorName"]) + "  </SystemName><RcopiaPracticeUsername>" + Convert.ToString(dtconfig.Rows[0]["RcopiaPracticeUsername"]) + "</RcopiaPracticeUsername> " +
                            //" </Caller><SystemName>ravendor1259</SystemName><RcopiaPracticeUsername>" + Convert.ToString(dtconfig.Rows[0]["RcopiaPracticeUsername"]) + "</RcopiaPracticeUsername> " +
                            " <Request> <Command>" + Convert.ToString(dtconfig.Rows[0]["Command"]) + " </Command> " +
                            " <LastUpdateDate>" +"06/01/2019"+ "</LastUpdateDate> <Patient> " +
                            //" <RcopiaID>" + recopiaId2 + "</RcopiaID><ExternalID>" + patientId + " </ExternalID>" +
                            // DateTime.Now.ToString("MM/dd/yyyy 00:00:00").Replace("-", "/")
                            " <RcopiaID>" + recopiaId2 + "</RcopiaID><ExternalID>" + recopiaId + " </ExternalID>" +
                            " </Patient> <Status>all</Status> " +
                            " </Request></RCExtRequest>";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Convert.ToString(dtconfig.Rows[0]["URL"]));
                request.Method = "POST";
                request.ContentType = "application/text";


                request.ContentLength = xmlData.Length;
                using (Stream webStream = request.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                {
                    requestWriter.Write(xmlData);
                }

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();

                        DataSet dsResultset = new DataSet();
                        dsResultset.ReadXml(new StringReader(response));
                        DataTable dtDrug = new DataTable();

                        dtDrug = dsResultset.Tables["Drug"];

                        if (dtDrug !=null && dtDrug.Rows.Count > 0)
                        {
                            DataTable InsertTable = GetTable();

                            for (int i = 0; i < dtDrug.Rows.Count; i++)
                            {
                                InsertTable.Rows.Add();

                                InsertTable.Rows.Add(patientId, dtDrug.Rows[i]["DrugDescription"].ToString(), dtDrug.Rows[i]["Strength"].ToString()
                                                     , dtDrug.Rows[i]["Schedule"].ToString(), DateTime.Now.ToString("MM/dd/yyyy 00:00:00").Replace("-", "/"));

                                InsertTable.AcceptChanges();


                            }
                            string xmlICD = ConvertDatatableToXML(InsertTable);

                            sqlPrescripTion = " EXEC [USP_SAVE_MEDICINE] '" + xmlICD + "'";

                            result = (string)db.GetSingleValue(sqlPrescripTion);
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

        #region ConvertDatatableToXML**************************************************************************************************************************
        /// <summary>
        /// ConvertDatatableToXML
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>string</returns>
        private string ConvertDatatableToXML(DataTable dt)
        {
            System.IO.MemoryStream objMemoryStream = new MemoryStream();
            dt.TableName = "XMLData";
            dt.WriteXml(objMemoryStream, true);
            objMemoryStream.Seek(0, SeekOrigin.Begin);
            StreamReader objStreamReader = new StreamReader(objMemoryStream);
            string xmlstr;
            xmlstr = objStreamReader.ReadToEnd();
            return (xmlstr);
        }
        #endregion

        #region GetTable***************************************************************************************************************************************
        /// <summary>
        /// GetTable
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PatientID", typeof(string));
            table.Columns.Add("MedicineName", typeof(string));
            table.Columns.Add("Dosage", typeof(string));
            table.Columns.Add("Frequency", typeof(string));
            table.Columns.Add("Date", typeof(string));
            return table;
        }
        #endregion

        #region Get Vitals List By Patient Id
        public List<clsObservation> GetVitalsList(string patientId)
        {
            List<clsObservation> lstDetails = new List<clsObservation>();
            DataTable dtObservation = BindVitals(patientId);

            if (dtObservation.Rows.Count > 0)
            {
                for (int index = 0; index <= dtObservation.Rows.Count - 1; index++)
                {
                    clsObservation objObservation = new clsObservation();
                    objObservation.BloodPressure = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["BloodPressure_L"]))+"/"+ _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["BloodPressure_R"]));
                    objObservation.Temperature = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Temperature"]));
                    objObservation.Height = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Height_L"]))+"'"+ _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Height_R"])) + "''";
                    objObservation.Pulse = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Pulse"]));
                    objObservation.Respiratory = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Respiratory"]));
                    objObservation.AppointmentDate = Convert.ToString(dtObservation.Rows[index]["Date"]);
                    objObservation.AppointmentFromTime = Convert.ToString(dtObservation.Rows[index]["FromTime"]);
                    objObservation.AppointmentToTime = Convert.ToString(dtObservation.Rows[index]["ToTime"]);
                    objObservation.pWeight = _sharedService.Decrypt(Convert.ToString(dtObservation.Rows[index]["Weight"]));
                    lstDetails.Add(objObservation);
                }
            }
            return lstDetails;
        }

        private DataTable BindVitals(string patientId)
        {
            DataTable dtObservation = new DataTable();
            string sqlObservation = "EXEC USP_GETPATIENTVITALLIST '" + patientId + "'";
            try
            {
                dtObservation = db.GetData(sqlObservation);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtObservation;
        }
        #endregion


    }
}
