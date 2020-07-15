using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsMedicine : Class
    /// </summary>
    public class clsMedicine
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string medicineName { get; set; }
        public string dosage { get; set; }
        public string frequency { get; set; }
        public string date { get; set; }
        #endregion
    }
}