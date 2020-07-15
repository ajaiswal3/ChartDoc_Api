using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsPatientChargeClaimFields : Class
    /// </summary>
    public class clsPatientChargeClaimFields : IDisposable
    {
        #region Public Properties******************************************************************************************************************************
        public int chargeid { get; set; }
        public int fieldid { get; set; }
        public string value { get; set; }
       
        public bool disposed;
        #endregion

        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsPatientChargeClaimFields()
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