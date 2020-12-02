using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class InsuranceResponseDTO
    {
        public InsuranceData data { get; set; }
        public bool IsActiveInsurance { get; set; }
        public bool HasInsuranceData { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }

    public class InsuranceData
    {
        public string InsuranceProvidor { get; set; }
        public string PolicyHolder { get; set; }
        public string CoPAyAmount { get; set; }
        public string InsuranceDetails { get; set; }
        public string PatientId { get; set; }
    }
}
