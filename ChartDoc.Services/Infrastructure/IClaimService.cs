﻿using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IClaimService
    {
        List<clsClaimHeader> GetAppointmentList(string fromDate, string toDate, string providerId, string statusId,string patientName,string feeTicket);
        clsClaimHeader GetChargePatientHeader(int appointmentId);
        List<clsClaimDetails> GetChargePatientDetails(int appointmentId);
        List<clsClaimAdjustment> GetChargePatientAdjustment(int chargeId);
        string SaveClaim(int chargeId, string xmlHeader, string xmlDetails, string xmlAdjustment, string isDelete);
    }
}
