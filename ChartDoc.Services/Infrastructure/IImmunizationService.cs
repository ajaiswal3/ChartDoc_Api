﻿using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IImmunizationService
    {
        List<clsImmunizations> GetImmunizations(string PatientId);
    }
}
