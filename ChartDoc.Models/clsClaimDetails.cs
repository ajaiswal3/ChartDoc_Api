using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsClaimDetails : IDisposable
    {
        public int chargeId { get; set; }
        public DateTime date { get; set; }
        public int cptId { get; set; }
        public string cptCode { get; set; }
        public string cptDesc { get; set; }
        public int icd1 { get; set; }
        public string icdCode1 { get; set; }
        public string icdDesc1 { get; set; }
        public int ? icd2 { get; set; }
        public string icdCode2 { get; set; }
        public string icdDesc2 { get; set; }
        public int ? icd3 { get; set; }
        public string icdCode3 { get; set; }
        public string icdDesc3 { get; set; }
        public int ? icd4 { get; set; }
        public string icdCode4 { get; set; }
        public string icdDesc4 { get; set; }
        public decimal chargeAmount { get; set; }
        public decimal allowedAmount { get; set; }
        public decimal deduction { get; set; }
        public decimal insAdjustment { get; set; }
        public decimal miscAdjustment { get; set; }
        public decimal copay { get; set; }
        public decimal balance { get; set; }
        public string capitated { get; set; }
        public string modifiedCode { get; set; }
        public int reasonId { get; set; }
        public decimal paymentReceived { get; set; }
        public decimal insuranceBalance { get; set; }

    public bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsClaimDetails()
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
