using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IUserAccessService
    {
        List<UserAccessDetailsDTO> GetUserAccessDetailsByUserType(int userTypeId);
        string GetUserSignatureRequest(string userName);
        string GetResponseStatus(string sig_response);
    }
}
