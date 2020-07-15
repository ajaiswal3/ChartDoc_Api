using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IFollowupService
    {
        List<clsFollowUp> GetFollowUp(string AppointmentId);
        string SaveFollowup(clsFollowUp objFollowup);
    }
}
