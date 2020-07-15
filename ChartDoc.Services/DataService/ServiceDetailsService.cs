using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// public class ServiceDetailsService : IServiceDetailsService
    /// </summary>
    public class ServiceDetailsService : IServiceDetailsService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region ServiceDetailsService Constructor**************************************************************************************************************
        /// <summary>
        /// ServiceDetailsService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        public ServiceDetailsService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        #endregion

        #region DeleteServiceDetails***************************************************************************************************************************
        /// <summary>
        /// DeleteServiceDetails
        /// </summary>
        /// <param name="sService">clsService</param>
        /// <returns>string</returns>
        public string DeleteServiceDetails(clsService sService)
        {
            string flag = "0";
            string sqlService = " EXEC USP_SAVESERVICE_DELETE '" + sService.serviceId + "' ";
            flag = (string)context.GetSingleValue(sqlService);
            return flag;
        }
        #endregion

        #region GetServiceDetails******************************************************************************************************************************
        /// <summary>
        /// GetServiceDetails
        /// </summary>
        /// <returns>List<clsService></returns>
        public List<clsService> GetServiceDetails()
        {
            List<clsService> lstDetails = new List<clsService>();
            DataTable dtService = GetServices();
            for (int index = 0; index <= dtService.Rows.Count - 1; index++)
            {
                clsService objService = new clsService();
                objService.serviceId = Convert.ToString(dtService.Rows[index]["SERVICEID"]);
                objService.serviceName = Convert.ToString(dtService.Rows[index]["SERVICENAME"]);
                lstDetails.Add(objService);
            }
            return lstDetails;
        }
        #endregion

        #region SaveServiceDetails*****************************************************************************************************************************
        /// <summary>
        /// SaveServiceDetails
        /// </summary>
        /// <param name="sService">clsService</param>
        /// <returns>string</returns>
        public string SaveServiceDetails(clsService sService)
        {
            string flag = "0";
            string sqlService = " EXEC USP_SAVESERVICE_ADD_EDIT '" + sService.serviceId + "' ,'" + sService.serviceName + "'";
            flag = (string)context.GetSingleValue(sqlService);
            return flag;
        }
        #endregion

        #region GetServices************************************************************************************************************************************
        /// <summary>
        /// GetServices
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable GetServices()
        {
            DataTable dtService = new DataTable();
            string sqlService = "SELECT  [SERVICEID] ,[SERVICENAME]  FROM [M_SERVICE]";
            dtService = context.GetData(sqlService);
            return dtService;
        }
        #endregion
    }
}
