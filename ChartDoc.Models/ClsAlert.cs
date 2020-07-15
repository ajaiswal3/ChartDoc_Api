using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsAlert : Class
    /// </summary>
    public class clsAlert
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string patientId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        #endregion
    }
}