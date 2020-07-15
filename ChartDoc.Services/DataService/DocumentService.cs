using ChartDoc.Context;
using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// DocumentService : Public Class
    /// </summary>
    public class DocumentService : IDocumentService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region DocumentService Constructor********************************************************************************************************************
        /// <summary>
        /// DocumentService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public DocumentService(IConfiguration configaration)
        {
            db._configaration = configaration;
        }
        #endregion

        #region GetDocument************************************************************************************************************************************
        /// <summary>
        /// GetDocument : To get the document based on PatientId, Flag and id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="flag">string</param>
        /// <param name="id">int</param>
        /// <returns>List<clsDocument></returns>
        public List<clsDocument> GetDocument(string patientId, string flag, int id)
        {
            List<clsDocument> lstDetails = new List<clsDocument>();
            DataTable dtDocument = GetDocumentDetails(patientId, flag, id);
            for (int index = 0; index <= dtDocument.Rows.Count - 1; index++)
            {
                clsDocument objDetails = new clsDocument();
                objDetails.path = Convert.ToString(dtDocument.Rows[index]["Path"]);
                objDetails.fileName = Convert.ToString(dtDocument.Rows[index]["FileName"]);
                objDetails.patientId = Convert.ToString(dtDocument.Rows[index]["PatientId"]);
                objDetails.documentType = Convert.ToString(dtDocument.Rows[index]["DocumentType"]);
                objDetails.procDiagId = Convert.ToInt32(dtDocument.Rows[index]["Proc_Diag_Id"]);
                lstDetails.Add(objDetails);
            }
            return lstDetails;
        }
        #endregion

        #region GetDocumentDetails******************************************************************************************************************************
        /// <summary>
        /// GetDocumentDetails : Private Method
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="flag">string</param>
        /// <param name="id">int</param>
        /// <returns>DataTable</returns>
        private DataTable GetDocumentDetails(string patientId, string flag, int id)
        {
            DataTable dtDocumentDetails = new DataTable();
            string sqlDocumentDetails = string.Empty;
            if (string.Compare(flag, "D") == 0)
            {
                sqlDocumentDetails = "SELECT doc.PatientId, doc.Path, doc.FileName, doc.DocumentType, doc.Proc_Diag_ID FROM T_Diagnosis diag INNER JOIN T_DOCUMENT doc ON diag.Id=doc.Proc_Diag_ID " +
                        " WHERE doc.Patientid='" + patientId + "' AND doc.DocumentType='" + flag + "' AND diag.Id=" + id;
            }
            else
            {
                sqlDocumentDetails = "SELECT doc.PatientId, doc.Path, doc.FileName, doc.DocumentType, doc.Proc_Diag_ID FROM T_Procedure p INNER JOIN T_DOCUMENT doc ON p.Id=doc.Proc_Diag_ID " +
                        " WHERE doc.Patientid='" + patientId + "' AND doc.DocumentType='" + flag + "' AND p.Id=" + id;
            }
            dtDocumentDetails = db.GetData(sqlDocumentDetails);
            return dtDocumentDetails;
        }
        #endregion
    }
}
