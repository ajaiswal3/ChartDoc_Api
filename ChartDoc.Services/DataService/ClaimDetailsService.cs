using ChartDoc.DAL;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace ChartDoc.Services.DataService
{
    public class ClaimDetailsService : IClaimDetailsService
    {
        #region Instance variables
        DBUtils db = DBUtils.GetInstance;
        private readonly ILogService logService;
        ISharedService _sharedService;
        #endregion

        #region Constructor
        /// <summary>
        /// ClaimDetailsService : Constructor
        /// </summary>
        /// <param name="configaration"></param>
        /// <param name="logService"></param>
        public ClaimDetailsService(IConfiguration configaration, ILogService logService, ISharedService sharedService)
        {
            db._configaration = configaration;
            this.logService = logService;
            this._sharedService = sharedService;
        }
        #endregion

        public bool GetClaimDetails()
        {
            var res = ClaimDetails();
            return res;
        }

        private bool ClaimDetails()
        {
            DataSet results = new DataSet();
            bool res = false;
            string sqlString = "EXEC USP_GETCLAIMDETAILS '',''";

            try
            {
                results = db.GetDataInDataSet(sqlString);
                res = CreateXmlFile(results);
            }
            catch (SqlException sqlEx)
            {
                var logger = logService.GetLogger(typeof(ClaimDetailsService));
                logger.Error(sqlEx);
                res = false;
            }
            catch (NullReferenceException nullEx)
            {
                var logger = logService.GetLogger(typeof(ClaimDetailsService));
                logger.Error(nullEx);
                res = false;
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(ClaimDetailsService));
                logger.Error(ex);
                res = false;
            }
            return res;
        }

        private bool CreateXmlFile(DataSet ds)
        {
            bool res = false;
            string xmlFilePath = db._configaration["XMLPath"];
            if (!Directory.Exists(xmlFilePath))
            {
                Directory.CreateDirectory(xmlFilePath);
            }
            using (XmlWriter writer = XmlWriter.Create(Path.Combine(xmlFilePath, "ClaimDetails_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xml")))
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("claims");

                foreach (DataRow row1 in ds.Tables[0].Rows)
                {
                    writer.WriteStartElement("claim");
                    CreateNode(writer, ds.Tables[0], row1);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[1];

                    dt.Select("chargeid=" + row1["claimid"]);
                    foreach (DataRow row2 in dt.Rows)
                    {
                        writer.WriteStartElement("charge");
                        CreateNode(writer, dt, row2);
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                res = true;
            }
            return res;
        }

        private XmlWriter CreateNode(XmlWriter writer, DataTable dt, DataRow row)
        {
            List<string> valueToDecrypt = new List<string>();
            valueToDecrypt.Add("ins_addr_1");
            valueToDecrypt.Add("ins_addr_2");
            valueToDecrypt.Add("ins_city");
            valueToDecrypt.Add("ins_name_f");
            valueToDecrypt.Add("ins_name_l");
            valueToDecrypt.Add("ins_number");
            valueToDecrypt.Add("ins_name_m");
            valueToDecrypt.Add("ins_phone");
            valueToDecrypt.Add("ins_sex");
            valueToDecrypt.Add("ins_state");
            valueToDecrypt.Add("ins_zip");
            valueToDecrypt.Add("pat_addr_1");
            valueToDecrypt.Add("pat_addr_2");
            valueToDecrypt.Add("pat_city");
            valueToDecrypt.Add("pat_country");
            valueToDecrypt.Add("pat_name_f");
            valueToDecrypt.Add("pat_name_l");
            valueToDecrypt.Add("pat_name_m");
            valueToDecrypt.Add("pat_rel");
            valueToDecrypt.Add("pat_sex");
            valueToDecrypt.Add("pat_state");
            valueToDecrypt.Add("pat_ssn");
            valueToDecrypt.Add("pat_zip");
            valueToDecrypt.Add("pat_phone");
            foreach (DataColumn column in dt.Columns)
            {
                if (valueToDecrypt.Contains(column.ColumnName))
                {
                    writer.WriteAttributeString(column.ColumnName, _sharedService.Decrypt(row[column].ToString()));
                }
                else
                {
                    writer.WriteAttributeString(column.ColumnName, row[column].ToString());
                }
                //writer.WriteString(column.ColumnName);
                //writer.WriteValue(row[column].ToString());
            }
            return writer;


        }
    }
}
