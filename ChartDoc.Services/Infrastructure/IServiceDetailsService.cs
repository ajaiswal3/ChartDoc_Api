using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IServiceDetailsService
    {
        List<clsService> GetServiceDetails();
        string SaveServiceDetails(clsService sservice);
        string DeleteServiceDetails(clsService sservice);
    }
}
