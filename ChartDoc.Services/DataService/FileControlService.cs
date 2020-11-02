using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
            string imageName = string.Empty;
            //var uploads = Path.Combine(context._configaration["FolderPath"], @"Images\Patient\Profile\");
            var uploads = Path.Combine(_env.ContentRootPath, @"Images\Patient\Profile\");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (uploadFile != null && uploadFile.Length > 0)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName)).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(uploadFile.FileName);
                var filePath = Path.Combine(uploads, imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await uploadFile.CopyToAsync(stream);
                        filePath = filePath.Replace(@"\", @"/");
                        filePath = filePath.Replace(filePath.Substring(0, filePath.IndexOf("Images")), context._configaration["SavedImageUrl"]);
                        data.imagePath = filePath;
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

        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
               // MessageBox.Show("Encryption failed!", "Error");
            }
        }

        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }
    }
}
