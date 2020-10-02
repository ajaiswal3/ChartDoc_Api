using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientSocial : Class
    /// </summary>
    public class clsPatientSocial
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string maritalStatus { get; set; }
        public string guardianFName { get; set; }
        public string guardianLName { get; set; }
        public string addLine { get; set; }
        public string addCity { get; set; }
        public string addState { get; set; }
        public string addZip { get; set; }
        public string DOB { get; set; }
        public string patientSSN { get; set; }
        public string phoneNumber { get; set; }
        public string guardianSSN { get; set; }
        public string driversLicenseFilePath { get; set; }
        public string race { get; set; }
        public string ethicity { get; set; }
        public string language { get; set; }
        public string commMode { get; set; }
        public string socialMaritalStatusOther { get; set; }
        #endregion
    }
}