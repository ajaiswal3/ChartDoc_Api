using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// MedicineService : Public Class
    /// </summary>
    public class MedicineService : IMedicineService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region MedicineService Constructor********************************************************************************************************************
        /// <summary>
        /// MedicineService : Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public MedicineService(IConfiguration configuration, ILogService logService)
        {
            db._configaration = configuration;
            this.logService = logService;
        }
        #endregion

        #region GetMedicine************************************************************************************************************************************
        /// <summary>
        /// GetMedicine
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsMedicine></returns>
        public List<clsMedicine> GetMedicine(string patientId)
        {
            List<clsMedicine> lstDetails = new List<clsMedicine>();
            DataTable dtMedicine = BindMedicine(patientId);
            for (int index = 0; index <= dtMedicine.Rows.Count - 1; index++)
            {
                clsMedicine objMedicine = new clsMedicine();
                objMedicine.medicineName = Convert.ToString(dtMedicine.Rows[index]["MedicineName"]);
                objMedicine.dosage = Convert.ToString(dtMedicine.Rows[index]["Dosage"]);
                objMedicine.frequency = Convert.ToString(dtMedicine.Rows[index]["Frequency"]);
                objMedicine.date = Convert.ToString(dtMedicine.Rows[index]["Date"]);
                lstDetails.Add(objMedicine);
            }
            return lstDetails;
        }
        #endregion


        #region BindMedicine***********************************************************************************************************************************
        /// <summary>
        /// BindMedicine
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindMedicine(string patientId)
        {
            DataTable dtMedicine = new DataTable();
            string sqlMedicine = " SELECT  [Id] ,[PatientId] ,[MedicineName] ,[Dosage] " +
                             " ,[Frequency] ,[AppointmentID],CONVERT(VARCHAR(20),[Date] ,103) AS DATE FROM [T_ Medicine] WHERE PatientId='" + patientId + "'";
            try
            {
                dtMedicine = db.GetData(sqlMedicine);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtMedicine;
        }
        #endregion

    }
}
