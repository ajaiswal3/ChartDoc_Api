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
    public class FamilyService : IFamilyService
    {
        #region Instance Variable
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region FamilyService Constructor**********************************************************************************************************************
        /// <summary>
        /// FamilyService : Public Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public FamilyService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetFamilies************************************************************************************************************************************
        /// <summary>
        /// GetFamilies : Public Method To Get Families By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns></returns>
        public List<clsFamilies> GetFamilies(string patientId)
        {
            List<clsFamilies> lstDetails = new List<clsFamilies>();
            DataTable dtFamilies = BindFamilies(patientId);
            for (int index = 0; index <= dtFamilies.Rows.Count - 1; index++)
            {
                clsFamilies Details = new clsFamilies();
                Details.id = Convert.ToString(dtFamilies.Rows[index]["id"]);
                Details.patientId = Convert.ToString(dtFamilies.Rows[index]["PatientId"]);
                Details.member = Convert.ToString(dtFamilies.Rows[index]["Member"]);
                Details.diseases = Convert.ToString(dtFamilies.Rows[index]["Diseases"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion

        #region BindFamilies***********************************************************************************************************************************
        /// <summary>
        /// BindFamilies : Private Method To Get Families By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindFamilies(string patientId)
        {
            DataTable dtFamilies = new DataTable();
            string sql = "SELECT [Id] ,[PatientId], [Member] ,[Diseases]  FROM [dbo].[t_Family] Where [PatientId]='" + patientId + "'";
            dtFamilies = db.GetData(sql);
            return dtFamilies;
        }
        #endregion


        #region GetAlert************************************************************************************************************************************
        /// <summary>
        /// GetFamilies : Public Method To Get Patient Alert By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns></returns>
        public List<clsAlert> GetAlert(string patientId)
        {
            List<clsAlert> lstDetails = new List<clsAlert>();
            DataTable dtFamilies = BindAlert(patientId);
            for (int index = 0; index <= dtFamilies.Rows.Count - 1; index++)
            {
                clsAlert Details = new clsAlert();
                //clsFamilies Details = new clsFamilies();
                Details.id = Convert.ToString(dtFamilies.Rows[index]["id"]);
                Details.patientId = Convert.ToString(dtFamilies.Rows[index]["PatientId"]);
                Details.code = Convert.ToString(dtFamilies.Rows[index]["Code"]);
                Details.description = Convert.ToString(dtFamilies.Rows[index]["Description"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion

        #region BindFamilies***********************************************************************************************************************************
        /// <summary>
        /// BindFamilies : Private Method To Get Patient Alert By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindAlert(string patientId)
        {
            DataTable dtFamilies = new DataTable();
            string sql = "SELECT [Id] ,[PatientId], [Code] ,[Description]  FROM [dbo].[T_Alert] Where [PatientId]='" + patientId + "'";
            dtFamilies = db.GetData(sql);
            return dtFamilies;
        }
        #endregion
    }
}
