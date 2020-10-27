using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    ///ChiefComplaintService : Public Class 
    /// </summary>
    public class ChiefComplaintService : IChiefComplaintService
    {
        #region Instance variables*****************************************************************************************************************************
        private readonly ILogService logService;
        ISharedService _sharedService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region ChiefComplaintService Constructor**************************************************************************************************************
        /// <summary>
        /// ChiefComplaintService : Parameterized Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public ChiefComplaintService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            db._configaration = configaration;
            this._sharedService = sharedService;
            this.logService = logService;
        }
        #endregion

        #region GetChiefComplaint******************************************************************************************************************************
        /// <summary>
        /// GetChiefComplaint : Public Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>List<clsCC></returns>
        public List<clsCC> GetChiefComplaint(string patientId)
        {
            List<clsCC> cCDetailsList = new List<clsCC>();
            DataTable dtChiefComplaint = BindCc(patientId);
            for (int index = 0; index <= dtChiefComplaint.Rows.Count - 1; index++)
            {
                using (clsCC details = new clsCC())
                {
                    details.pId = Convert.ToString(dtChiefComplaint.Rows[index]["Id"]);
                    details.pccDescription = _sharedService.Decrypt(Convert.ToString(dtChiefComplaint.Rows[index]["Description"]));
                    cCDetailsList.Add(details);
                }
            }
            return cCDetailsList;
        }
        #endregion

        #region BindCc*****************************************************************************************************************************************
        /// <summary>
        /// BindCc : Private Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindCc(string patientId)
        {
            DataTable dtCC = new DataTable();
            string sqlCC = "SELECT ID,  [Description] FROM [t_ClientComplaint] " + " WHERE [AppointmentId]='" + patientId + "'";
            try
            {
                dtCC = db.GetData(sqlCC);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(this.GetType());
                logger.Error(ex);
            }
            return dtCC;
        }
        #endregion

        #region SaveChiefComplaint*****************************************************************************************************************************
        /// <summary>
        /// SaveChiefComplaint : Public Method
        /// </summary>
        /// <param name="cc">clsCC</param>
        /// <returns>string</returns>
        public string SaveChiefComplaint(clsCC cc)
        {
            string result = string.Empty;
            string sqlChiefComplaint = " EXEC [USP_SaveChiefComplaint_V2] '" + cc.pId + "' ,'" + cc.pccDescription + "'";
            result = (string)db.GetSingleValue(sqlChiefComplaint);
            return result;
        }
        #endregion
    }
}
