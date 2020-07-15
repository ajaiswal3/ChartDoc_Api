using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientInsurance : Class
    /// </summary>
    public class clsPatientInsurance
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string providerName { get; set; }
        public string providerId { get; set; }
        public string insurancePolicy { get; set; }
        public string policyType { get; set; }
        public string policyTypeId { get; set; }
        public string cardImageFilePath { get; set; }
        public string effectiveFrom { get; set; }
        public string status { get; set; }
        public string statusId { get; set; }
        #endregion
    }
}