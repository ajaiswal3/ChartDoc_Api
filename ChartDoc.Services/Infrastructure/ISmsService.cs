﻿using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface ISmsService
    {
        void SendMessage(clsAppointment appointment);
    }
}
