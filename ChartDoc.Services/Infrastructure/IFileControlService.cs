using ChartDoc.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChartDoc.Services.Infrastructure
{
    public interface IFileControlService
    {
        Task<clsPatientSocial> SaveSocialImage(clsPatientSocial data, IFormFile uploadFile);
        Task<clsPatientAuthorization> SaveAuthorizationImage(clsPatientAuthorization data, IFormFile uploadFile);
        Task<clsPatientBilling> SaveBillingImage(clsPatientBilling data, IFormFile file);
        Task<clsPatientInsurance> SaveInsuranceImage(clsPatientInsurance data, IFormFile uploadFile);

        clsUserObj SaveUserImage(clsUserObj iUser, IFormFileCollection file);
        clsAppointment SaveAppointmentImage(clsAppointment appointment, IFormFileCollection file);
        List<clsDocument> SaveProcedureImage(string PatientId, IFormFileCollection UploadFiles);
        List<clsDocument> SaveDiagnosisImage(string PatientId, IFormFileCollection UploadFiles);

         Task<clsPatientDetails> SavePatientProfileImage(clsPatientDetails data, IFormFile uploadFile);


    }
}
