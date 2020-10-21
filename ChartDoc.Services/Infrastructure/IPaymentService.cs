using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IPaymentService
    {
        clsPayment GetPaymentDetails(string paymentId, string patientId);
        List<clsPaymentDetails> GetPaymentList(string fromDate, string toDate, string patientName);
        string SavePaymentDetails(int paymentId, string xmlPaymentDetails, string xmlPaymentBreakup, string isdelete);
        List<clsPaymentBreakup> GetPaymentBreakUp(string paymentId, string patientId);
    }
}
