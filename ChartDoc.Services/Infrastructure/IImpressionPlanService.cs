using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IImpressionPlanService
    {
        string SaveImpressionPlan(clsImpression impression);
        List<clsImpression> GetImpressionPlan(string AppointmentId);
    }
}
