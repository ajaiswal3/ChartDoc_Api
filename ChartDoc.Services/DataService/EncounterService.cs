using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// EncounterService : Public Class
    /// </summary>
    public class EncounterService : IEncounterService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        ISharedService _sharedService;
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region EncounterService Constructor*******************************************************************************************************************
        /// <summary>
        /// EncounterService : Public Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public EncounterService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            context._configaration = configaration;
            this._sharedService = sharedService;
            this.logService = logService;
        }
        #endregion

        #region GetEncounter***********************************************************************************************************************************
        /// <summary>
        /// GetEncounter : To Get Encounters List by Patient Id
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsEncounter></returns>
        public List<clsEncounter> GetEncounter(string patientId)
        {
            List<clsEncounter> lstDetails = new List<clsEncounter>();
            DataTable dtEncounters = BindEncounter(patientId);
            clsEncounter objEncounter = null;
            for (int index = 0; index <= dtEncounters.Rows.Count - 1; index++)
            {
                objEncounter = new clsEncounter();
                objEncounter.id = Convert.ToString(dtEncounters.Rows[index]["id"]);
                objEncounter.name = Convert.ToString(dtEncounters.Rows[index]["DoctorName"]);
                objEncounter.encounterNote = _sharedService.Decrypt(Convert.ToString(dtEncounters.Rows[index]["EncounterNote"]));
                objEncounter.doctorId = Convert.ToString(dtEncounters.Rows[index]["DoctorID"]);
                objEncounter.summary = _sharedService.Decrypt(Convert.ToString(dtEncounters.Rows[index]["Summary"]));
                objEncounter.encounterDate = Convert.ToString(dtEncounters.Rows[index]["EncounterDate"]);
                lstDetails.Add(objEncounter);
            }
            return lstDetails;
        }
        #endregion

        #region BindEncounter**********************************************************************************************************************************
        /// <summary>
        /// BindEncounter : Private Method To Get Encounters List by Patient Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>DataTable</returns>
        private DataTable BindEncounter(string patientId)
        {
            DataTable dtEncounter = new DataTable();
            string sqlEncounter = "SELECT ID,  [DoctorName],[Patient],[EncounterDate] ,[EncounterNote],[DoctorID],Summary FROM [Vw_Encounter_V2] " +
                " WHERE patientID='" + patientId + "' order by [EncounterDate] desc";
            try
            {
                dtEncounter = context.GetData(sqlEncounter);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtEncounter;
        }
        #endregion

        #region SaveEncounter**********************************************************************************************************************************
        /// <summary>
        /// SaveEncounter : Public Method To Save An Encounter.
        /// </summary>
        /// <param name="encounter">clsEncounter</param>
        /// <returns>string</returns>
        public string SaveEncounter(clsEncounter encounter)
        {
            string result = string.Empty;
            string sqlEncounter = " EXEC [USP_INSERTPATIEN_V2] '" + encounter.encounterNote + "' ,'" + encounter.summary + "' ,'" + encounter.patientId + "','" + encounter.doctorId + "'";
            result = (string)context.GetSingleValue(sqlEncounter);
            return result;
        }
        #endregion
    }
}
