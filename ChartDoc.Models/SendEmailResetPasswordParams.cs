using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class SendEmailResetPasswordParams
    {
        public string Email { get; set; }
        public string LandingPageLink { get; set; }
        public string UserId { get; set; }
    }
}
