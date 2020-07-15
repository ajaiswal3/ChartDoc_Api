using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsImmunizations : Class
    /// </summary>
    public class clsImmunizations
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string patientId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        #endregion
    }
}