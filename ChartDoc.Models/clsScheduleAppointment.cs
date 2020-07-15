using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsScheduleAppointment : Class
    /// </summary>
    public class clsScheduleAppointment
    {
        #region Public Properties******************************************************************************************************************************
        public int appointmentId { get; set; }
        public string doctorId { get; set; }
        public string doctorName { get; set; }
        public string patientId { get; set; }
        public string patientName { get; set; }
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string colorCode { get; set; }
        public string isReady { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string serviceId { get; set; }
        public string positionId { get; set; }
        public string note { get; set; }
        public string reasonId { get; set; }
        public string reason { get; set; }
        public string appointmentFromTime { get; set; }
        public string appointmentToTime { get; set; }
        public string roomNo { get; set; }
        public string filePath { get; set; }
        #endregion
    }
}