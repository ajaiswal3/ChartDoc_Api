using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IInsuranceService
    {
        List<string> GetInsurance(string PatientID);
    }
}
