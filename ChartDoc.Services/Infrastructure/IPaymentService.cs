using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IPaymentService
    {
        clsPayment GetPaymentDetails(string patientId);
        List<clsPaymentDetails> GetPaymentList();
        string SavePaymentDetails(int paymentId, string xmlPaymentDetails, string xmlPaymentBreakup);
    }
}
