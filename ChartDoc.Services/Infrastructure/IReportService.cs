using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
   public interface IReportService
    {
        List<clsPartyLedger> GetPartyLedger(string fromDate, string toDate, string patientName);
        List<clsPatientBalance> GetPatientBalance(string patientName);
    }
}
