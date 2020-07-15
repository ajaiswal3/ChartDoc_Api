using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IFlowAreaService
    {
        string UpdateFlowArea(string AppointmentID, string RoomNO, string Flowarea);
    }
}
