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
    /// public class SocialService : ISocialService
    /// </summary>
    public class SocialService : ISocialService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region SocialService Constructor
        /// <summary>
        /// SocialService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        public SocialService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetSocials*************************************************************************************************************************************
        /// <summary>
        /// GetSocials
        /// </summary>
        /// <param name="PatientId">string</param>
        /// <returns>List<clsSocials></returns>
        public List<clsSocials> GetSocials(string PatientId)
        {
            List<clsSocials> lstDetails = new List<clsSocials>();
            DataTable dtSocials = BindSocials(PatientId);
            for (int index = 0; index <= dtSocials.Rows.Count - 1; index++)
            {
                clsSocials objSocials = new clsSocials();
                objSocials.id = Convert.ToString(dtSocials.Rows[index]["id"]);
                objSocials.patientId = Convert.ToString(dtSocials.Rows[index]["PatientId"]);
                objSocials.addiection = Convert.ToString(dtSocials.Rows[index]["Addiection"]);
                objSocials.frequency = Convert.ToString(dtSocials.Rows[index]["Frequency"]);
                objSocials.duration = Convert.ToString(dtSocials.Rows[index]["Duration"]);
                lstDetails.Add(objSocials);
            }
            return lstDetails;
        }
        #endregion


        #region BindSocials************************************************************************************************************************************
        /// <summary>
        /// BindSocials
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>string</returns>
        private DataTable BindSocials(string patientId)
        {
            DataTable dtSocials = new DataTable();
            string sqlSocials = "SELECT [Id] ,[PatientId], [Addiection] ,[Frequency], [Duration]  FROM [dbo].[t_Social] Where [PatientId]='" + patientId + "'";
            dtSocials = db.GetData(sqlSocials);
            return dtSocials;
        }
        #endregion
    }
}
