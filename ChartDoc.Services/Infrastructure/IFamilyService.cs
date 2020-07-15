using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IFamilyService
    {
        List<clsFamilies> GetFamilies(string PatientId);
        List<clsAlert> GetAlert(string PatientId);
    }
}
