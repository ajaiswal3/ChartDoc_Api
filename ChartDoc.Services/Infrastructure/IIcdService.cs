using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IIcdService
    {
        List<clsICD> GetAllICD();
        List<clsICD> GetSavedICD(string PatientId);
        string SaveICD(int appointmentId, string icd);
    }
}
