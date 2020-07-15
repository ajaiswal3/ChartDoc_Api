using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ChartDoc.Services.Infrastructure
{
    public interface IProcedureService
    {
        List<clsProcedures> GetProcedureByPatientId(string PatientId);
        clsProcedures GetDoctorProfile(string appointmentId, clsProcedures procedures);
        string SaveProcedure(string xmlDoc, string xmlProcedures, string patientId);
    }
}
