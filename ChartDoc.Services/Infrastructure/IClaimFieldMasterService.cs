using ChartDoc.Models;
using System.Collections.Generic;

namespace ChartDoc.Services.Infrastructure
{
    public interface IClaimFieldMasterService
    {
        List<clsClaimFieldMaster> GetClaimFieldMaster();
        string SaveClaimFieldMaster(clsClaimFieldMaster dateRange);
    }
}