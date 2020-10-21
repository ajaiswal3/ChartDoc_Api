using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsPaymentDetails : IDisposable
    {
        public string paymentDetailId { get; set; }
        public string paymentId { get; set; }
        public string patientId { get; set; }
        public string PatientName { get; set; }
        public int typeOfTxnId { get; set; }
        public string typeOfTxnName { get; set; }
        public int reasonId { get; set; }
        public string reasonName { get; set; }
        public int instrumentTypeId { get; set; }
        public string instrumentTypeName { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string paymentDate { get; set; }
        public decimal amount { get; set; }
        public string transferId { get; set; }
        public string transferName { get; set; }

        public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsPaymentDetails()
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
