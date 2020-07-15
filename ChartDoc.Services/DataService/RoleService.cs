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
    /// public class RoleService : IRoleService
    /// </summary>
    public class RoleService : IRoleService
    {
        #region Instance Variable
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region RoleService Constructor************************************************************************************************************************
        /// <summary>
        /// RoleService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public RoleService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        #endregion

        #region GetRole****************************************************************************************************************************************
        /// <summary>
        /// GetRole
        /// </summary>
        /// <returns>List<clsRole></returns>
        public List<clsRole> GetRole()
        {
            List<clsRole> lstDetails = new List<clsRole>();
            DataTable dtRole = BindRole();
            for (int index = 0; index <= dtRole.Rows.Count - 1; index++)
            {
                clsRole objRole = new clsRole();
                objRole.pId= Convert.ToString(dtRole.Rows[index]["ID"]);
                objRole.pCcDescription = Convert.ToString(dtRole.Rows[index]["PageName"]);
                lstDetails.Add(objRole);
            }
            return lstDetails;
        }
        #endregion

        #region BindRole***************************************************************************************************************************************
        /// <summary>
        /// BindRole
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindRole()
        {
            DataTable dtRole = new DataTable();
            string sqlRole = "SELECT [ID],[PageName] FROM[tblMenuList1]";
            dtRole = context.GetData(sqlRole);
            return dtRole;
        }
        #endregion
    }
}
