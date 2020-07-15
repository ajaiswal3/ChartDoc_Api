using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsCalender : Class
    /// </summary>
    public class clsCalender
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string tag { get; set; }
        public string tagDesc { get; set; }
        public string doctorId { get; set; }
        public string doctor { get; set; }
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string eventReason { get; set; }
        public string booingTag { get; set; }
        public string bookingDesc { get; set; }
        #endregion
    }
}