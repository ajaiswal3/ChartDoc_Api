using ChartDoc.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ChartDoc.Models;
using System.Xml;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// public class SharedService : ISharedService
    /// </summary>
    public class SharedService : ISharedService
    {
        #region ConvertDatatableToXML**************************************************************************************************************************
        /// <summary>
        /// ConvertDatatableToXML
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>string</returns>
        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.TableName = "XMLData";
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }
        #endregion

        #region DecodeString***********************************************************************************************************************************
        /// <summary>
        /// DecodeString
        /// </summary>
        /// <param name="encodedString">string</param>
        /// <returns>string</returns>
        public string DecodeString(string encodedString)
        {
            string base64Decoded;
            byte[] data = System.Convert.FromBase64String(encodedString);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
        #endregion

        #region ObjArrayToDataTable****************************************************************************************************************************
        /// <summary>
        /// ObjArrayToDataTable
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="objects">T[]</param>
        /// <returns></returns>
        public DataTable ObjArrayToDataTable<T>(T[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                Type t = objects[0].GetType();
                DataTable dt = new DataTable(t.Name);

                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }
                foreach (var o in objects)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return null;
        }
        #endregion

        #region SingleObjToDataTable***************************************************************************************************************************
        /// <summary>
        /// SingleObjToDataTable
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="objects">T</param>
        /// <returns>DataTable</returns>
        public DataTable SingleObjToDataTable<T>(T objects)
        {
            Type t = objects.GetType();
            DataTable dt = new DataTable(t.Name);

            foreach (PropertyInfo pi in t.GetProperties())
            {
                dt.Columns.Add(new DataColumn(pi.Name));
            }
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = objects.GetType().GetProperty(dc.ColumnName).GetValue(objects, null);
            }
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region ObjToDataTableDocuments************************************************************************************************************************
        /// <summary>
        /// ObjToDataTableDocuments
        /// </summary>
        /// <param name="objects">List<clsDocument></param>
        /// <returns>DataSet</returns>
        public DataSet ObjToDataTableDocuments(List<clsDocument> objects)
        {
            DataSet ds = new DataSet();
            int i = 0;
            foreach (var item in objects)
            {
                i++;
                Type t = item.GetType();
                DataTable dt = new DataTable("table" + i);
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }
                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    dr[dc.ColumnName] = item.GetType().GetProperty(dc.ColumnName).GetValue(item, null);
                }
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }

            return ds;
        }
        #endregion

        #region ObjListToDataTable*****************************************************************************************************************************
        /// <summary>
        /// ObjListToDataTable
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="objects">DataSet</param>
        /// <returns>DataSet</returns>
        public DataSet ObjListToDataTable<T>(List<T> objects)
        {
            DataSet ds = new DataSet();
            int i = 0;
            foreach (var item in objects)
            {
                i++;
                Type t = item.GetType();
                DataTable dt = new DataTable("table" + i);
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }
                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    dr[dc.ColumnName] = item.GetType().GetProperty(dc.ColumnName).GetValue(item, null);
                }
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }

            return ds;
        }
        #endregion

        #region ObjToDataTableProcedure************************************************************************************************************************
        /// <summary>
        /// ObjToDataTableProcedure
        /// </summary>
        /// <param name="objects">clsProcedures</param>
        /// <returns>DataTable</returns>
        public DataTable ObjToDataTableProcedure(clsProcedures objects)
        {
            Type t = objects.GetType();
            DataTable dt = new DataTable(t.Name);
            foreach (PropertyInfo pi in t.GetProperties())
            {
                dt.Columns.Add(new DataColumn(pi.Name));
            }
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = objects.GetType().GetProperty(dc.ColumnName).GetValue(objects, null);
            }
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region ConvertDatatableToXMLNew***********************************************************************************************************************
        /// <summary>
        /// ConvertDatatableToXMLNew
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>string</returns>
        public string ConvertDatatableToXMLNew(DataTable dt)
        {
            StringBuilder formattedXML = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateNode(XmlNodeType.Element, string.Empty, "DocumentElement", null);
            DataColumnCollection dtColumns = dt.Columns;

            foreach (DataRow dataItem in dt.Rows)
            {
                XmlElement element = doc.CreateElement("XMLData");
                foreach (DataColumn thisColumn in dtColumns)
                {
                    object value = dataItem[thisColumn];
                    XmlElement tmp = doc.CreateElement(thisColumn.ColumnName);
                    if (value != null)
                    {
                        tmp.InnerXml = value.ToString();
                    }
                    else
                    {
                        tmp.InnerXml = string.Empty;
                    }
                    element.AppendChild(tmp);
                }
                node.AppendChild(element);
            }
            doc.AppendChild(node);
            return doc.InnerXml;
        }
        #endregion

    }
}
