using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsUserObj : Class
    /// </summary>
    public class clsUserObj
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string fullName { get; set; }
        public string dateOfBirth { get; set; }
        public string SSN{ get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string imagePath { get; set; }
        public string roleType { get; set; }
        public string roleSubType { get; set; }
        public string specialityType { get; set; }
        public string roleList {get;set;}
        #endregion
    }
}