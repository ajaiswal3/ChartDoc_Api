using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChartDoc.Services.DataService
{
   public class SmsService : ISmsService
    {
        DBUtils context = DBUtils.GetInstance;
        string smsMsgBody;
        string smsMsgSubject;
        string smsApiKey;
        string smsApiSecret;
        string smsFrom;
        public SmsService(IConfiguration configaration)
        {
            context._configaration = configaration;
        }
        public void SendMessage(clsAppointment appointment)
        {

            string sql = "USP_GETCONFIG";
            DataTable result = context.GetData(sql);

            foreach (DataRow row in result.Rows)
            {
                smsMsgBody = Convert.ToString(row["SMSMESSAGE"]);
                smsMsgSubject = Convert.ToString(row["EMAILSUBJECT"]);
                smsApiKey = Convert.ToString(row["SMSSERVERKEY"]);
                smsApiSecret = Convert.ToString(row["SMSSERVERUSERID"]);
                smsFrom = Convert.ToString(row["SMSFROM"]);
              
            }
            smsMsgSubject = smsMsgSubject.Replace("@@Date@@", appointment.date);
            smsMsgBody = smsMsgBody.Replace("@@date@@", appointment.date);
            smsMsgBody = smsMsgBody.Replace("@@FromTime@@", Convert.ToString(appointment.fromTime));
            smsMsgBody = smsMsgBody.Replace("@@ToTime@@", Convert.ToString(appointment.toTime));
            smsMsgBody = smsMsgBody.Replace("@@AppointmentNo@@", appointment.appointmentNo);
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey =smsApiKey,
                ApiSecret = smsApiSecret
            });
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = smsFrom,
                to = appointment.contactNo,
                text = smsMsgBody
            });
        }
    }
}
