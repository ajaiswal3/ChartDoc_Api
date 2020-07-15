using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientBilling : Class
    /// </summary>
    public class clsPatientBilling
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string billingParty { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string addLine { get; set; }
        public string addLine1 { get; set; }
        public string addCity { get; set; }
        public string addState { get; set; }
        public string addZip { get; set; }
        public string SSN { get; set; }
        public string driversLicenseFilePath { get; set; }
        public string primaryPhone { get; set; }
        public string secondaryPhone { get; set; }
        #endregion
    }
}