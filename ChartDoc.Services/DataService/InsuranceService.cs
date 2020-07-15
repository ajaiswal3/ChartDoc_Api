using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// InsuranceService : Public Class
    /// </summary>
    public class InsuranceService : IInsuranceService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region InsuranceService Constructor*******************************************************************************************************************
        /// <summary>
        /// InsuranceService : Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public InsuranceService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region GetInsurance***********************************************************************************************************************************
        /// <summary>
        /// GetInsurance
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<string></returns>
        public List<string> GetInsurance(string patientId)
        {
            List<string> lstDetails = new List<string>();
            DataTable dtInsurance = db.GetData("EXEC   USP_fetch_Insurance '" + patientId + "'");
            for (int index = 0; index <= dtInsurance.Rows.Count - 1; index++)
            {
                string Details;
                Details = Convert.ToString(dtInsurance.Rows[index]["Desc"]);
                lstDetails.Add(Details);
            }
            return lstDetails;
        }
        #endregion
    }
}
