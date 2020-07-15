using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsReason : Class
    /// </summary>
    public class clsReason
    {
        #region Public Properties******************************************************************************************************************************
        public int reasonId { get; set; }
        public string reasonCode { get; set; }
        public string reasonDescription { get; set; }
        public string patientName { get; set; }
        public string type { get; set; }
        #endregion
    }
}