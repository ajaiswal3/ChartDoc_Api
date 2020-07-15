using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IDiagnosisService
    {
        List<clsDiagnosis> GetDiagnosisByPatientId(string PatientId);
        string SaveDiagnosis(string xmlDoc, string xmlDiagnosis, string patientId);
    }
}
