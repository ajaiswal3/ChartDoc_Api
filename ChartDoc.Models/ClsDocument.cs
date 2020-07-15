using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsDocument : Class
    /// </summary>
    public class clsDocument
    {
        #region Public Properties******************************************************************************************************************************
        public string path { get; set; }
        public string fileName { get; set; }
        public string documentType { get; set; }
        public string patientId { get; set; }
        public int procDiagId { get; set; }
        #endregion
    }
}