using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChartDoc.Models
{
    public class clsClaimHeader : IDisposable
    {
        public string date { get; set; }
        public string patientName { get; set; }
        public string patientId { get; set; }
        public string feeTicketNo { get; set; }
        public int appointmentId { get; set; }
        public decimal claimBalance { get; set; }
        public decimal patientBalance { get; set; }
        public int statusId { get; set; }
        public string status { get; set; }
        public int chargeId { get; set; }
        public int modeOfTransaction { get; set; }
        public string doctorName { get; set; }
        public string doctorId { get; set; }
        public int insuranceId1 { get; set; }
        public string insuranceName1 { get; set; }
        public int insuranceId2 { get; set; }
        public string insuranceName2 { get; set; }
        public int insuranceId3 { get; set; }
        public string insuranceName3 { get; set; }
        public int locationId { get; set; }
        public string locationName { get; set; }
        public int placeOfId { get; set; }
        public string placeOfName { get; set; }
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public int referenceId { get; set; }
        public string referenceName { get; set; }
        public int fileAsId { get; set; }
        public string fileAsName { get; set; }
        public int reasonId { get; set; }
        public decimal? paidAmount { get; set; }
        public int paidBy { get; set; }
        public string typeEM { get; set; }
        public int claimstatusId { get; set; }
        public bool? denied { get; set; }
        public bool disposed = false;
        public string policy1 { get; set; }
        public string policy2 { get; set; }
        public string policy3 { get; set; }

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        ~clsClaimHeader()
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
