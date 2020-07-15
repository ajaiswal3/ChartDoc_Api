using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// AllergyImmunizationAlertSocialFamilyService : Class
    /// </summary>
    public class AllergyImmunizationAlertSocialFamilyService : IAllergyImmunizationAlertSocialFamilyService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region Constructor************************************************************************************************************************************
        /// <summary>
        /// AllergyImmunizationAlertSocialFamilyService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public AllergyImmunizationAlertSocialFamilyService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region SaveAllergyImmunization***********************************************************************************************************************
        /// <summary>
        /// SaveAllergyImmunization : Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="allergies">string</param>
        /// <param name="immunizations">string</param>
        /// <param name="alert">string</param>
        /// <param name="families">string</param>
        /// <param name="socials">string</param>
        /// <returns>string</returns>
        public string SaveAllergyImmunization(string patientId, string allergies, string immunizations, string alert, string families, string socials)
        {
            string sqlAllergyImmunization = " EXEC [USP_SAVE_ALLERGIES_IMMUNIZATION_ALERT] '" + patientId + "' ,'" + allergies + "','" + immunizations + "','"  + alert + "','" + families + "','" + socials + "'";
            
            string  result = (string)db.GetSingleValue(sqlAllergyImmunization);
            return result;
        }
        #endregion


    }
}
