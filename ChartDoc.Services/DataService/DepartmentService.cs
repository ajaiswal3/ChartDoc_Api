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
    /// DepartmentService : Public Class
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region DepartmentService Constructor******************************************************************************************************************
        /// <summary>
        /// DepartmentService : Public Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public DepartmentService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        #endregion

        #region GetDept****************************************************************************************************************************************
        /// <summary>
        /// GetDept : Public Method
        /// </summary>
        /// <returns>List<clsDept></returns>
        public List<clsDept> GetDept()
        {
            List<clsDept> lstDetails = new List<clsDept>();
            DataTable dtDept = BindDept();
            for (int index = 0; index <= dtDept.Rows.Count - 1; index++)
            {
                clsDept objDept = new clsDept();
                objDept.pId = Convert.ToString(dtDept.Rows[index]["DepartmentID"]);
                objDept.pCcDescription = Convert.ToString(dtDept.Rows[index]["DepartmentName"]);
                lstDetails.Add(objDept);
            }
            return lstDetails;
        }
        #endregion

        #region BindDept***************************************************************************************************************************************
        /// <summary>
        /// BindDept : Private Method
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindDept()
        {
            DataTable dtDept = new DataTable();
            string sql = "SELECT [DepartmentID] ,[DepartmentName] FROM [M_Department]";
            dtDept = context.GetData(sql);
            return dtDept;
        }
        #endregion

        #region DeleteDept***************************************************************************************************************************
        /// <summary>
        /// DeleteDepartment
        /// </summary>
        /// <param name="sService">clsDept</param>
        /// <returns>string</returns>
        public string DeleteSAVEDEPARTMENT(clsDept dept)
        {
            string flag = "0";
            string sqlService = " EXEC USP_SAVEDEPARTMENT_DELETE '" + dept.pId + "' ";
            flag = (string)context.GetSingleValue(sqlService);
            return flag;
        }
        #endregion

        #region SaveDepartment*****************************************************************************************************************************
        /// <summary>
        /// SaveDepartment
        /// </summary>
        /// <param name="sService">clsDept</param>
        /// <returns>string</returns>
        public string SaveDepartment(clsDept dept)
        {
            string flag = "0";
            string sqlService = " EXEC USP_SAVEDEPARTMENT_ADD_EDIT '" + dept.pId + "' ,'" + dept.pCcDescription + "'";
            flag = (string)context.GetSingleValue(sqlService);
            return flag;
        }
        #endregion
    }
}
