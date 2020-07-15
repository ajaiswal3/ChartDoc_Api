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
    /// <summary>
    /// CategoryService : Class
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region CategoryService********************************************************************************************************************************
        /// <summary>
        /// CategoryService : Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public CategoryService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region GetCategoryDetails*****************************************************************************************************************************
        /// <summary>
        /// GetCategoryDetails : Public Method
        /// </summary>
        /// <returns>List<clsCategory></returns>
        public List<clsCategory> GetCategoryDetails()
        {
            List<clsCategory> categoryDetailsList = new List<clsCategory>();
            DataTable dtCategory = GetCategory();

            for (int index = 0; index <= dtCategory.Rows.Count - 1; index++)
            {
                clsCategory Details = new clsCategory();
                Details.id = Convert.ToString(dtCategory.Rows[index]["ID"]);
                Details.name = Convert.ToString(dtCategory.Rows[index]["NAME"]);
                categoryDetailsList.Add(Details);
            }
            return categoryDetailsList;
        }
        #endregion

        #region GetCategory************************************************************************************************************************************
        /// <summary>
        /// GetCategory : Method
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable GetCategory()
        {
            DataTable dtCategory = new DataTable();
            string sqlCategory = "SELECT TOP 1000 [ID] ,[NAME]  FROM [M_CATEGORY]";
            dtCategory = db.GetData(sqlCategory);
            return dtCategory;
        }
        #endregion
    }
}
