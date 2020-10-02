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
        string ValidatePatient(string patientId, string patientFName, string patientMName, string patientLName, string patientAddr1, string patientAddr2, string dob, string ssn, string contact);
    }
}
