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
    public class ChargeDateRangeService : IChargeDateRangeService
    {
        #region Instance variables
        DBUtils db = DBUtils.GetInstance;
        private readonly ILogService logService;
        #endregion

        #region Constructor
        /// <summary>
        /// ChargeDateRangeService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ChargeDateRangeService(IConfiguration configaration, ILogService logService)
        {
            db._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region Public Method
        #region Get Charge Date Range
        /// <summary>
        /// ChargeDateRangeService : Get Charge Date Range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<clsChargeDateRange> GetChargeDateRange(string fromDate = "", string toDate = "")
        {
            List<clsChargeDateRange> chargeDateRangeList = new List<clsChargeDateRange>();
            DataTable dtChargeDateRange = BindChargeDateRange(fromDate, toDate);
            foreach (DataRow rowChargeDateRange in dtChargeDateRange.Rows)
            {
                using (clsChargeDateRange dateRange = new clsChargeDateRange())
                {
                    try
                    {
                        dateRange.id = Convert.ToInt32(rowChargeDateRange["ID"]);
                        dateRange.startDate = Convert.ToString(rowChargeDateRange["StartDate"]);
                        dateRange.endDate = Convert.ToString(rowChargeDateRange["EndDate"]);
                        dateRange.description = Convert.ToString(rowChargeDateRange["Description"]);
                        dateRange.status = Convert.ToString(rowChargeDateRange["Status"]);
                        chargeDateRangeList.Add(dateRange);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                        logger.Error(ex);
                    }

                }
            }
            return chargeDateRangeList;
        }
        #endregion

        #region Save Charge Date Range
        /// <summary>
        /// ChargeDateRangeService : Save Charge Date Range
        /// </summary>
        /// <param name="dateRange"></param>
        /// <returns></returns>
        public string SaveChargeDateRange(clsChargeDateRange dateRange)
        {
            string resChargeYear = string.Empty;
            string sqlChargeYear = "EXEC USP_Save_ChargeYear " + dateRange.id + ",'" + dateRange.startDate + "','" + dateRange.endDate + "','" + dateRange.status + "'";
            try
            {
                resChargeYear = (string)db.GetSingleValue(sqlChargeYear);
            }
            catch (SqlException sqlEx)
            {
                resChargeYear = sqlEx.Message;
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                resChargeYear = nullEx.Message;
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                resChargeYear = ex.Message;
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(ex);
            }
            return resChargeYear;
        }
        #endregion
        #endregion

        #region Private Method
        #region Bind Charge Date Range
        /// <summary>
        /// ChargeDateRangeService : Bind Charge Date Range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private DataTable BindChargeDateRange(string fromDate, string toDate)
        {
            DataTable dtChargeYear = new DataTable();

            string sqlChargeYear = "EXEC USP_GET_ChargeYear '" + fromDate + "', '" + toDate + "'";
            try
            {
                dtChargeYear = db.GetData(sqlChargeYear);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ChargeDateRangeService));
                logger.Error(ex);
            }
            return dtChargeYear;
        }
        #endregion
        #endregion
    }
}
