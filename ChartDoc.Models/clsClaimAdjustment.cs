using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsClaimAdjustment : IDisposable
    {
        public int autoId { get; set; }
        public int chargeId { get; set; }
        public int insuranceId { get; set; }
        public string insuranceName { get; set; }
        public string groupName { get; set; }
        public decimal amount { get; set; }
        public string reasonId { get; set; }

        public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsClaimAdjustment()
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
