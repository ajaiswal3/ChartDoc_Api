using System;
using System.IO;
using log4net;
using log4net.Config;
using System.Reflection;
using ChartDoc.Services.Infrastructure;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// LogService : Public Class
    /// </summary>
    public class LogService : ILogService
    {
        #region GetLogger**************************************************************************************************************************************
        /// <summary>
        /// GetLogger
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>ILog</returns>
        public ILog GetLogger(Type type)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            return LogManager.GetLogger(type);
        }
        #endregion
    }
}
