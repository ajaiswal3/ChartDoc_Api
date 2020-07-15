using ChartDoc.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ChartDoc.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// ConfigureExceptionHandler: Extension method to handly error globaly.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="logService">ILogService</param>
        #region ConfigureExceptionHandler****************************************************************************************************************************
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogService logService)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var type = (((Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature)contextFeature).Error.TargetSite).DeclaringType;
                        var logger = logService.GetLogger(type);
                        logger.Error($" {contextFeature.Error}");

                        await context.Response.WriteAsync(new 
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
        #endregion
    }
}
