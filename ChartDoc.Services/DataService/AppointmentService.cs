using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// AppointmentService : Class
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        #region Instance variables*****************************************************************************************************************************
        private readonly ILogService logService;
        DBUtils context = DBUtils.GetInstance;
        private readonly ISmsService smsService;
        private readonly IEmailService emailService;
        #endregion

        #region AppointmentService Constructor*****************************************************************************************************************
        /// <summary>
        /// AppointmentService : Method
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public AppointmentService(IConfiguration configaration, ILogService logService, ISmsService smsService, IEmailService emailService)
        {
            context._configaration = configaration;
            this.logService = logService;
            this.smsService = smsService;
            this.emailService = emailService;
        }
        #endregion

        #region GetAppointment*********************************************************************************************************************************
        /// <summary>
        /// GetAppointment : Method, not implemented yet.
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>clsScheduleAppointment</returns>
        public clsScheduleAppointment GetAppointment(string date)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SaveActiveAppointment**************************************************************************************************************************
        /// <summary>
        /// SaveActiveAppointment : Method
        /// </summary>
        /// <param name="appointment">clsAppointment</param>
        /// <returns>string</returns>
        public string SaveActiveAppointment(clsAppointment appointment)
        {
            string resActiveAppointment = string.Empty;
            string sqlActiveAppointment = "EXEC USP_SAVE_APPOINTMENT " + appointment.appointmentId + ",'" 
                + appointment.appointmentNo + "','" 
                + appointment.patientId + "','" 
                + appointment.patientName + "','" 
                + appointment.address + "','" 
                + appointment.contactNo + "','" 
                + appointment.doctorId + "','" 
                + appointment.date + "','" 
                + appointment.fromTime + "','" 
                + appointment.toTime + "','" 
                + appointment.reasonCode + "','" 
                + appointment.reasonDescription + "','"
                + appointment.tag + "','" 
                + appointment.reasonId + "','" 
                + appointment.reason + "'," 
                + appointment.isReady + "," 
                + appointment.positionId + ",'" 
                + appointment.positionName + "','" 
                + appointment.roomNo + "','" 
                + appointment.flowArea + "'," 
                + appointment.serviceId + ",'" 
                + appointment.note + "','" 
                + appointment.gender + "','" 
                + appointment.email + "','" 
                + appointment.dob + "','" 
                + appointment.imageUrl + "'";
            resActiveAppointment = (string)context.GetSingleValue(sqlActiveAppointment);

            try
            {
                string toEmail = appointment.email;
                string body = " Hi "    + appointment.patientName + " Your Booking has been confirmed. Date =" + appointment.date + " From Time : " 
                                        + appointment.fromTime + " and ToTime =" + appointment.toTime + "  Thanks & Regards. ChartDoc Team";
                if (sqlActiveAppointment.Substring(0, 1) == "1")
                {
                    smsService.SendMessage(appointment);
                    emailService.sendEmail(appointment);
                }
                //SendMail(tomail, body);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(AppointmentService));
                logger.Error(ex);
                return resActiveAppointment;
            }
            return resActiveAppointment;
        }
        #endregion

        #region SendMail***************************************************************************************************************************************
        /// <summary>
        /// SendMail : Method
        /// </summary>
        /// <param name="toAddress">string</param>
        /// <param name="mailBody">string</param>
        private void SendMail(string toAddress, string mailBody)
        {
            string uId = "chartdoc.test@gmail.com";
            string pwd = "chart@4321";

            MailMessage oMailMessage = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            oMailMessage.From = new MailAddress(uId);
            oMailMessage.To.Add(toAddress);
            oMailMessage.Subject = "Booking Appointment";
            oMailMessage.Body = mailBody;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(uId, pwd);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(oMailMessage);
        }
        #endregion
    }
}
