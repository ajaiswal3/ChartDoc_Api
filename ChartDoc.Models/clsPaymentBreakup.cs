using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsPaymentBreakup : IDisposable
    {
        public int id { get; set; }
        public string freeTicketNo { get; set; }
        public string appointmentDate { get; set; }
        public int appointmentId { get; set; }
        public decimal amount { get; set; }//due amount
        public decimal paidAmount { get; set; }
        public int payId { get; set; }

        public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsPaymentBreakup()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }
}
