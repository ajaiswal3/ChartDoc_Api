using ChartDoc.Context;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChartDoc.DAL;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using ChartDoc.Radius;
using System.DirectoryServices;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// public class UserService : IUserService
    /// </summary>
    public class UserService : IUserService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        private readonly ILogService logService;
        private readonly IEmailService emailService;

        private string _duoHostName = "127.0.0.1";
        private string _duoSharedSecret = "secret123";
        #endregion

        #region UserService Constructor************************************************************************************************************************
        /// <summary>
        /// UserService : Constructor
        /// </summary>
        /// <param name="configaration">IConfiguration</param>
        public UserService( IConfiguration configaration, ILogService logService,  IEmailService emailService)
        {
            db._configaration = configaration;
            this.logService = logService;
            
            this.emailService = emailService;
        }
        #endregion

        #region GetAllDoctorsDetails***************************************************************************************************************************
        /// <summary>
        /// GetAllDoctorsDetails
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>List<clsDoctor></returns>
        public List<clsDoctor> GetAllDoctorsDetails(string date)
        {
            DataTable dtDoctors = new DataTable();
            List<clsDoctor> lstDetails = new List<clsDoctor>();
            string sqlDoctors = "USP_FETCH_DOCTORLIST '" + date + "'";
            dtDoctors = db.GetData(sqlDoctors);
            for (int index = 0; index <= dtDoctors.Rows.Count - 1; index++)
            {
                clsDoctor objDoctors = new clsDoctor();
                objDoctors.id = Convert.ToString(dtDoctors.Rows[index]["id"]);
                objDoctors.name = Convert.ToString(dtDoctors.Rows[index]["NAME"]);
                objDoctors.image = string.Empty;
                lstDetails.Add(objDoctors);
            }
            return lstDetails;
        }
        #endregion

        #region GetUser****************************************************************************************************************************************
        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="userName">string</param>
        /// <param name="password">string</param>
        /// <returns>clsUser</returns>
        public clsUser GetUser(string userName, string password)
        {
            clsUser objUser= new clsUser();
            DataTable dtUser = new DataTable();
            string sqlUser = "SELECT * FROM VW_USERLOGININFO WHERE USERNAME='" + userName + "' AND PASSWORD=dbo.fnMIME64Encode(1,'" + password + "')";
            dtUser = db.GetData(sqlUser);
            if (dtUser.Rows.Count != 0)
            {
                int result = 0;
                string sqlinsert = string.Empty;
                DataTable dt1 = new DataTable();
                string sqluseridcheack = string.Empty;
                string sqluserid = "SELECT USERID FROM M_USER WHERE USERNAME='" + userName + "'";

                int userid = 0;
                userid = (int)db.GetSingleValue(sqluserid);

                string sqlmaxdate = string.Empty;
                string maxdate = string.Empty;

                string sqltodate = "SELECT CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103) AS TODATE ";
                string todate = (string)db.GetSingleValue(sqltodate);

                sqlmaxdate = "SELECT MAX(CONVERT(VARCHAR(10),CAST(LASTLOGOUTTIME AS DATE),103)) AS LASTLOGOUTTIME FROM  T_USER_LOG";

                DataTable dt2 = db.GetData(sqlmaxdate);
                maxdate = dt2.Rows[0][0].ToString();
                if (maxdate == "")
                {
                    maxdate = todate;
                }
                else
                {
                    maxdate = (string)db.GetSingleValue(sqlmaxdate);
                }

                sqluseridcheack = "SELECT USERID FROM T_USER_LOG WHERE USERID='" + userid + "'";
                dt1 = db.GetData(sqluseridcheack);

                if (dt1.Rows.Count == 0)
                {
                    sqlinsert = "INSERT INTO T_USER_LOG (USERID,LASTLOGINTIME) VALUES('" + userid + "',GETDATE())";
                    result = db.HandleData(sqlinsert);
                }
                else
                {
                    if (maxdate == todate)
                    {

                        sqlinsert = "UPDATE T_USER_LOG  set USERID='" + userid + "',LASTLOGINTIME=GETDATE() where USERID='" + userid + "' ";
                        result = db.HandleData(sqlinsert);
                    }
                    else
                    {
                        sqlinsert = "INSERT INTO T_USER_LOG (USERID,LASTLOGINTIME) VALUES('" + userid + "',GETDATE())";
                        result = db.HandleData(sqlinsert);
                    }
                }
            }

            if (dtUser.Rows.Count > 0)
            {
                objUser.userId = Convert.ToString(dtUser.Rows[0]["UserID"]);
                objUser.userType = Convert.ToString(dtUser.Rows[0]["USERTYPE"]);
                objUser.utName = Convert.ToString(dtUser.Rows[0]["UTNAME"]);
                objUser.fName = Convert.ToString(dtUser.Rows[0]["FULLDESCRIPTION"]);
                objUser.applicableTo = Convert.ToString(dtUser.Rows[0]["APPLICABLETO"]).Trim();
                objUser.iUserId = Convert.ToString(dtUser.Rows[0]["IUserID"]);
                objUser.userTag = Convert.ToString(dtUser.Rows[0]["USERTAG"]);
                objUser.code = Convert.ToString(dtUser.Rows[0]["Code"]);
                objUser.userName = userName;
                objUser.errorCode = "0";
                objUser.errorDesc = "Succesful";
                objUser.department = Convert.ToString(dtUser.Rows[0]["DEPARTMENTNAME"]);
                objUser.departmentId = Convert.ToString(dtUser.Rows[0]["DEPARTMENTID"]);
            }
            else
            {
                objUser.userId = string.Empty;
                objUser.userType = string.Empty;
                objUser.utName = string.Empty;
                objUser.fName = string.Empty;
                objUser.applicableTo = string.Empty;
                objUser.iUserId = string.Empty;
                objUser.userTag = string.Empty;
                objUser.code = string.Empty;
                objUser.userName = string.Empty;
                objUser.errorCode = "-2";
                objUser.errorDesc = "Invalid User/Password";
                objUser.department = string.Empty;
                objUser.departmentId = string.Empty;
            }
            return objUser;
        }
        #endregion

        public async Task<string> Authenticate(string userName, string password)
        {
            string returnStatus = string.Empty;
            string statusValue = string.Empty;

            userName = @"ChartDoc\" + userName;

            RadiusClient rc = new RadiusClient(_duoHostName, _duoSharedSecret);
            RadiusPacket authPacket = rc.Authenticate(userName, password);
            authPacket.SetAttribute(new VendorSpecificAttribute(10135, 1, UTF8Encoding.UTF8.GetBytes("Testing")));
            authPacket.SetAttribute(new VendorSpecificAttribute(10135, 2, new[] { (byte)7 }));

            for (int i = 0; i < 6; i++)
            {
                RadiusPacket receivedPacket = await rc.SendAndReceivePacket(authPacket);
                if (receivedPacket == null)
                {
                    if(i<6)
                    {
                        continue;
                    }
                    //throw new Exception("Can't contact remote radius server !");
                    returnStatus = "Can not contact remote radius server !";
                    return returnStatus;
                }

                switch (receivedPacket.PacketType)
                {
                    case RadiusCode.ACCESS_ACCEPT:
                        //Console.WriteLine("Access-Accept");
                        returnStatus = "Access - Accept#";
                        foreach (var attr in receivedPacket.Attributes)
                        {
                            statusValue += attr.Type.ToString() + " = " + attr.Value;
                            //Console.WriteLine(attr.Type.ToString() + " = " + attr.Value);
                        }
                        returnStatus = returnStatus + statusValue;
                        break;
                    case RadiusCode.ACCESS_CHALLENGE:
                        //Console.WriteLine("Access-Challenge");
                        returnStatus = "Access-Challenge";
                        break;
                    case RadiusCode.ACCESS_REJECT:
                        //Console.WriteLine("Access-Reject");
                        returnStatus = "Access-Reject";
                        if (!rc.VerifyAuthenticator(authPacket, receivedPacket))
                        {
                            //Console.WriteLine("Authenticator check failed: Check your secret");
                            returnStatus = returnStatus + "Authenticator check failed: Check your secret";
                        }
                        break;
                    default:
                        //Console.WriteLine("Rejected");
                        returnStatus = "Rejected";
                        break;
                }
                if(!string.IsNullOrEmpty(returnStatus))
                {
                    break;
                }
            }

            return returnStatus;
        }

        //Add User to Active Directory
        public string AddActiveDirectoryUser(string domainName, string userName, string userFullName, string password)
        {
            string status = string.Empty;
            domainName = "ChartDoc.Internal";
            userName = "CHARTDOC";
            password = "ChartD0cWaters";
            try
            {
                //DirectoryEntry child = new DirectoryEntry("LDAP://" + domainName + "/" + objectDn, userName, password);
                DirectoryEntry child = new DirectoryEntry("LDAP://" + domainName, userName, password);
                DirectoryEntry newUser = child.Children.Add("suvresh_new", "user");
                newUser.Invoke("SetPassword", new object[] { "3l!teP@$$w0RDz" });
                newUser.CommitChanges();
            }
            catch (Exception ex)
            {

            }

            return status;
        }

        #region GetUserList************************************************************************************************************************************
        /// <summary>
        /// GetUserList
        /// </summary>
        /// <param name="userType">string</param>
        /// <returns>List<clsDoctorList></returns>
        public List<clsDoctorList> GetUserList(string userType)
        {
            List<clsDoctorList> lstDetails = new List<clsDoctorList>();
            DataTable dtDoctors = BindDoctorList(userType);
            for (int index = 0; index <= dtDoctors.Rows.Count - 1; index++)
            {
                clsDoctorList objDoctor = new clsDoctorList();
                objDoctor.id = Convert.ToString(dtDoctors.Rows[index]["id"]);
                objDoctor.doctorName = Convert.ToString(dtDoctors.Rows[index]["NAME"]);
                objDoctor.doctorImage = Convert.ToString(dtDoctors.Rows[index]["ImagePath"]);
                objDoctor.dateOfBirth = Convert.ToString(dtDoctors.Rows[index]["DOB"]);
                objDoctor.email = Convert.ToString(dtDoctors.Rows[index]["EMAIL"]);
                objDoctor.phone = Convert.ToString(dtDoctors.Rows[index]["MOBILE"]);
                objDoctor.roleSubType = Convert.ToString(dtDoctors.Rows[index]["REPORTINGTOID"]);
                objDoctor.roleType = Convert.ToString(dtDoctors.Rows[index]["USERTYPE"]);
                objDoctor.specialtyType = Convert.ToString(dtDoctors.Rows[index]["DEPARTMENTID"]);
                objDoctor.ssn = Convert.ToString(dtDoctors.Rows[index]["SSN"]);
                objDoctor.roleList = Convert.ToString(dtDoctors.Rows[index]["RoleList"]);
                objDoctor.isActive = Convert.ToInt32(dtDoctors.Rows[index]["IsActive"]);
                lstDetails.Add(objDoctor);
            }
            return lstDetails;
        }
        #endregion

        private InsuranceData GetInsuranceDataByPatientId(string patientId)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dtPatient = GetPatientInfoForClaimMD(patientId);
            InsuranceData objInsurance = new InsuranceData();

            if (dtPatient.Rows.Count>0)
            {
                //TODO
                // Map Patient details to CURL string
                sb.AppendFormat(@"curl -i -F AccountKey=9295oIlHpKlworVmEoiWzTbuSjdM -F UserID=sanjay -F payerid=""87726"" -F ins_name_l=""SMITH"" -F ins_name_f=""BOB"" ");
                sb.AppendFormat(@"-F ins_dob=""2015-07-30"" -F ins_number=""12341234"" -F pat_rel=""18"" -F service_code=""30"" -F fdos=""2019-07-13"" -F prov_name_l=""COMPANY"" -F prov_npi=""1111111112"" ");
                sb.AppendFormat(@"-F prov_taxonomy=""207P00000X"" -F prov_taxid=""999999999"" -F prov_taxid_type=""E"" -F prov_addr_1=""21 JUMP ST"" -F prov_city=""Oradell"" -F prov_state=""NJ"" -F prov_zip=""076491519"" https://www.claim.md/services/eligxml/");

                // Call Curl command to GET Insurance Data from Claim.MD
                // Bind Insurance Data to <InsuranceData> object
                
                objInsurance.CoPAyAmount = "$60";
                objInsurance.InsuranceDetails = "Insurance Details";
                objInsurance.InsuranceProvidor = "Cigna";
                objInsurance.PatientId = patientId;
                objInsurance.PolicyHolder = "Sanjay";
            }

            //var client = new HttpClient();
            try
            {
                //// Create the HttpContent for the form to be posted.
                //var requestContent = new FormUrlEncodedContent(new[] {
                //new KeyValuePair<string, string>("text", sb.ToString()),
                //});

                //// Get the response.
                //HttpResponseMessage response = await client.PostAsync(
                //    "https://www.claim.md/services/eligxml/",
                //    requestContent);

                //// Get the response content.
                //HttpContent responseContent = response.Content;

                //// Get the stream of the content.
                //using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                //{
                //    // Write the output.
                //    //Console.WriteLine(await reader.ReadToEndAsync());
                //    var ss = await reader.ReadToEndAsync();
                //}

                //using (var httpClient = new HttpClient())
                //{
                //    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://www.claim.md/services/eligxml/"))
                //    {
                //        var multipartContent = new MultipartFormDataContent();
                //        multipartContent.Add(new StringContent("9295oIlHpKlworVmEoiWzTbuSjdM"), "AccountKey");
                //        multipartContent.Add(new StringContent("sanjay"), "UserID");
                //        multipartContent.Add(new StringContent("87726"), "payerid");
                //        multipartContent.Add(new StringContent("SMITH"), "ins_name_l");
                //        multipartContent.Add(new StringContent("BOB"), "ins_name_f");
                //        multipartContent.Add(new StringContent("2015-07-30"), "ins_dob");
                //        multipartContent.Add(new StringContent("12341234"), "ins_number");
                //        multipartContent.Add(new StringContent("18"), "pat_rel");
                //        multipartContent.Add(new StringContent("30"), "service_code");
                //        multipartContent.Add(new StringContent("2019-07-13"), "fdos");
                //        multipartContent.Add(new StringContent("COMPANY"), "prov_name_l");
                //        multipartContent.Add(new StringContent("1111111112"), "prov_npi");
                //        multipartContent.Add(new StringContent("207P00000X"), "prov_taxonomy");
                //        multipartContent.Add(new StringContent("999999999"), "prov_taxid");
                //        multipartContent.Add(new StringContent("E"), "prov_taxid_type");
                //        multipartContent.Add(new StringContent("21 JUMP ST"), "prov_addr_1");
                //        multipartContent.Add(new StringContent("Oradell"), "prov_city");
                //        multipartContent.Add(new StringContent("NJ"), "prov_state");
                //        multipartContent.Add(new StringContent("076491519"), "prov_zip");
                //        request.Content = multipartContent;

                //        var response = await httpClient.SendAsync(request);

                //        HttpContent responseContent = response.Content;

                //        // Get the stream of the content.
                //        using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                //        {
                //            // Write the output.
                //            //Console.WriteLine(await reader.ReadToEndAsync());
                //            var ss = await reader.ReadToEndAsync();
                //        }
                //    }
                //}

                //string str = PostCurlCommand(sb.ToString(), 15);
            }
            catch (Exception ex)
            {
                
            }

            return objInsurance;
        }
        public InsuranceResponseDTO ValidatePatientInsurance(string patientId)
        {
            InsuranceResponseDTO responseObj = new InsuranceResponseDTO();
            var retVal = GetInsuranceDataByPatientId(patientId);
            if(retVal!=null)
            {
                responseObj.data = retVal;
                responseObj.HasInsuranceData = true;
                responseObj.IsActiveInsurance = true;
            }
            responseObj.Code = "";
            responseObj.Status = "";

            return responseObj;
        }
        public int ResetPasswordEmail(SendEmailResetPasswordParams emailParams)
        {
            int status = 0;
            try
            {
                emailService.SendResetPasswordLinkEmail(emailParams.Email, emailParams.LandingPageLink);
                status = 1;
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }

        public int CreatePasswordEmail(SendEmailCreatePasswordParams emailParams)
        {
            int status = 0;
            try
            {
                emailService.SendCreatePasswordEmail(emailParams.Email, emailParams.UserName, emailParams.UserId, emailParams.PasswordLink);
                status = 1;
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }

        private string PostCurlCommand(string curlCommand, int timeoutInSeconds)
        {
            if (string.IsNullOrEmpty(curlCommand))
                return "";

            curlCommand = curlCommand.Trim();
            if (curlCommand.StartsWith("curl"))
            {
                curlCommand = curlCommand.Substring("curl".Length).Trim();
            }

            curlCommand = curlCommand.Replace("--compressed", "");

            var fullPath = System.IO.Path.Combine(Environment.SystemDirectory, "curl.exe");
            if (System.IO.File.Exists(fullPath) == false)
            {
                //if (Debugger.IsAttached) { Debugger.Break(); }
                throw new Exception("Windows 10 or higher is required to run this application");
            }

            List<string> parameters = new List<string>();

            try
            {
                Queue<char> q = new Queue<char>();

                foreach (var c in curlCommand.ToCharArray())
                {
                    q.Enqueue(c);
                }

                StringBuilder currentParameter = new StringBuilder();

                void insertParameter()
                {
                    var temp = currentParameter.ToString().Trim();
                    if (string.IsNullOrEmpty(temp) == false)
                    {
                        parameters.Add(temp);
                    }

                    currentParameter.Clear();
                }

                while (true)
                {
                    if (q.Count == 0)
                    {
                        insertParameter();
                        break;
                    }

                    char x = q.Dequeue();

                    if (x == '\'')
                    {
                        insertParameter();

                        while (true)
                        {
                            x = q.Dequeue();
                            if (x == '\\' && q.Count > 0 && q.Peek() == '\'')
                            {
                                currentParameter.Append('\'');
                                q.Dequeue();
                                continue;
                            }

                            if (x == '\'')
                            {
                                insertParameter();
                                break;
                            }

                            currentParameter.Append(x);
                        }
                    }
                    else if (x == '"')
                    {
                        insertParameter();

                        while (true)
                        {
                            x = q.Dequeue();

                            // if next 2 characetrs are \"
                            if (x == '\\' && q.Count > 0 && q.Peek() == '"')
                            {
                                currentParameter.Append('"');
                                q.Dequeue();
                                continue;
                            }

                            if (x == '"')
                            {
                                insertParameter();
                                break;
                            }

                            currentParameter.Append(x);
                        }
                    }
                    else
                    {
                        currentParameter.Append(x);
                    }
                }
            }
            catch
            {
                //if (Debugger.IsAttached) { Debugger.Break(); }
                throw new Exception("Invalid curl command");
            }

            StringBuilder finalCommand = new StringBuilder();

            foreach (var p in parameters)
            {
                if (p.StartsWith("-"))
                {
                    finalCommand.Append(p);
                    finalCommand.Append(" ");
                    continue;
                }

                var temp = p;

                if (temp.Contains("\""))
                {
                    temp = temp.Replace("\"", "\\\"");
                }

                if (temp.Contains("'"))
                {
                    temp = temp.Replace("'", "\\'");
                }

                finalCommand.Append($"\"{temp}\"");
                finalCommand.Append(" ");
            }

            using (var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "curl.exe",
                    Arguments = finalCommand.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                    WorkingDirectory = Environment.SystemDirectory
                }
            })
            {
                proc.Start();

                proc.WaitForExit(timeoutInSeconds * 10);

                var bb = proc.StandardOutput.ReadToEnd();

                return bb;
            }
        }

        #region SaveUser***************************************************************************************************************************************
        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="id">string</param>
        /// <param name="xmlUser">string</param>
        /// <returns>string</returns>
        public string SaveUser(string id,string xmlUser,string fullname,string email)
        {
            string result = string.Empty;
            string sqlUser = " EXEC [USP_CreateUser] '"+ id +"','" + xmlUser + "'";
            result = (string)db.GetSingleValue(sqlUser);
            try
            {

                if (id == "0")
                {

                    emailService.sendUserEmail(fullname, email);
                }
                
                //SendMail(tomail, body);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(AppointmentService));
                logger.Error(ex);
                return result;
            }
            return result;
        }

        public string SaveTemplate(int id, string title, string description, char tag)
        {
            string result = string.Empty;
            string sqlTemplate = string.Empty;
            try
            {

                sqlTemplate = " EXEC [USP_SaveTEMPLATE] '" + id + "','" + title + "', '" + description + "', '" + tag + "'";
                result = (string)db.GetSingleValue(sqlTemplate);
            }
            catch (Exception ex)
            {
                var logger = logService.GetLogger(typeof(AppointmentService));
                logger.Error(ex);
                return result;
            }
            return result;
        }
        #endregion

        private DataTable GetTemplateById(int id)
        {
            DataTable dtPatient = new DataTable();
            string sqlDoctor = "exec USP_GetTEMPLATE " + id;
            dtPatient = db.GetData(sqlDoctor);
            return dtPatient;
        }

        private List<TemplateData> GetTemplateDataFromDatatable(DataTable dtTemplate)
        {
            List<TemplateData> objData = new List<TemplateData>();
            if (dtTemplate.Rows.Count>0)
            {
                for (int index = 0; index <= dtTemplate.Rows.Count - 1; index++)
                {
                    TemplateData obj = new TemplateData();
                    obj.ID = Convert.ToInt32(dtTemplate.Rows[index]["ID"]);
                    obj.Title = Convert.ToString(dtTemplate.Rows[index]["Title"]);
                    obj.Description = Convert.ToString(dtTemplate.Rows[index]["Description"]);

                    objData.Add(obj);
                }
            }
            return objData;
        }

        public TemplateDTO TemplateByTemplateId(int id)
        {
            TemplateDTO retObj = new TemplateDTO();
            var retVal = GetTemplateDataFromDatatable(GetTemplateById(id));

            retObj.data = retVal;
            retObj.Code = "";
            retObj.Status = "";

            return retObj;
        }

        #region UpdateStatusofUser*****************************************************************************************************************************
        /// <summary>
        /// UpdateStatusofUser
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="status">int</param>
        /// <returns>string</returns>
        public string UpdateStatusofUser(int id,int status)
        {
            string result = string.Empty;
            string sqlUser = "USP_UPDATE_STATUS_OF_USER " + id + "," + status;
            result = (string)db.GetSingleValue(sqlUser);
            return result;
        }

        private string ValidateUserEmail(string userEmail)
        {
            string result = string.Empty;
            string sqlUser = "USP_ValidateUserEmail '" + userEmail + "'";
            result = (string)db.GetSingleValue(sqlUser);
            return result;
        }

        private string ResetUserPasswordDB(string userEmail, string userPassword)
        {
            string result = string.Empty;
            string sqlUser = "USP_UpdateUserPassword '" + userEmail + "', '" + userPassword + "'";
            result = (string)db.GetSingleValue(sqlUser);
            return result;
        }

        public ResetPasswordDTO GetResponseResetPassword(string userEmail, string userPassword)
        {
            string retVal = ResetUserPasswordDB(userEmail, userPassword);
            ResetPasswordDTO returnObj = new ResetPasswordDTO();
            ReturnData retData = new ReturnData();
            if (retVal.Contains("Your new password has been"))
            {
                retData.Valid = true;
            }
            else
            {
                retData.Valid = false;
            }
            retData.Message = retVal;
            returnObj.data = retData;

            returnObj.Status = "";
            returnObj.Code = "";

            return returnObj;
        }

        public ResetPasswordDTO GetResponseValidateUserEmail(string userEmail)
        {
            string retVal = ValidateUserEmail(userEmail);
            ResetPasswordDTO returnObj = new ResetPasswordDTO();
            ReturnData retData = new ReturnData();
            if(retVal.Contains("We sent an"))
            {
                retData.Valid = true;
            }
            else
            {
                retData.Valid = false;
            }
            retData.Message = retVal;
            returnObj.data = retData;

            returnObj.Status = "";
            returnObj.Code = "";

            return returnObj;
        }
        #endregion

        #region BindDoctorList*********************************************************************************************************************************
        /// <summary>
        /// BindDoctorList
        /// </summary>
        /// <param name="userType">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindDoctorList(string userType = "")
        {
            DataTable dtDoctor = new DataTable();
            string sqlDoctor = "exec USP_FETCH_USERLIST '" + userType + "'";
            dtDoctor = db.GetData(sqlDoctor);
            return dtDoctor;
        }
        #endregion

        private DataTable GetPatientInfoForClaimMD(string patientId)
        {
            DataTable dtPatient = new DataTable();
            string sqlDoctor = "exec USP_GETINSURANCEELIGIBILITY '" + patientId + "'";
            dtPatient = db.GetData(sqlDoctor);
            return dtPatient;
        }
    }
}
