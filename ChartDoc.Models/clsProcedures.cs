using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsProcedures : Class
    /// </summary>
    public class clsProcedures
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public DateTime procedureDate { get; set; }
        public string drName { get; set; }
        public string drProfile { get; set; }
        public string procedureDesc { get; set; }
        public int id { get; set; }
        #endregion
    }
}