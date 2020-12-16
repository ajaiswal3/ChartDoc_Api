using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace ChartDoc.Services.DataService
{
    public class EmailService : IEmailService
    {
        DBUtils context = DBUtils.GetInstance;
        string emailMsgBody;
        string emailMsgSubject;
        string emailServerIp;
        string emailServerPort;
        string emailServerUserId;
        string emailServerUserPassword;
        public EmailService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }

        public void sendUserEmail(string fullName,string email)
        {
            string sql = "USP_GETCONFIG @ID=2";
            DataTable result = context.GetData(sql);
            foreach (DataRow row in result.Rows)
            {
                emailMsgBody = Convert.ToString(row["EMAILBODY"]);
                emailMsgSubject = Convert.ToString(row["EMAILSUBJECT"]);
                emailServerIp = Convert.ToString(row["EMAILSERVERIP"]);
                emailServerPort = Convert.ToString(row["EMAILSERVERPORT"]);
                emailServerUserId = Convert.ToString(row["EMAILSERVERUSERID"]);
                emailServerUserPassword = Convert.ToString(row["EMAILSERVERPASSWORD"]);
            }
            MailMessage objMailMessage = new MailMessage();     
            SmtpClient objSmtpClient = new SmtpClient(emailServerIp);
            emailMsgBody = emailMsgBody.Replace("@@FName@@", fullName);
            emailMsgBody = emailMsgBody.Replace("@@website@@", "ChartDoc URL");
            emailMsgBody = emailMsgBody.Replace("@@Login@@", fullName);
            emailMsgBody = emailMsgBody.Replace("@@Password@@", Convert.ToString("1234"));
          
            objMailMessage.From = new MailAddress(emailServerUserId);
            objMailMessage.To.Add(email);
            objMailMessage.Subject = emailMsgSubject;
            objMailMessage.Body = emailMsgBody;

            objSmtpClient.Port = Convert.ToInt32(emailServerPort);
            objSmtpClient.Credentials = new System.Net.NetworkCredential(emailServerUserId, emailServerUserPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Send(objMailMessage);
        }
        public void sendEmail(clsAppointment appointment)
        {
            string sql = "USP_GETCONFIG @ID=1";
            DataTable result = context.GetData(sql);
            foreach (DataRow row in result.Rows)
            {
                emailMsgBody = Convert.ToString(row["EMAILBODY"]);
                emailMsgSubject = Convert.ToString(row["EMAILSUBJECT"]);
                emailServerIp = Convert.ToString(row["EMAILSERVERIP"]);
                emailServerPort = Convert.ToString(row["EMAILSERVERPORT"]);
                emailServerUserId = Convert.ToString(row["EMAILSERVERUSERID"]);
                emailServerUserPassword = Convert.ToString(row["EMAILSERVERPASSWORD"]);
            }
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(emailServerIp);
            emailMsgSubject = emailMsgSubject.Replace("@@Date@@", appointment.date);
            emailMsgBody = emailMsgBody.Replace("@@date@@", appointment.date);
            emailMsgBody = emailMsgBody.Replace("@@FromTime@@", Convert.ToString(appointment.fromTime));
            emailMsgBody = emailMsgBody.Replace("@@ToTime@@", Convert.ToString(appointment.toTime));
            emailMsgBody = emailMsgBody.Replace("@@AppointmentNo@@", appointment.appointmentNo);
            objMailMessage.From = new MailAddress(emailServerUserId);
            objMailMessage.To.Add(appointment.email);
            objMailMessage.Subject = emailMsgSubject;
            objMailMessage.Body = emailMsgBody;

            objSmtpClient.Port = Convert.ToInt32(emailServerPort);
            objSmtpClient.Credentials = new System.Net.NetworkCredential(emailServerUserId, emailServerUserPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Send(objMailMessage);
        }

        public void SendResetPasswordLinkEmail(string email, string resetLink, string userId)
        {
            string sql = "USP_GETCONFIG @ID=3";
            DataTable result = context.GetData(sql);
            foreach (DataRow row in result.Rows)
            {
                emailMsgBody = Convert.ToString(row["EMAILBODY"]);
                emailMsgSubject = Convert.ToString(row["EMAILSUBJECT"]);
                emailServerIp = Convert.ToString(row["EMAILSERVERIP"]);
                emailServerPort = Convert.ToString(row["EMAILSERVERPORT"]);
                emailServerUserId = Convert.ToString(row["EMAILSERVERUSERID"]);
                emailServerUserPassword = Convert.ToString(row["EMAILSERVERPASSWORD"]);
            }
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(emailServerIp);
            emailMsgBody = emailMsgBody.Replace("@@EMAIL@@", email);
            emailMsgBody = emailMsgBody.Replace("@@RESETLINK@@", resetLink);
            emailMsgBody = emailMsgBody.Replace("@@USERID@@", userId);

            objMailMessage.From = new MailAddress(emailServerUserId);
            objMailMessage.To.Add(email);
            objMailMessage.Subject = emailMsgSubject;
            objMailMessage.Body = emailMsgBody;

            objSmtpClient.Port = Convert.ToInt32(emailServerPort);
            objSmtpClient.Credentials = new System.Net.NetworkCredential(emailServerUserId, emailServerUserPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Send(objMailMessage);
        }

        public void SendCreatePasswordEmail(string email, string userName, string userId, string passwordLink)
        {
            string sql = "USP_GETCONFIG @ID=4";
            DataTable result = context.GetData(sql);
            foreach (DataRow row in result.Rows)
            {
                emailMsgBody = Convert.ToString(row["EMAILBODY"]);
                emailMsgSubject = Convert.ToString(row["EMAILSUBJECT"]);
                emailServerIp = Convert.ToString(row["EMAILSERVERIP"]);
                emailServerPort = Convert.ToString(row["EMAILSERVERPORT"]);
                emailServerUserId = Convert.ToString(row["EMAILSERVERUSERID"]);
                emailServerUserPassword = Convert.ToString(row["EMAILSERVERPASSWORD"]);
            }
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(emailServerIp);
            emailMsgBody = emailMsgBody.Replace("@@UserName@@", userName);
            emailMsgBody = emailMsgBody.Replace("@@CreatePassword@@", passwordLink);
            emailMsgBody = emailMsgBody.Replace("@@UserId@@", userId);

            objMailMessage.From = new MailAddress(emailServerUserId);
            objMailMessage.To.Add(email);
            objMailMessage.Subject = emailMsgSubject;
            objMailMessage.Body = emailMsgBody;

            objSmtpClient.Port = Convert.ToInt32(emailServerPort);
            objSmtpClient.Credentials = new System.Net.NetworkCredential(emailServerUserId, emailServerUserPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Send(objMailMessage);
        }
    }
}
