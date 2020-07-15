using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IFlowSheetService
    {
        List<clsFlowSheet> GetAppointment(string date);
        List<clsFlowSheet> GetFlowsheet(string date, string DoctorID);
        List<clsFlowSheet> GetOfficeCalenderList(string date);
        List<clsFlowSheet> AppointmentWeeklyView(string date, string DoctorID);
    }
}
