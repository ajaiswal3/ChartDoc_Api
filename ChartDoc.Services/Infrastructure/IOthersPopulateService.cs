using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IOthersPopulateService
    {
        List<clsOtherPopulate> OtherPopulate(string Id);
        string SaveOthersDetails(clsOtherPopulate sOthers);
        string DeleteOthersDetails(clsOtherPopulate sOthers);
    }
}
