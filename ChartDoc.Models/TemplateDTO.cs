using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Models
{
    public class TemplateDTO
    {
        public List<TemplateData> data { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }
    public class TemplateData
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
