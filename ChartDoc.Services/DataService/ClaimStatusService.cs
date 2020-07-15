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
    public class ClaimStatusService : IClaimStatusService
    {
        #region Instance variables
        private readonly ILogService logService;
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// ClaimStatusService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ClaimStatusService(IConfiguration configaration, ILogService logService)
        {
            db._configaration = configaration;
            this.logService = logService;
        }
        #endregion

        #region Public Method
        #region Get Completed ClaimStatus List
        /// <summary>
        /// ClaimStatus : Get Claim Status List
        /// </summary>

        /// <returns></returns>
        public List<clsClaimStatus> GetClaimStatus()
        {
            List<clsClaimStatus> claimStatus = new List<clsClaimStatus>();
            DataTable dtClaimStatus = BindClaimStatusList();

            foreach (DataRow item in dtClaimStatus.Rows)
            {
                using (clsClaimStatus status = new clsClaimStatus())
                {
                    try
                    {
                        status.id = Convert.ToInt32(item["id"]);
                        status.name = Convert.ToString(item["Name"]);
                        claimStatus.Add(status);
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(typeof(ClaimService));
                        logger.Error(ex);
                    }
                }
            }
            return claimStatus;
        }
        #endregion
        #endregion

        #region Private Method
        #region Bind Claim Status List
        private DataTable BindClaimStatusList()
        {
            DataTable dtClaimStatus = new DataTable();
            string sqlClaimStatus = "EXEC USP_GETCLAIMSTATUS ";
            try
            {
                dtClaimStatus = db.GetData(sqlClaimStatus);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(sqlEx);
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(nullEx);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimService));
                logger.Error(ex);
            }
            return dtClaimStatus;
        }
        #endregion
        #endregion
    }
}
