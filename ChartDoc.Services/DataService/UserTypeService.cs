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
    /// public class UserTypeService : IUserTypeService
    /// </summary>
    public class UserTypeService : IUserTypeService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region UserTypeService Constructor********************************************************************************************************************
        /// <summary>
        /// UserTypeService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public UserTypeService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        #endregion

        #region GetUType***************************************************************************************************************************************
        /// <summary>
        /// GetUType
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>List<clsUType></returns>
        public List<clsUType> GetUType(string id)
        {
            List<clsUType> lstDetails = new List<clsUType>();
            DataTable dtUser = BindUser(id);
            for (int index = 0; index <= dtUser.Rows.Count - 1; index++)
            {
                clsUType objUType  = new clsUType();
                objUType.utCode = Convert.ToString(dtUser.Rows[index]["UTCODE"]);
                objUType.userDescription = Convert.ToString(dtUser.Rows[index]["UTDESCRIPTION"]);
                lstDetails.Add(objUType);
            }
            return lstDetails;
        }
        #endregion

        #region BindUser***************************************************************************************************************************************
        /// <summary>
        /// BindUser
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindUser(string id)
        {
            DataTable dtUser = new DataTable();
            string sqlUser = "SELECT [UTCODE] ,[UTDESCRIPTION]  FROM [M_USERTYPE] where UTID=" + id;
            dtUser = context.GetData(sqlUser);
            return dtUser;
        }
        #endregion
    }
}
