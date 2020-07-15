using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsUser : Class
    /// </summary>
    public class clsUser
    {
        #region Public Properties******************************************************************************************************************************
        public string departmentId { get; set; }
        public string department { get; set; }
        public string code { get; set; }
        public string errorDesc { get; set; }
        public string errorCode { get; set; }
        public string userId { get; set; }
        public string userType { get; set; }
        public string applicableTo { get; set; }
        public string fName { get; set; }
        public string utName { get; set; }
        public string userName { get; set; }
        public string iUserId { get; set; }
        public string userTag { get; set; }
        #endregion

    }
}