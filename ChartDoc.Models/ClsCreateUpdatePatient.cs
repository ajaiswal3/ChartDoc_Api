using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsCreateUpdatePatient : Class
    /// </summary>
    public class clsCreateUpdatePatient
    {
        #region Public Properties******************************************************************************************************************************
        public clsPatientDetails sPatientDetails { get; set; }
        public clsPatientAuthorization sPatientAuthorisation { get; set; }
        public clsPatientBilling sPatientBilling { get; set; }
        public clsPatientEmergencyContact sPatientEmergency { get; set; }
        public clsPatientEmployerContact sPatientEmpContact { get; set; }
        public List<clsPatientInsurance> sPatientInsurance { get; set; }
        public clsPatientSocial sPatientSocial { get; set; }
        #endregion
    }
}