using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IAppointmentService
    {
        string SaveActiveAppointment(clsAppointment appt);
        clsScheduleAppointment GetAppointment(string date);
    }
}
