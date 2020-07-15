using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsAllergyImmunization : Class
    /// </summary>
    public class clsAllergyImmunization
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public clsAllergies[] allergies { get; set; }
        public clsImmunizations[] immunizations { get; set; }
        public clsAlert alert { get; set; }
        public clsSocials[] socials { get; set; }
        public clsFamilies[] families { get; set; }
        #endregion
    }
}