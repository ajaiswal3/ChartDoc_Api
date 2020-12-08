using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class ResetPasswordDTO
    {
        public ReturnData data { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }

    public class ReturnData
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
    }
}
