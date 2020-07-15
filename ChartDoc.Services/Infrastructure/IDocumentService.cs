using ChartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Services.Infrastructure
{
    public interface IDocumentService
    {
        List<clsDocument> GetDocument(string PatientId, string Flag, int id);
    }
}
