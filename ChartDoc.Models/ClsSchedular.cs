using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsSchedular : Class
    /// </summary>
    public class clsSchedular
    {
        #region Public Properties******************************************************************************************************************************
        public string doctorId { get; set; }
        public string doctorName { get; set; }
        public string patientId { get; set; }
        public string patientName { get; set; }
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        #endregion
    }
}
