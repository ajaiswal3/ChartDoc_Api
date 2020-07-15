using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsFollowUp : Class
    /// </summary>
    public class clsFollowUp
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string pId { get; set; }
        public string pFollowupDate { get; set; }
        public string pFollowupPeriod { get; set; }
        #endregion
    }
}