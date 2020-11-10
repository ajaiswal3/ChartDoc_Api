using ChartDoc.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ChartDoc.Models;
using System.Xml;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using ChartDoc.DAL;

namespace ChartDoc.Services.DataService
{
    public class MenuService : IMenuService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;

        #endregion

        #region MenuService Constructor*******************************************************************************************************************************
        /// <summary>
        /// MenuService : Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public MenuService(IConfiguration configuration)
        {
            db._configaration = configuration;

        }
        #endregion

        #region Private Method*********************************************************************************************************************************
        /// <summary>
        /// GetMenu : Private Method
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        private DataSet GetMenu()
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_GETMENUDETAILS";
            ds = db.ExecuteDs(sql);
            return ds;
            //string sqlDiagnosis = "SELECT [Id], [PatientId] ,[DiagnosisDate] ,[DiagnosisDesc]  FROM [dbo].[T_Diagnosis] Where PatientId = '" + patientId + "'";
            //dtDiagnosis = db.GetData(sqlDiagnosis);
            //return dtDiagnosis;
        }
        #endregion

        #region GetMenu************************************************************************************************************************
        /// <summary>
        /// GetMenudata : Public Method
        /// </summary>
        /// <param >string</param>
        /// <returns>List<clsDiagnosis></returns>
        public ClsMeuList GetMenudata()
        {
            ClsMeuList lstdata = new ClsMeuList();
            DataSet ds = GetMenu();
            DataTable dtmenu = ds.Tables[0];
            DataTable dtsubmenu = ds.Tables[1];
            List<ClsMenu> clsMenus = new List<ClsMenu>();
            for (int index = 0; index <= dtmenu.Rows.Count - 1; index++)
            {
                ClsMenu clsMenu = new ClsMenu();
                clsMenu.menuId= Convert.ToString(dtmenu.Rows[index]["menuId"]);
                clsMenu.menuName = Convert.ToString(dtmenu.Rows[index]["menuName"]);
                clsMenu.className = Convert.ToString(dtmenu.Rows[index]["class"]);
                clsMenus.Add(clsMenu);
            }

            List<ClsSubMenu> clsSubMenus = new List<ClsSubMenu>();
            for (int index = 0; index <= dtsubmenu.Rows.Count - 1; index++)
            {
                ClsSubMenu clsSubMenu = new ClsSubMenu();
                clsSubMenu.menuId = Convert.ToString(dtsubmenu.Rows[index]["menuId"]);
                clsSubMenu.submenu = Convert.ToString(dtsubmenu.Rows[index]["submenu"]);
                clsSubMenu.submenuRoute = Convert.ToString(dtsubmenu.Rows[index]["submenuRoute"]);
                clsSubMenu.parentmenuId = Convert.ToString(dtsubmenu.Rows[index]["parentmenuId"]);
                clsSubMenu.events = Convert.ToString(dtsubmenu.Rows[index]["Event"]);
                clsSubMenu.queryParamsvalue = Convert.ToString(dtsubmenu.Rows[index]["queryParamsvalue"]);
                clsSubMenus.Add(clsSubMenu);
            }
            lstdata.clsMenu = new List<ClsMenu>();
            lstdata.clsMenu = clsMenus;
            lstdata.clssubMenu = new List<ClsSubMenu>();
            lstdata.clssubMenu = clsSubMenus;

            return lstdata;
        }
        #endregion
    }
}
