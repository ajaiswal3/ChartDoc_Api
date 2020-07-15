using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsFlowSheet : Class
    /// </summary>
    public class clsFlowSheet
    {
        #region Public Properties******************************************************************************************************************************
        public clsScheduleAppointment schduleAppoinment { get; set; }
        public clsWaitingArea waitingArea { get; set; }
        public clsConsultationRoom consultationRoom { get; set; }
        public clsCheckingOut checkingOut { get; set; }
        #endregion
    }
}