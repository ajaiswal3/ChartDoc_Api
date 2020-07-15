using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface ICalendarService
    {
        List<clsCalender> GetCalender();
        string SaveCalender(clsCalender Calendr);
        string DeleteCalender(string ID);
    }
}
