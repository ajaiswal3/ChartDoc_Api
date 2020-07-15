using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsDoctorList : Class
    /// </summary>
    public class clsDoctorList
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string doctorName { get; set; }
        public string doctorImage { get; set; }
        public string dateOfBirth { get; set; }
        public string ssn { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string roleType { get; set; }
        public string roleSubType { get; set; }
        public string specialtyType { get; set; }
        public string roleList { get; set; }
        public int isActive { get; set; }
        #endregion
    }
}