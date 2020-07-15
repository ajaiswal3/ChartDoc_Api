using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsDiagnosis : Class
    /// </summary>
    public class clsDiagnosis
    {
        #region Public Properties******************************************************************************************************************************
        public int id { get; set; }
        public string patientId { get; set; }
        public DateTime diagnosisDate { get; set; }
        public string diagnosisDesc { get; set; }
        #endregion
    }
}