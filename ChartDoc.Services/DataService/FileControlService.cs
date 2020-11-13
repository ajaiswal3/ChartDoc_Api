using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// FileControlService : Public Class
    /// </summary>
    public class FileControlService : IFileControlService
    {
        #region Instance Variable******************************************************************************************************************************
        private readonly ILogService logService;
        private readonly IHostingEnvironment _env;
        DBUtils context = DBUtils.GetInstance;
        #endregion

        #region FileControlService Constructor*****************************************************************************************************************
        /// <summary>
        /// FileControlService : Parameterized Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        /// <param name="logService">ILogService</param>
        public FileControlService(IConfiguration configaration, ILogService logService, IHostingEnvironment env)
        {
            context._configaration = configaration;
            _env = env;
            this.logService = logService;
        }
        #endregion

        #region Save Social Image******************************************************************************************************************************
        /// <summary>
        /// SaveSocialImage : Public Method To Save Social Image
        /// </summary>
        /// <param name="data">clsPatientSocial</param>
        /// <param name="uploadFile">IFormFile</param>
        /// <returns>clsPatientSocial</returns>
        public async Task<clsPatientSocial> SaveSocialImage(clsPatientSocial data, IFormFile uploadFile)
        {
            string imageName = string.Empty;
            //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Patient");
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (uploadFile != null &&  uploadFile.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
                var filePath = Path.Combine(uploads, "Social/" + imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                      await  uploadFile.CopyToAsync(stream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        data.driversLicenseFilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return data;
        }
        #endregion

        #region Save Billing Image*****************************************************************************************************************************
        /// <summary>
        /// SaveBillingImage : Public Method To Save Billing Image
        /// </summary>
        /// <param name="data">clsPatientBilling</param>
        /// <param name="file">IFormFile</param>
        /// <returns>clsPatientBilling</returns>
        public async Task<clsPatientBilling>   SaveBillingImage(clsPatientBilling data, IFormFile file)
        {
            string imageName = string.Empty;
            //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Patient");
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file != null && file.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(file.FileName)).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, "Billing/" + imageName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                       await file.CopyToAsync(stream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        data.driversLicenseFilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return data;
        }
        #endregion

        #region Save Authorization Image***********************************************************************************************************************
        /// <summary>
        /// SaveAuthorizationImage : Public Method To Save Authorization Image
        /// </summary>
        /// <param name="data">clsPatientAuthorization</param>
        /// <param name="uploadFile">IFormFile</param>
        /// <returns>clsPatientAuthorization</returns>
        public async Task<clsPatientAuthorization>   SaveAuthorizationImage(clsPatientAuthorization data, IFormFile uploadFile)
        {
            string imageName = string.Empty;
            //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Patient");
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (uploadFile != null && uploadFile.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
                var filePath = Path.Combine(uploads, "Authorization/" + imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                       await uploadFile.CopyToAsync(stream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        data.authorizationFilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return data;
        }
        #endregion

        #region Save Insurance Image***************************************************************************************************************************
        /// <summary>
        /// SaveInsuranceImage : Public Method To Save Insurance Image
        /// </summary>
        /// <param name="data">clsPatientInsurance</param>
        /// <param name="uploadFile">IFormFile</param>
        /// <returns>clsPatientInsurance</returns>
        public async Task<clsPatientInsurance>   SaveInsuranceImage(clsPatientInsurance data, IFormFile uploadFile)
        {
            string imageName = string.Empty;
            //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Patient");
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (uploadFile != null && uploadFile.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
                var filePath = Path.Combine(uploads, "Insurance/" + imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                      await  uploadFile.CopyToAsync(stream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        data.cardImageFilePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return data;
        }
        #endregion

        #region Save User Image********************************************************************************************************************************
        /// <summary>
        /// SaveUserImage : Public Method To Save User Image
        /// </summary>
        /// <param name="iUser">clsUserObj</param>
        /// <param name="file">IFormFileCollection</param>
        /// <returns>clsUserObj</returns>
        public async Task<clsUserObj>  SaveUserImage(clsUserObj iUser, IFormFileCollection file)
        {
            if (file.Count > 0)
            {
                //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\User");
                var uploads = Path.Combine(_env.ContentRootPath, @"Images\User");
                Guid fileName = Guid.NewGuid();
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                string filePath = Path.Combine(uploads, fileName.ToString() + "-" + iUser.fullName + Path.GetExtension(file[0].FileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file[0].CopyToAsync(fileStream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        iUser.imagePath = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return iUser;
        }
        #endregion

        #region Save Appointment Image*************************************************************************************************************************
        /// <summary>
        /// SaveAppointmentImage : Public Method To Save Appointment Image
        /// </summary>
        /// <param name="appointment">clsAppointment</param>
        /// <param name="file">IFormFileCollection</param>
        /// <returns>clsAppointment</returns>
        public async Task<clsAppointment>  SaveAppointmentImage(clsAppointment appointment, IFormFileCollection file)
        {
            if (file.Count > 0)
            {
                //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Appointment");
                var uploads = Path.Combine(_env.ContentRootPath, @"Images\Appointment");
                Guid fileName = Guid.NewGuid();
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                string filePath = Path.Combine(uploads, fileName.ToString() + "-" + appointment.patientName + Path.GetExtension(file[0].FileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file[0].CopyToAsync(fileStream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        appointment.imageUrl = filePath;
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
            }
            return appointment;
        }
        #endregion

        #region Save Procedure Image***************************************************************************************************************************
        /// <summary>
        /// SaveProcedureImage : Public Method To Save Procedure Image
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="uploadFiles">IFormFileCollection</param>
        /// <returns>List<clsDocument></returns>
        public List<clsDocument> SaveProcedureImage(string patientId, IFormFileCollection uploadFiles)
        {
            List<clsDocument> documentList = null;
            if (uploadFiles.Count > 0)
            {
                documentList = new List<clsDocument>();
                foreach (IFormFile file in uploadFiles)
                {
                    if (file.Length > 0)
                    {
                        //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Procedures");
                        var uploads = Path.Combine(_env.ContentRootPath, @"Images\Procedures");
                        Guid FileName = Guid.NewGuid();
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        string filePath = Path.Combine(uploads, FileName.ToString() + "-" + file.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            try
                            {
                                file.CopyToAsync(fileStream);
                                filePath = filePath.Replace(@"\", @"/");
                                filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);

                                documentList.Add(new clsDocument
                                {
                                    documentType = "P",
                                    fileName = file.FileName,
                                    path = filePath,
                                    patientId = patientId
                                });
                            }
                            catch (Exception ex)
                            {
                                var logger = logService.GetLogger(this.GetType());
                                logger.Error(ex);
                            }
                        }
                    }
                }
            }
            return documentList;
        }
        #endregion

        #region Save Diagnosis Image***************************************************************************************************************************
        /// <summary>
        /// SaveDiagnosisImage : Public Method To Save Diagnosis Image
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="uploadFiles">IFormFileCollection</param>
        /// <returns>List<clsDocument></returns>
        public List<clsDocument> SaveDiagnosisImage(string patientId, IFormFileCollection uploadFiles)
        {
            List<clsDocument> documentList = null;
            if (uploadFiles.Count > 0)
            {
                documentList = new List<clsDocument>();
                foreach (IFormFile file in uploadFiles)
                {
                    if (file.Length > 0)
                    {
                        //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Diagnosis");
                        var uploads = Path.Combine(_env.ContentRootPath, @"Images\Diagnosis");
                        Guid fileName = Guid.NewGuid();
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        string filePath = Path.Combine(uploads, fileName.ToString() + "-" + file.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            try
                            {
                                file.CopyToAsync(fileStream);
                                filePath = filePath.Replace(@"\", @"/");
                                filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);

                                documentList.Add(new clsDocument
                                {
                                    documentType = "D",
                                    fileName = file.FileName,
                                    path = filePath,
                                    patientId = patientId
                                });
                            }
                            catch (Exception ex)
                            {
                                var logger = logService.GetLogger(this.GetType());
                                logger.Error(ex);
                            }
                        }
                    }
                }
            }
            return documentList;
        }
        #endregion

        #region Save Patient Profile Image*********************************************************************************************************************
        /// <summary>
        /// SavePatientProfileImage : Public Method To Save Patient Profile Image
        /// </summary>
        /// <param name="data">clsPatientDetails</param>
        /// <param name="uploadFile">IFormFile</param>
        /// <returns>clsPatientDetails</returns>
       public async Task<clsPatientDetails> SavePatientProfileImage(clsPatientDetails data, IFormFile uploadFile)
        
        {
            //string imageName = string.Empty;

            //var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient\Profile\");
            //if (!Directory.Exists(uploads))
            //{
            //    Directory.CreateDirectory(uploads);
            //}
            //if (uploadFile != null && uploadFile.Length > 0)
            //{
            //    imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
            //    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
            //    var filePath = Path.Combine(uploads, imageName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    try
            //    {
            //        await uploadFile.CopyToAsync(stream);
            //        filePath = filePath.Replace(@"\", @"/");
            //        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
            //        data.imagePath = filePath;
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = logService.GetLogger(this.GetType());
            //        logger.Error(ex);
            //    }
            //}
            //}
            //return data;
            string imageName = string.Empty;
            string imageNameOutput = string.Empty;
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient\Profile\");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (uploadFile != null && uploadFile.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
                imageNameOutput = imageName + DateTime.Now.ToString("yymmssfff")+"_enc" + Path.GetExtension(uploadFile.FileName);
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
                data.imageName = imageNameOutput;
                var filePath = Path.Combine(uploads, imageName);
                var filePathOutput = Path.Combine(uploads, imageNameOutput);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                       // uploadFile.CopyTo(stream);
                        await uploadFile.CopyToAsync(stream);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        var logger = logService.GetLogger(this.GetType());
                        logger.Error(ex);
                    }
                }
                this.EncryptFile(filePath, filePathOutput);
                File.Delete(filePath);
                filePathOutput = filePathOutput.Replace(@"\", @"/");
                filePathOutput = filePathOutput.Replace(filePathOutput.Substring(0, filePathOutput.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                data.imagePath = filePathOutput;
            }
            return data;
            
        }
        #endregion

        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void EncryptFile(string inputFilePath, string outputfilePath)
        {

            string EncryptionKey = GetEncryptKEY();
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        //private void DecryptFile(string inputFile, string outputFile)
        //{

        //    {
        //        string password = @"myKey123"; // Your Key Here

        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();

        //        CryptoStream cs = new CryptoStream(fsCrypt,
        //            RMCrypto.CreateDecryptor(key, key),
        //            CryptoStreamMode.Read);

        //        FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        //        int data;
        //        while ((data = cs.ReadByte()) != -1)
        //            fsOut.WriteByte((byte)data);

        //        fsOut.Close();
        //        cs.Close();
        //        fsCrypt.Close();

        //    }
        //}

        public string DecryptFile(string filemame)
        {
            //Get the Input File Name and Extension
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient\Profile");
            string fileName = filemame.Split('.')[0];
            string fileExtension = "." + filemame.Split('.')[1];

            //Build the File Path for the original (input) and the decrypted (output) file
            string input = Path.Combine(uploads, fileName+ fileExtension);
            string output = Path.Combine(uploads, fileName+"_dec"+fileExtension);


            this.Decrypt(input, output);

            //Download the Decrypted File.
            //Response.Clear();
            //Response.ContentType = FileUpload1.PostedFile.ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
            //Response.WriteFile(output);
            //Response.Flush();

            //Delete the original (input) and the decrypted (output) file.
            byte[] b = System.IO.File.ReadAllBytes(output);
            var bytetdata= "data:image/png;base64," + Convert.ToBase64String(b);
            File.Delete(output);

            // Response.End();
          return bytetdata;
        }
        private string GetEncryptKEY()
        {
            DataTable dt = new DataTable();
            string sql = "select EncryptKEY from tbl_Param";
            dt = context.GetData(sql);
            return Convert.ToString(dt.Rows[0][0]);
            // return "OTkwMzA3NjMyNQ==";
        }
        private void Decrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = GetEncryptKEY();
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

    }
}
