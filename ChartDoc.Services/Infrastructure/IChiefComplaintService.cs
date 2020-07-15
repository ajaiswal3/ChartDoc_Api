using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IChiefComplaintService
    {
        string SaveChiefComplaint(clsCC cc);
        List<clsCC> GetChiefComplaint(string AppointmentId);
    }
}
