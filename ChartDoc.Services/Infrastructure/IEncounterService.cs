using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IEncounterService
    {
        List<clsEncounter> GetEncounter(string PatientId);
        string SaveEncounter(clsEncounter encounter);
    }
}
