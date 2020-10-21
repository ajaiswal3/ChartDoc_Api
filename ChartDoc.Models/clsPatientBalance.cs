using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsPatientBalance : IDisposable
    {
        public string patientID { get; set; }
        public string patientName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public decimal? totalBillValue { get; set; }
        public decimal? alreadyPaid { get; set; }
        public decimal? totalOutstanding { get; set; }
       
        public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsPatientBalance()
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
