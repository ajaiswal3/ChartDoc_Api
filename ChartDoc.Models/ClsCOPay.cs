using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsCOPay : Class
    /// </summary>
    public class clsCOPay
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string appointmentId { get; set; }
        public string paymentType { get; set; }
        public string refNo1 { get; set; }
        public string refNo2 { get; set; }
        public string paymentDate { get; set; }
        public string amount { get; set; }
        public string addLine { get; set; }
        #endregion


    }
}