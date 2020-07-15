using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IReasonService
    {
        List<clsReason> GetReason(string Type);
        string DeleteReason(clsReason sReason);
        string SaveReason(clsReason sReason);
    }
}
