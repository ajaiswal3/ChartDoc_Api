using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class SendEmailCreatePasswordParams
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string PasswordLink { get; set; }
    }
}
