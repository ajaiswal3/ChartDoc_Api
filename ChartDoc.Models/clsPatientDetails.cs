using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientDetails : Class
    /// </summary>
    public class clsPatientDetails
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string addressLine { get; set; }
        public string addressLine1 { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public string addressPostalCode { get; set; }
        public string addressCountry { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string mobNo { get; set; }
        public string imageName { get; set; }
        public string imagePath { get; set; }
        public string flag { get; set; }
        public string age { get; set; }
        public string recopiaId { get; set; }
        public string recopiaName { get; set; }
        public string primaryPhone { get; set; }
        public string secondaryPhone { get; set; }
        public string providerName { get; set; }
        public string policyNo { get; set; }
        public string effectiveFrom { get; set; }
        #endregion
    }
}