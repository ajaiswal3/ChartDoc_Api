using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;
namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// ImmunizationService : Public Class
    /// </summary>
    public class ImmunizationService : IImmunizationService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region ImmunizationService Constructor****************************************************************************************************************
        /// <summary>
        /// ImmunizationService : Parameterized Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public ImmunizationService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetImmunizations*******************************************************************************************************************************
        /// <summary>
        /// GetImmunizations
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsImmunizations></returns>
        public List<clsImmunizations> GetImmunizations(string patientId)
        {
            List<clsImmunizations> lstDetails = new List<clsImmunizations>();
            DataTable dtImmunizations = BindImmunizations(patientId);
            for (int index = 0; index <= dtImmunizations.Rows.Count - 1; index++)
            {
                clsImmunizations objDetails = new clsImmunizations();
                objDetails.id = Convert.ToString(dtImmunizations.Rows[index]["id"]);
                objDetails.patientId = Convert.ToString(dtImmunizations.Rows[index]["PatientId"]);
                objDetails.code = Convert.ToString(dtImmunizations.Rows[index]["Code"]);
                objDetails.description = Convert.ToString(dtImmunizations.Rows[index]["Description"]);
                objDetails.date = Convert.ToString(dtImmunizations.Rows[index]["ImmunizationDate"]);
                lstDetails.Add(objDetails);
            }
            return lstDetails;
        }
        #endregion

        #region BindImmunizations******************************************************************************************************************************
        /// <summary>
        /// BindImmunizations
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindImmunizations(string patientId)
        {
            DataTable dtImmunizations = new DataTable();
            string sqlImmunizations = "SELECT [Id] ,[PatientId], [Code] ,[Description],convert(varchar(10),[Date],103) as  [ImmunizationDate]  FROM [dbo].[t_Immunizations] Where [PatientId]='" + patientId + "'";
            dtImmunizations = db.GetData(sqlImmunizations);
            return dtImmunizations;
        }
        #endregion
    }
}
