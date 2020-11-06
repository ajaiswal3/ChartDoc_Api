using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChartDoc.Services.DataService
{
    public class UserAccessService: IUserAccessService
    {
        #region Instance Variable
        DBUtils context = DBUtils.GetInstance;
        private readonly ILogService logService;
        #endregion

        public UserAccessService(IConfiguration configaration, ILogService logService)
        {
            context._configaration = configaration;
            this.logService = logService;
        }

        private DataTable GetUserAccessControlsDetails(int userTypeId)
        {
            DataTable dtUserAccessDetails = new DataTable();
            string sqClaimFieldsDetails = "exec [dbo].[USP_GETUSERRIGHTS] " + userTypeId + "";
            dtUserAccessDetails = context.GetData(sqClaimFieldsDetails);
            return dtUserAccessDetails;
        }

        public List<UserAccessDetailsDTO> GetUserAccessDetailsByUserType(int userTypeId)
        {
            List<UserAccessDetailsDTO> lstDetails = new List<UserAccessDetailsDTO>();
            
            DataTable dtUserAccessDetails = GetUserAccessControlsDetails(userTypeId);
            for (int index = 0; index <= dtUserAccessDetails.Rows.Count - 1; index++)
            {
                UserAccessDetailsDTO objAccessDetails = new UserAccessDetailsDTO();
                objAccessDetails.PageNane = dtUserAccessDetails.Rows[index]["PageName"].ToString();
                objAccessDetails.ID = Convert.ToInt32(dtUserAccessDetails.Rows[index]["ID"]);
                objAccessDetails.Status = dtUserAccessDetails.Rows[index]["Status"].ToString();
                
                lstDetails.Add(objAccessDetails);
            }
            return lstDetails;
        }
    }
}
