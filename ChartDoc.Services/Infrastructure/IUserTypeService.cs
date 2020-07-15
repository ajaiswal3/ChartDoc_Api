using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IUserTypeService
    {
        List<clsUType> GetUType(string Id);
    }
}
