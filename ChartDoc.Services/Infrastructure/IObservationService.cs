using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IObservationService
    {
        List<clsObservation> GetObservation(string AppointmentId);
        string SaveObservation(clsObservation objObservation);
        List<clsObservation> GetVitalsList(string patientId);
    }
}
