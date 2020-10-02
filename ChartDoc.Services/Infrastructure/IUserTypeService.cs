using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IUserTypeService
    {
        List<clsUType> GetUType(string Id);

        string DeleteRole(clsUType uType);
        string SaveRole(clsUType uType);
    }
}
