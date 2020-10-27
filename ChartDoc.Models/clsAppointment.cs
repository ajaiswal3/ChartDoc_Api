using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsAppointment : Class
    /// </summary>
    public class clsAppointment
    {
        #region Public Properties******************************************************************************************************************************
        public int appointmentId { get; set; }
        public string appointmentNo { get; set; }
        public string patientId { get; set; }
        public string patientName { get; set; }
        public string address { get; set; }
        public string contactNo { get; set; }
        public string doctorId { get; set; }
        public string date { get; set; }
        public TimeSpan fromTime { get; set; }
        public TimeSpan toTime { get; set; }
        public string reasonCode { get; set; }
        public string reasonDescription { get; set; }
        public string tag { get; set; }
        public string reasonId { get; set; }
        public string reason { get; set; }
        public bool isReady { get; set; }
        public int positionId { get; set; }
        public string positionName { get; set; }
        public string roomNo { get; set; }
        public string flowArea { get; set; }
        public int serviceId { get; set; }
        public string note { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string imageUrl { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }

        #endregion
    }
}