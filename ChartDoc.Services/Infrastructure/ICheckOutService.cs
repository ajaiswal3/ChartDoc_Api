using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface ICheckOutService
    {
        string UpdateCheckOut(string AppointmentID, string flg);
    }
}
