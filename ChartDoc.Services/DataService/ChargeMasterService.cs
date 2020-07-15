using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChartDoc.Services.DataService
{
    public class ChargeMasterService : IChargeMasterService
    {
        #region Instance variables
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// ChargeMasterService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ChargeMasterService(IConfiguration configaration, ILogService logService)
        {
            db._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region Public Method
        #region Get Charge Details
        /// <summary>
        /// ChargeMasterService : Get Charge Details
        /// </summary>
        /// <param name="chargeYearId"></param>
        /// <returns></returns>
        public List<clsChargeMaster> GetChargeDetails(int chargeYearId)
        {
            List<clsChargeMaster> chargeMasterList = new List<clsChargeMaster>();
            DataTable dtChargeMaster = BindChargeDetails(chargeYearId);
            foreach (DataRow rowChargeMaster in dtChargeMaster.Rows)
            {
                using (clsChargeMaster chargeMaster = new clsChargeMaster())
                {
                    try
                    {
                        chargeMaster.id = Convert.ToInt32(rowChargeMaster[0]);
                        chargeMaster.chargeYearId = Convert.ToInt32(rowChargeMaster[1]);
                        chargeMaster.cptId = Convert.ToInt32(rowChargeMaster[2]);
                        chargeMaster.cptCode = Convert.ToString(rowChargeMaster[3]);
                        chargeMaster.cptDescription = Convert.ToString(rowChargeMaster[4]);
                        chargeMaster.amount = Convert.ToDecimal(rowChargeMaster[5]);
                        chargeMasterList.Add(chargeMaster);
                    }
                    catch(Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ChargeMasterService));
                        logger.Error(ex);
                    }
                }
            }
            return chargeMasterList;
        }
        #endregion

        #region Save Charge Details
        /// <summary>
        /// ChargeMasterService : Save Charge Details
        /// </summary>
        /// <param name="chargeYearId"></param>
        /// <param name="chargeDetailsXml"></param>
        /// <returns></returns>
        public string SaveChargeDetails(int chargeYearId, string chargeDetailsXml)
        {
            string resChargeDetails = string.Empty;
            string sqlChargeDetails = "EXEC USP_SaveChargeMaster '" + chargeDetailsXml + "', " + chargeYearId;
            try
            {
                resChargeDetails = (string)db.GetSingleValue(sqlChargeDetails);
            }
            catch (SqlException sqlEx)
            {
                resChargeDetails = sqlEx.Message;
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                resChargeDetails = nullEx.Message;
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                resChargeDetails = ex.Message;
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(ex);
            }
            return resChargeDetails;
        }
        #endregion
        #endregion

        #region Private Method
        #region Bind Charge Details
        /// <summary>
        /// ChargeMasterService : Bind Charge Details
        /// </summary>
        /// <param name="chargeYearId"></param>
        /// <returns></returns>
        private DataTable BindChargeDetails(int chargeYearId)
        {
            DataTable dtChargeDetails = new DataTable();
            string sqlChargeDetails = "EXEC USP_GetChargeMaster " + chargeYearId;
            try
            {
                dtChargeDetails = db.GetData(sqlChargeDetails);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ChargeMasterService));
                logger.Error(ex);
            }
            
            return dtChargeDetails;
        }
        #endregion
        #endregion

    }
}
