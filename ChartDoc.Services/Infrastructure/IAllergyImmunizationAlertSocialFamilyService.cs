using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IAllergyImmunizationAlertSocialFamilyService
    {
        string SaveAllergyImmunization(string patientId, string allergies, string immunizations, string alert, string families, string socials);
    }
}
