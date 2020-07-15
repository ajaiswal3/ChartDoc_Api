using log4net;
using System;

namespace ChartDoc.Services.Infrastructure
{
    public interface ILogService
    {
        ILog GetLogger(Type type);
    }
}
