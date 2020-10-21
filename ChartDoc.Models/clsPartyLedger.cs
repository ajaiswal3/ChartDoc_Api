using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace ChartDoc.Models
{
   public class clsPartyLedger : IDisposable
    {
        public string date { get; set; }
        public string particulars { get; set; }
        public decimal? amount { get; set; }
        public string txnType { get; set; }
        public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsPartyLedger()
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
