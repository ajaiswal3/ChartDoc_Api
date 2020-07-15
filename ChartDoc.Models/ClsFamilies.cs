using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsFamilies : Class
    /// </summary>
    public class clsFamilies
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string patientId { get; set; }
        public string member { get; set; }
        public string diseases { get; set; }
        #endregion
    }
}