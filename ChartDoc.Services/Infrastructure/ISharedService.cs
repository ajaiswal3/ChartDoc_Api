using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ChartDoc.Models;

namespace ChartDoc.Services.Infrastructure
{
    public interface ISharedService
    {
        string DecodeString(string encodedString);
        DataTable SingleObjToDataTable<T>(T objects);
        DataTable ObjArrayToDataTable<T>(T[] objects);
        string ConvertDatatableToXML(DataTable dt);
        DataSet ObjToDataTableDocuments(List<clsDocument> objects);
        DataSet ObjListToDataTable<T>(List<T> objects);
        DataTable ObjToDataTableProcedure(clsProcedures objects);
        string ConvertDatatableToXMLNew(DataTable dt);
    }
}
