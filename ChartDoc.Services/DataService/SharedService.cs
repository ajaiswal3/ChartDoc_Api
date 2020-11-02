using ChartDoc.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ChartDoc.Models;
using System.Xml;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using ChartDoc.DAL;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// public class SharedService : ISharedService
    /// </summary>
    public class SharedService : ISharedService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
       
        #endregion

        #region SharedService Constructor*******************************************************************************************************************************
        /// <summary>
        /// FlowSheetService : Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public SharedService(IConfiguration configuration)
        {
            db._configaration = configuration;
           
        }
        #endregion

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

        private  string DecryptKEY(string encryptString)
        {
           
            string decryptData = string.Empty;
            UTF8Encoding encodeData = new UTF8Encoding();

            if (!string.IsNullOrEmpty(encryptString))
            {
                try
                {
                    Decoder Decode = encodeData.GetDecoder();
                    byte[] toDecodeByte = Convert.FromBase64String(encryptString);
                    int charCount = Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                    char[] decodedChar = new char[charCount];
                    Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                    decryptData = new String(decodedChar);
                    return decryptData;
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }
            else return "";
        }
        // To encrypt - we are using this method
        public  string Encrypt(string encryptString)
        {
            if (!string.IsNullOrEmpty(encryptString))
            {
                string key = GetEncryptKEY();
                string EncryptionKey = DecryptKEY(key);

                byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptString = Convert.ToBase64String(ms.ToArray());
                    }
                }

            }
            return encryptString;
        }
        public  string Decrypt(string cipherText)
        {
            if (!string.IsNullOrEmpty(cipherText))
            {
                string key = GetEncryptKEY();
                string EncryptionKey = DecryptKEY(key);

                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return cipherText;
        }
        public  string EncryptKEY(string data)
        {
            

            if (!string.IsNullOrEmpty(data))
            {
                string encryptedData = string.Empty;
                try
                {
                    byte[] encode = new byte[data.Length];
                    encode = Encoding.UTF8.GetBytes(data);
                    encryptedData = Convert.ToBase64String(encode);
                    return encryptedData;
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }
            else return "";
        }
        private string GetEncryptKEY()
        {
            DataTable dt = new DataTable();
            string sql = "select EncryptKEY from tbl_Param";
            dt = db.GetData(sql);
            return Convert.ToString(dt.Rows[0][0]);
            // return "OTkwMzA3NjMyNQ==";
        }
    }
}
