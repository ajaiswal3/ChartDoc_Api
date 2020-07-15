using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IPatientInformationService
    {
        clsCreateUpdatePatient GetPatientInfo(string PatientId);
        string CreatePatient(string patientId, string patientDetails, string patientBilling, string emergencyContact, string employerContact, string insurance, string social, string authorization);
    }
}
