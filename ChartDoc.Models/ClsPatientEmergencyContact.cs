using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientEmergencyContact : Class
    /// </summary>
    public class clsPatientEmergencyContact
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string contactName { get; set; }
        public string contactPhone { get; set; }
        public string relationship { get; set; }
        #endregion
    }
}