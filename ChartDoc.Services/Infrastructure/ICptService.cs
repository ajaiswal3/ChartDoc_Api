using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface ICptService
    {
        List<clsCPT> GetCPT();
        List<clsCPT> GetSavedCPT(string PatientId);
        string SaveCPT(string PatientID, string cpt);
        List<clsCPT> GetCPTWITHChargeAmount();
    }
}
