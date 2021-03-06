﻿using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IEmailService
    {
        void sendEmail(clsAppointment appointment);
        void sendUserEmail(string fullName, string email);

    }
}
