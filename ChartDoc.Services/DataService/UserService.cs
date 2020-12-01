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
        #endregion

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
    }
}
