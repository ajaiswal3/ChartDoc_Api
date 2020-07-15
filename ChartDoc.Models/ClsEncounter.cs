using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsEncounter : Class
    /// </summary>
    public class clsEncounter
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string summary { get; set; }
        public string doctorId { get; set; }
        public string id { get; set; }
        public string encounterDate { get; set; }
        public string name { get; set; }
        public string encounterNote { get; set; }
        #endregion
    }
}