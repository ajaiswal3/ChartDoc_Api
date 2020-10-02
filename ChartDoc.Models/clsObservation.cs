using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsObservation : Class
    /// </summary>
    public class clsObservation
    {
        #region Public Properties******************************************************************************************************************************
        public string patientId { get; set; }
        public string pId { get; set; }
        public string pBloodPressureL { get; set; }
        public string pBloodPressureR { get; set; }
        public string pTemperature { get; set; }
        public string pHeightL { get; set; }
        public string pHeightR { get; set; }
        public string pWeight { get; set; }
        public string pPulse { get; set; }
        public string pRespiratory { get; set; }
        public string BloodPressure { get; set; }
        public string Temperature { get; set; }
        public string Height { get; set; }
        public string Pulse { get; set; }
        public string Respiratory { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentFromTime { get; set; }
        public string AppointmentToTime { get; set; }
        #endregion
    }
}