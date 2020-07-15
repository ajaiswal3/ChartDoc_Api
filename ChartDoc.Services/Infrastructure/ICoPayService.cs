using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface ICoPayService
    {
        List<clsCOPay> GetCOPay(string AppointmentID);
        string SaveCoPay(clsCOPay CoPay);
    }
}
