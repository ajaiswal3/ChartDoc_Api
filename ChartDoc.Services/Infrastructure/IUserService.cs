using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChartDoc.Services.Infrastructure
{
    public interface IUserService
    {
        clsUser GetUser(string userName, string password);
        List<clsDoctor> GetAllDoctorsDetails(string date);
        List<clsDoctorList> GetUserList(string userType);
        string UpdateStatusofUser(int id, int status);
        string SaveUser(string id,string iUser, string fullname, string email);
        ResetPasswordDTO GetResponseValidateUserEmail(string userEmail);
        ResetPasswordDTO GetResponseResetPassword(string userEmail, string userPassword);
        InsuranceResponseDTO ValidatePatientInsurance(string patientId);
        int ResetPasswordEmail(SendEmailResetPasswordParams emailParams);
        Task<string> Authenticate(string userName, string password);
        string SaveTemplate(int id, string title, string description, char tag);
        TemplateDTO TemplateByTemplateId(int id);
        int CreatePasswordEmail(SendEmailCreatePasswordParams emailParams);
    }
}
