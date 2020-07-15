using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IClaimFieldsService
    {
        List<clsClaimFieldsHeader> GetClaimFieldsHeader();
        List<clsClaimFieldsDetails> GetClaimFieldsDetails(string id);
        List<clsClaimFieldsDetails> GetClaimFieldsMasterDetails(int id);
        string SavePatientChargeClaimFields(int chargeId, string xlmPatientChargeClaimFields, string typeEM);

        List<clsPatientChargeClaimFields> GetInsuranceClaimFields(string chargeId);
        string SaveClaimFieldMaster(int claimFieldId, string name, char type, string isDeleted, string xmlDetails);
    }
}
