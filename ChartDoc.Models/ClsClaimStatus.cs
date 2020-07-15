using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsClaimStatus : Class
    /// </summary>
    public class clsClaimStatus : IDisposable
    {
        #region Public Properties******************************************************************************************************************************
        public int id { get; set; }
        public string name { get; set; }
        public bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsClaimStatus()
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
        #endregion
    }
}