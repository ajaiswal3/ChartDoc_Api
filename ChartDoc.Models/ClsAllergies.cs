using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ChartDoc.Models
{
    /// <summary>
    /// clsAllergies : Class
    /// </summary>
    public class clsAllergies : IDisposable
    {
        #region Public Properties******************************************************************************************************************************
        public string id { get; set; }
        public string patientId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public bool disposed;
        #endregion

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// ~clsAllergies : Destructor
        /// </summary>
        ~clsAllergies()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose : To free managed and unmanaged objects.
        /// </summary>
        /// <param name="disposing">bool</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
            }
            // Free any unmanaged objects here.

            disposed = true;
        }
    }
}