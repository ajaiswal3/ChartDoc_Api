using System;
using System.Data;
using ChartDoc.DAL;
using ChartDoc.Models;
using System.Collections.Generic;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// AllergiesService : Class
    /// </summary>
    public class AllergiesService : IAllergiesService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region Constructor************************************************************************************************************************************
        /// <summary>
        /// AllergiesService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public AllergiesService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetAllergies***********************************************************************************************************************************
        /// <summary>
        /// GetAllergies : Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsAllergies></returns>
        public List<clsAllergies> GetAllergies(string patientId)
        {
            List<clsAllergies> lstDetails = new List<clsAllergies>();            
            DataTable dtAllergies = BindAllergies(patientId);
            for (int index = 0; index <= dtAllergies.Rows.Count - 1; index++)
            {
                using (clsAllergies details = new clsAllergies())
                {
                    details.id = Convert.ToString(dtAllergies.Rows[index]["id"]);
                    details.patientId = Convert.ToString(dtAllergies.Rows[index]["PatientId"]);
                    details.code = Convert.ToString(dtAllergies.Rows[index]["Code"]);
                    details.description = Convert.ToString(dtAllergies.Rows[index]["Description"]);
                    lstDetails.Add(details);
                }
            }
            return lstDetails;
        }
        #endregion

        #region BindAllergies**********************************************************************************************************************************
        /// <summary>
        /// BindAllergies : Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindAllergies(string patientId)
        {
            DataTable dtAllergies = new DataTable();
            string sqldtAllergies = "SELECT [Id] ,[PatientId], [Code] ,[Description]  FROM [dbo].[t_Allergies] Where [PatientId]='" + patientId + "'";
            dtAllergies = db.GetData(sqldtAllergies);
            return dtAllergies;
        }
        #endregion
    }
}
