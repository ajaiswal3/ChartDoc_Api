using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class ActiveDirectoryParams
    {
        public string DomainName { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string Password { get; set; }
    }
}
