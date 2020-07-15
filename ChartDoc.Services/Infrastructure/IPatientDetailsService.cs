using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IPatientDetailsService
    {
       // List<clsPatientDetails> GetAllPatients();
        List<clsPatientDetails> SearchPatient(string firstName, string lastName, string DOB, string mobNo, string emailId, string gender);
        clsPatientDetails SearchPatientbyID(string ID);
    }
}
