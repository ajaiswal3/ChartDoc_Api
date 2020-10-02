using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IDepartmentService
    {
        List<clsDept> GetDept();
        string SaveDepartment(clsDept objdept);
        string DeleteSAVEDEPARTMENT(clsDept dept);
    }
}
