using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IChargeDateRangeService
    {
        List<clsChargeDateRange> GetChargeDateRange(string fromDate = "", string toDate = "");
        string SaveChargeDateRange(clsChargeDateRange dateRange);
    }
}
