using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IScheduleAppointmentService
    {
        string UpdateMarkReady(string AppointmentID, string Flag);
    }
}
