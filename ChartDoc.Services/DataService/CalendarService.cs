using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// CalendarService : Class
    /// </summary>
    public class CalendarService : ICalendarService
    {
        #region Instance variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region CalendarService Constructor********************************************************************************************************************
        /// <summary>
        /// CalendarService : Constructor
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public CalendarService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region GetCalender************************************************************************************************************************************
        /// <summary>
        /// GetCalender : Method
        /// </summary>
        /// <returns>List<clsCalender></returns>
        public List<clsCalender> GetCalender()
        {
            List<clsCalender> calenderDetailsList = new List<clsCalender>();
            DataTable dtCalender = BindCalender();
            for (int index = 0; index <= dtCalender.Rows.Count - 1; index++)
            {
                clsCalender objCalenderDetails = new clsCalender();

                objCalenderDetails.id = Convert.ToString(dtCalender.Rows[index]["id"]);
                objCalenderDetails.tag = Convert.ToString(dtCalender.Rows[index]["TAG"]);
                objCalenderDetails.tagDesc = Convert.ToString(dtCalender.Rows[index]["TAGDesc"]);
                objCalenderDetails.doctorId = Convert.ToString(dtCalender.Rows[index]["DoctorID"]);
                objCalenderDetails.doctor = Convert.ToString(dtCalender.Rows[index]["Doctor"]);
                objCalenderDetails.date = Convert.ToString(dtCalender.Rows[index]["Date"]);
                objCalenderDetails.fromTime = Convert.ToString(dtCalender.Rows[index]["FromTime"]);
                objCalenderDetails.toTime = Convert.ToString(dtCalender.Rows[index]["ToTime"]);
                objCalenderDetails.eventReason = Convert.ToString(dtCalender.Rows[index]["EventReason"]);
                objCalenderDetails.booingTag = Convert.ToString(dtCalender.Rows[index]["BooingTag"]);
                objCalenderDetails.bookingDesc = Convert.ToString(dtCalender.Rows[index]["BookingDesc"]);
                calenderDetailsList.Add(objCalenderDetails);
            }
            return calenderDetailsList;
        }
        #endregion

        #region SaveCalender***********************************************************************************************************************************
        /// <summary>
        /// SaveCalender : Method
        /// </summary>
        /// <param name="calendr">clsCalender</param>
        /// <returns>string</returns>
        public string SaveCalender(clsCalender calendr)
        {
            string result = string.Empty;
            result = SavCalnder(calendr);
            return result;
        }
        #endregion

        #region DeleteCalender*********************************************************************************************************************************
        /// <summary>
        /// DeleteCalender : Method
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>string</returns>
        public string DeleteCalender(string id)
        {
            string flag = "0";
            string sqlCalender = " Delete from T_OfficeCalander where id='" + id + "'";
            flag = (string)db.GetSingleValue(sqlCalender);
            return flag;
        }
        #endregion

        #region SavCalnder*************************************************************************************************************************************
        /// <summary>
        /// SavCalnder : Method
        /// </summary>
        /// <param name="clndr">clsCalender</param>
        /// <returns>string</returns>
        private string SavCalnder(clsCalender calender)
        {
            string resCalender = string.Empty;
            string sqlCalender = "EXEC USP_OfficeCalander '" + calender.id + "','" + calender.tag + "','" + calender.doctorId + "','" + calender.date + "','" + calender.fromTime
                + "','" + calender.toTime + "','" + calender.eventReason + "','" + calender.booingTag + "'";


            resCalender = (string)db.GetSingleValue(sqlCalender);
            return resCalender;
        }
        #endregion

        #region BindCalender***********************************************************************************************************************************
        /// <summary>
        /// BindCalender : Method
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable BindCalender()
        {
            DataTable dtCalender = new DataTable();
            string sqlBindCalender = "USP_BindCalender";
            dtCalender = db.GetData(sqlBindCalender);
            return dtCalender;
        }
        #endregion
    }
}
