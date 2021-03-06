﻿using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IUserService
    {
        clsUser GetUser(string userName, string password);
        List<clsDoctor> GetAllDoctorsDetails(string date);
        List<clsDoctorList> GetUserList(string userType);
        string UpdateStatusofUser(int id, int status);
        string SaveUser(string id,string iUser, string fullname, string email);
    }
}
