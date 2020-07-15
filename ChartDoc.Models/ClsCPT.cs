using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsCPT : Class
    /// </summary>
    public class clsCPT:IDisposable
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string patientId { get; set; }
        public string code { get; set; }
        public string desc { get; set; }
        public decimal? chargeAmount { get; set; }
        public bool disposed;
        #endregion
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsCPT()
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
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            disposed = true;
        }
    }
}