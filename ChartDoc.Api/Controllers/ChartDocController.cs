#region Using***********************************************************************************************************************************************************
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
#endregion

namespace ChartDoc.Api.Controllers
{
    [Route("api/ChartDoc")]
    [EnableCors("AllowMyOrigin")]
    [ApiController]
    public class ChartDocController : ControllerBase
    {
        #region Declaration of variables
        private readonly IUserService _userService;
        private readonly IIcdService _icdService;
        private readonly IPatientDetailsService _patientDetailsService;
        private readonly IDiagnosisService _diagnosisService;
        private readonly IProcedureService _procedureService;
        private readonly IDepartmentService _departmentService;
        private readonly IInsuranceService _insuranceService;
        private readonly ICalendarService _calendarService;
        private readonly IReasonService _reasonService;
        private readonly ICoPayService _copayService;
        private readonly IFlowSheetService _flowSheetService;
        private readonly ICptService _cptService;
        private readonly IEncounterService _encounterService;
        private readonly IRoleService _roleService;
        private readonly IUserTypeService _userTypeService;
        private readonly IOthersPopulateService _othersPopulateService;
        private readonly IPatientInformationService _patientInformationService;
        private readonly IServiceDetailsService _serviceDetailsService;
        private readonly IImpressionPlanService _impressionPlanService;
        private readonly IDocumentService _documentService;
        private readonly IChiefComplaintService _chiefComplaintService;
        private readonly IFollowupService _followupService;
        private readonly IObservationService _observationService;
        private readonly ISharedService _sharedService;
        private readonly IAllergiesService _allergiesService;
        private readonly IImmunizationService _immunizationService;
        private readonly ISocialService _socialService;
        private readonly IFamilyService _familyService;
        private readonly ICheckOutService _checkOutService;
        private readonly IScheduleAppointmentService _scheduleAppointmentService;
        private readonly IFlowAreaService _flowAreaService;
        private readonly IAppointmentService _appointmentService;
        private IHostingEnvironment _environment;
        private readonly IAllergyImmunizationAlertSocialFamilyService _allergyImmunizationAlertSocialFamilyService;
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;
        private readonly IFileControlService _fileControlService;
        private readonly ILogService _logService;
        private readonly IChargeDateRangeService _chargeDateRangeService;
        private readonly IChargeMasterService _chargeMasterService;
        private readonly IClaimService _claimService;
        private readonly IPaymentService _paymentService;
        private readonly IClaimFieldsService _claimFieldsService;
        private readonly IClaimStatusService _claimStatusService;

        private readonly IClaimFieldMasterService _claimFieldMasterService;
        private readonly IReportService _reportService;
        private readonly IUserAccessService _userAccessService;
        private readonly IClaimDetailsService _claimDetailsService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor : ChartDocController
        /// </summary>
        /// <param name="userService">IUserService</param>
        /// <param name="icdService">IIcdService</param>
        /// <param name="patientDetailsService">IPatientDetailsService</param>
        /// <param name="diagnosisService">IDiagnosisService</param>
        /// <param name="procedureService">IProcedureService</param>
        /// <param name="departmentService">IDepartmentService</param>
        /// <param name="insuranceService">IInsuranceService</param>
        /// <param name="calendarService">ICalendarService</param>
        /// <param name="reasonService">IReasonService</param>
        /// <param name="coPayService">ICoPayService</param>
        /// <param name="flowSheetService">IFlowSheetService</param>
        /// <param name="cptService">ICptService</param>
        /// <param name="encounterService">IEncounterService</param>
        /// <param name="roleService">IRoleService</param>
        /// <param name="userTypeService">IUserTypeService</param>
        /// <param name="othersPopulateService">IOthersPopulateService</param>
        /// <param name="patientInformationService">IPatientInformationService</param>
        /// <param name="serviceDetailsService">IServiceDetailsService</param>
        /// <param name="impressionPlanService">IImpressionPlanService</param>
        /// <param name="documentService">IDocumentService</param>
        /// <param name="chiefComplaintService">IChiefComplaintService</param>
        /// <param name="followupService">IFollowupService</param>
        /// <param name="observationService">IObservationService</param>
        /// <param name="sharedService">ISharedService</param>
        /// <param name="allergiesService">IAllergiesService</param>
        /// <param name="immunizationService">IImmunizationService</param>
        /// <param name="socialService">ISocialService</param>
        /// <param name="familyService">IFamilyService</param>
        /// <param name="checkOutService">ICheckOutService</param>
        /// <param name="scheduleAppointmentService">IScheduleAppointmentService</param>
        /// <param name="flowAreaService">IFlowAreaService</param>
        /// <param name="appointmentService">IAppointmentService</param>
        /// <param name="environment">IHostingEnvironment</param>
        /// <param name="allergyImmunizationAlertSocialFamilyService">IAllergyImmunizationAlertSocialFamilyService</param>
        /// <param name="medicineService">IMedicineService</param>
        /// <param name="categoryService">ICategoryService</param>
        /// <param name="fileControlService">IFileControlService</param>
        /// <param name="logService">ILogService</param>
        /// <param name="chargeDateRangeService">IChargeDateRangeService</param>
        /// <param name="chargeMasterService">IChargeMasterService</param>
        /// <param name="claimService">IClaimService</param>
        /// <param name="paymentService">IPaymentService</param>
        /// <param name="claimStatusService">IClaimStatusService</param>
        public ChartDocController(IUserService userService,
            IIcdService icdService,
            IPatientDetailsService patientDetailsService,
            IDiagnosisService diagnosisService,
            IProcedureService procedureService,
            IDepartmentService departmentService,
            IInsuranceService insuranceService,
            ICalendarService calendarService,
            IReasonService reasonService,
            ICoPayService coPayService,
            IFlowSheetService flowSheetService,
            ICptService cptService,
            IEncounterService encounterService,
            IRoleService roleService,
            IUserTypeService userTypeService,
            IOthersPopulateService othersPopulateService,
            IPatientInformationService patientInformationService,
            IServiceDetailsService serviceDetailsService,
            IImpressionPlanService impressionPlanService,
            IDocumentService documentService,
            IChiefComplaintService chiefComplaintService,
            IFollowupService followupService,
            IObservationService observationService,
            ISharedService sharedService,
            IAllergiesService allergiesService,
            IImmunizationService immunizationService,
            ISocialService socialService,
            IFamilyService familyService,
            ICheckOutService checkOutService,
            IScheduleAppointmentService scheduleAppointmentService,
            IFlowAreaService flowAreaService,
            IAppointmentService appointmentService,
            IHostingEnvironment environment,
            IAllergyImmunizationAlertSocialFamilyService allergyImmunizationAlertSocialFamilyService,
            IMedicineService medicineService,
            ICategoryService categoryService,
            IFileControlService fileControlService,
            ILogService logService,
            IChargeDateRangeService chargeDateRangeService,
            IChargeMasterService chargeMasterService,
            IClaimService claimService,
            IPaymentService paymentService,
            IClaimFieldsService claimFieldsService,
             IClaimStatusService claimStatusService,
              IReportService reportService,
              IUserAccessService userAccessService
            )
        {
            _userService = userService;
            _icdService = icdService;
            _patientDetailsService = patientDetailsService;
            _diagnosisService = diagnosisService;
            _procedureService = procedureService;
            _departmentService = departmentService;
            _insuranceService = insuranceService;
            _calendarService = calendarService;
            _reasonService = reasonService;
            _copayService = coPayService;
            _flowSheetService = flowSheetService;
            _cptService = cptService;
            _encounterService = encounterService;
            _roleService = roleService;
            _userTypeService = userTypeService;
            _othersPopulateService = othersPopulateService;
            _patientInformationService = patientInformationService;
            _serviceDetailsService = serviceDetailsService;
            _impressionPlanService = impressionPlanService;
            _documentService = documentService;
            _chiefComplaintService = chiefComplaintService;
            _followupService = followupService;
            _observationService = observationService;
            _sharedService = sharedService;
            _allergiesService = allergiesService;
            _immunizationService = immunizationService;
            _socialService = socialService;
            _familyService = familyService;
            _checkOutService = checkOutService;
            _scheduleAppointmentService = scheduleAppointmentService;
            _flowAreaService = flowAreaService;
            _appointmentService = appointmentService;
            _environment = environment;
            _allergyImmunizationAlertSocialFamilyService = allergyImmunizationAlertSocialFamilyService;
            _medicineService = medicineService;
            _categoryService = categoryService;
            _fileControlService = fileControlService;
            _logService = logService;
            _chargeDateRangeService = chargeDateRangeService;
            _chargeMasterService = chargeMasterService;
            _claimService = claimService;
            _paymentService = paymentService;
            _claimFieldsService = claimFieldsService;
            _claimStatusService = claimStatusService;
            _reportService = reportService;
            _userAccessService = userAccessService;
            _claimDetailsService = claimDetailsService;
        }
        #endregion

        #region GET Api 
        #region 1st Phase
        #region Get Login Status
        /// <summary>
        /// GetLoginStatus : To get login Status
        /// </summary>
        /// <param name="userName">string</param>
        /// <param name="password">string</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLoginStatus/{userName}/{password}")]
        public ActionResult<clsUser> GetLoginStatus(string userName, string password)
        {
            return _userService.GetUser(userName, password);
        }
        #endregion

        #region Get ICD Details
        /// <summary>
        /// GetICD : Get ICD Details
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsICD>></returns>
        [HttpGet]
        [Route("GetICD")]
        public ActionResult<IEnumerable<clsICD>> GetICD()
        {
            return _icdService.GetAllICD();
        }

        /// <summary>
        /// GetSavedICD : Get ICD Details
        /// </summary>
        /// <param name="PatientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsICD>></returns>
        [HttpGet]
        [Route("GetSavedICD/{PatientId}")]
        public ActionResult<IEnumerable<clsICD>> GetSavedICD(string PatientId)
        {
            return _icdService.GetSavedICD(PatientId);
        }
        #endregion

        #region Search Patient
        /// <summary>
        /// SearchPatient : Search Patient
        /// </summary>
        /// <param name="firstName">string</param>
        /// <param name="lastName">string</param>
        /// <param name="dob">string</param>
        /// <param name="mobile">string</param>
        /// <param name="email">string</param>
        /// <returns>ActionResult<IEnumerable<clsPatientDetails>></returns>
        [HttpGet]
        [Route("SearchPatient/{firstName}/{lastName}/{DOB}/{Mobile}/{Email}/{Gender}/{Isactivated}")]
        public ActionResult<IEnumerable<clsPatientDetails>> SearchPatient(string firstName = "", string lastName = "", string dob = null, string mobile = "", string email = "", string gender ="",string isactivated="")
        {
            firstName = firstName.Replace("{", "").Replace("}", "").Trim().ToUpper();
            lastName = lastName.Replace("{", "").Replace("}", "").Trim().ToUpper();
            dob = dob.Replace("{", "").Replace("}", "").Trim();
            mobile = mobile.Replace("{", "").Replace("}", "").Trim();
            email = email.Replace("{", "").Replace("}", "").Trim();
            gender = gender.Replace("{", "").Replace("}", "").Trim();
            isactivated = isactivated.Replace("{", "").Replace("}", "").Trim();
            return _patientDetailsService.SearchPatient(firstName, lastName, dob, mobile, email, gender, isactivated);
        }
        #endregion

        #region Get All Patients Details
        /// <summary>
        /// GetAllPatients:Get All Patients Details
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsPatientDetails>></returns>
        [HttpGet]
        [Route("GetAllPatients")]
        public ActionResult<IEnumerable<clsPatientDetails>> GetAllPatients()
        {
            return _patientDetailsService.SearchPatient("", "", "", "", "", "","");
        }
        #endregion

        #region Get Patient Diagnosis Details
        /// <summary>
        /// GetDiagnosisByPatientId:Get Patient Diagnosis Details.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns> ActionResult<IEnumerable<clsDiagnosis>> </returns>
        [HttpGet]
        [Route("GetDiagnosisByPatientId/{patientId}")]
        public ActionResult<IEnumerable<clsDiagnosis>> GetDiagnosisByPatientId(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _diagnosisService.GetDiagnosisByPatientId(patientId);
        }
        #endregion

        #region Get Patient Procedure Details
        /// <summary>
        /// GetProceduresByPatientId:Get Patient Procedure Details.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsProcedures>></returns>
        [HttpGet]
        [Route("GetProceduresByPatientId/{patientId}")]
        public ActionResult<IEnumerable<clsProcedures>> GetProceduresByPatientId(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _procedureService.GetProcedureByPatientId(patientId);
        }
        #endregion

        #region Get All Departments
        /// <summary>
        /// GetDept:Get All Departments
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsDept>></returns>
        [HttpGet]
        [Route("GetDept")]
        public ActionResult<IEnumerable<clsDept>> GetDept()
        {
            return _departmentService.GetDept();
        }
        #endregion

        #region Get Insurance By PatientId
        /// <summary>
        /// GetInsurance:Get Insurance By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<string>></returns>
        [HttpGet]
        [Route("GetInsurance/{PatientID}")]
        public ActionResult<IEnumerable<string>> GetInsurance(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _insuranceService.GetInsurance(patientId);
        }
        #endregion

        #region Get Medicine By PatientId
        /// <summary>
        /// GetMedicine:Get Medicine By PatientId
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsMedicine>></returns>
        [HttpGet]
        [Route("GetMedicine/{PatientId}")]
        public ActionResult<IEnumerable<clsMedicine>> GetMedicine(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _medicineService.GetMedicine(patientId);
        }
        #endregion

        #region Get Doctors List
        /// <summary>
        /// GetDoctorList: Get Doctors List.
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>ActionResult<IEnumerable<clsDoctor>></returns>
        [HttpGet]
        [Route("GetDoctorList/{date}")]
        public ActionResult<IEnumerable<clsDoctor>> GetDoctorList(string date)
        {
            return _userService.GetAllDoctorsDetails(date);
        }
        #endregion

        #region Get User List
        /// <summary>
        /// GetUserList: Get User List.
        /// </summary>
        /// <param name="userType">string</param>
        /// <returns>ActionResult<IEnumerable<clsDoctorList>></returns>
        [HttpGet]
        [Route("GetUserList")]
        public ActionResult<IEnumerable<clsDoctorList>> GetUserList(string userType = "")
        {
            return _userService.GetUserList(userType);
        }
        #endregion

        #region Get Calendar Details
        /// <summary>
        /// GetCalender: Get Calendar Details.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsCalender>></returns>
        [HttpGet]
        [Route("GetCalender")]
        public ActionResult<IEnumerable<clsCalender>> GetCalender()
        {
            return _calendarService.GetCalender();
        }
        #endregion

        #region Get Office Calendar List
        /// <summary>
        /// GetOfficeCalenderList: Get Office Calendar List.
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>ActionResult<IEnumerable<clsFlowSheet>></returns>
        [HttpGet]
        [Route("GetOfficeCalenderList/{date}")]
        public ActionResult<IEnumerable<clsFlowSheet>> GetOfficeCalenderList(string date)
        {
            return _flowSheetService.GetOfficeCalenderList(date);
        }
        #endregion

        #region Appointment Weekly View
        /// <summary>
        /// AppointmentWeeklyView: Appointment Weekly View.
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="doctorId">string</param>
        /// <returns>ActionResult<IEnumerable<clsFlowSheet>></returns>
        [HttpGet]
        [Route("AppointmentWeeklyView/{date}/{DoctorID}")]
        public ActionResult<IEnumerable<clsFlowSheet>> AppointmentWeeklyView(string date, string doctorId)
        {
            return _flowSheetService.AppointmentWeeklyView(date, doctorId);
        }
        #endregion        

        #region Get Reason
        /// <summary>
        /// GetReason: Get Reason.
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>ActionResult<IEnumerable<clsReason>></returns>
        [HttpGet]
        [Route("GetReason/{Type}")]
        public ActionResult<IEnumerable<clsReason>> GetReason(string type)
        {
            return _reasonService.GetReason(type);
        }
        #endregion

        #region Get CoPay
        /// <summary>
        /// GetCOPay: Get CoPay.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>ActionResult<IEnumerable<clsCOPay>></returns>
        [HttpGet]
        [Route("GetCOPay/{AppointmentID}")]
        public ActionResult<IEnumerable<clsCOPay>> GetCOPay(string appointmentId)
        {
            return _copayService.GetCOPay(appointmentId);
        }
        #endregion

        #region Get Appointment
        /// <summary>
        /// GetAppointment: Get Appointment.
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>ActionResult<IEnumerable<clsFlowSheet>></returns>
        [HttpGet]
        [Route("GetAppointment/{date}")]
        public ActionResult<IEnumerable<clsFlowSheet>> GetAppointment(string date)
        {
            return _flowSheetService.GetAppointment(date);
        }
        #endregion

        #region Get CPT Details
        /// <summary>
        /// GetCPT: Get CPT Details.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsCPT>></returns>
        [HttpGet]
        [Route("GetCPT")]
        public ActionResult<IEnumerable<clsCPT>> GetCPT()
        {
            return _cptService.GetCPT();
        }

        /// <summary>
        /// GetSavedCPT: Get Saved CPT.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsCPT>></returns>
        [HttpGet]
        [Route("GetSavedCPT/{PatientId}")]
        public ActionResult<IEnumerable<clsCPT>> GetSavedCPT(string patientId)
        {
            return _cptService.GetSavedCPT(patientId);
        }
        #endregion

        #region Get Encounter By PatientID
        /// <summary>
        /// GetEncounter: Get Encounter By PatientID.
        /// </summary>
        /// <param name="PatientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsEncounter>></returns>
        [HttpGet]
        [Route("GetEncounter/{PatientId}")]
        public ActionResult<IEnumerable<clsEncounter>> GetEncounter(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _encounterService.GetEncounter(patientId);

        }
        #endregion

        #region Get All Roles
        /// <summary>
        /// GetRole: Get All Roles.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsRole>></returns>
        [HttpGet]
        [Route("GetRole")]
        public ActionResult<IEnumerable<clsRole>> GetRole()
        {
            return _roleService.GetRole();
        }
        #endregion

        #region Get User Type By Id
        /// <summary>
        /// GetUType: Get User Type By Id.
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ActionResult<IEnumerable<clsUType>></returns>
        [HttpGet]
        [Route("GetUType/{Id}")]
        public ActionResult<IEnumerable<clsUType>> GetUType(string id)
        {
            return _userTypeService.GetUType(id);
        }
        #endregion

        #region Get Others By Type Id
        /// <summary>
        /// OtherPopulate: Get Others By Type Id.
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ActionResult<IEnumerable<clsOtherPopulate>></returns>
        [HttpGet]
        [Route("OtherPopulate/{Id}")]
        public ActionResult<IEnumerable<clsOtherPopulate>> OtherPopulate(string id)
        {
            return _othersPopulateService.OtherPopulate(id);
        }
        #endregion

        #region Get Patient Details By Patient Id
        /// <summary>
        /// GetPatientInfo: Get Patient Details By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<clsCreateUpdatePatient></returns>
        [HttpGet]
        [Route("GetPatientInfo/{PatientId}")]
        public ActionResult<clsCreateUpdatePatient> GetPatientInfo(string patientId)
        {
            return _patientInformationService.GetPatientInfo(patientId);
        }
        #endregion

        #region Get Service Details
        /// <summary>
        /// GetServiceDetails: Get Service Details.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsService>></returns>
        [HttpGet]
        [Route("GetServiceDetails")]
        public ActionResult<IEnumerable<clsService>> GetServiceDetails()
        {
            return _serviceDetailsService.GetServiceDetails();
        }
        #endregion

        #region Get FlowSheet Details
        /// <summary>
        /// GetFlowsheet: Get FlowSheet Details.
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="doctorId">string</param>
        /// <returns>ActionResult<IEnumerable<clsFlowSheet>></returns>
        [HttpGet]
        [Route("GetFlowsheet/{date}/{DoctorID}")]
        public ActionResult<IEnumerable<clsFlowSheet>> GetFlowsheet(string date, string doctorId)
        {
            return _flowSheetService.GetFlowsheet(date, doctorId);
        }
        #endregion

        #region Get Patient By Patient Id
        /// <summary>
        /// SearchPatientbyID: Get Patient By Patient Id.
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ActionResult<clsPatientDetails></returns>
        [HttpGet]
        [Route("SearchPatientbyID/{ID}")]
        public ActionResult<clsPatientDetails> SearchPatientbyID(string id)
        {
            return _patientDetailsService.SearchPatientbyID(id);
        }
        #endregion

        #region Get Impression Plan
        /// <summary>
        /// GetImpressionPlan: Get Impression Plan.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>ActionResult<IEnumerable<clsImpression>></returns>
        [HttpGet]
        [Route("GetImpressionPlan/{AppointmentId}")]
        public ActionResult<IEnumerable<clsImpression>> GetImpressionPlan(string appointmentId)
        {
            return _impressionPlanService.GetImpressionPlan(appointmentId);
        }
        #endregion

        #region Get Document Details By PatientId
        /// <summary>
        /// GetDocument: Get Document Details By PatientId.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <param name="flag">string</param>
        /// <param name="id">int</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDocument/{PatientId}/{Flag}/{id}")]
        public ActionResult<IEnumerable<clsDocument>> GetDocument(string patientId, string flag, int id)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _documentService.GetDocument(patientId, flag, id);
        }
        #endregion

        #region Get Chief Complaint By Appointment Id
        /// <summary>
        /// GetChiefComplaint: Get Chief Complaint By Appointment Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsCC>></returns>
        [HttpGet]
        [Route("GetChiefComplaint/{PatientId}")]
        public ActionResult<IEnumerable<clsCC>> GetChiefComplaint(string patientId)
        {
            return _chiefComplaintService.GetChiefComplaint(patientId);
        }
        #endregion

        #region Get Follow Up By Appointment Id
        /// <summary>
        /// GetFollowUp: Get Follow Up By Appointment Id.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <returns>ActionResult<IEnumerable<clsFollowUp>></returns>
        [HttpGet]
        [Route("GetFollowUp/{AppointmentId}")]
        public ActionResult<IEnumerable<clsFollowUp>> GetFollowUp(string appointmentId)
        {
            return _followupService.GetFollowUp(appointmentId);
        }
        #endregion

        #region Get Observation By Appointment Id
        /// <summary>
        /// GetObservation: Get Observation By Appointment Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsObservation>></returns>
        [HttpGet]
        [Route("GetObservation/{PatientId}")]
        public ActionResult<IEnumerable<clsObservation>> GetObservation(string patientId)
        {
            return _observationService.GetObservation(patientId);
        }
        #endregion

        #region Get Allergies By Patient Id
        /// <summary>
        /// GetAllergies: Get Allergies By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsAllergies>></returns>
        [HttpGet]
        [Route("GetAllergies/{PatientId}")]
        public ActionResult<IEnumerable<clsAllergies>> GetAllergies(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _allergiesService.GetAllergies(patientId);
        }
        #endregion

        #region Get Immunization By Patient Id
        /// <summary>
        /// GetImmunizations: Get Immunization By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsImmunizations>></returns>
        [HttpGet]
        [Route("GetImmunizations/{PatientId}")]
        public ActionResult<IEnumerable<clsImmunizations>> GetImmunizations(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _immunizationService.GetImmunizations(patientId);
        }
        #endregion

        #region Get Social Information By Patient Id
        /// <summary>
        /// GetSocials: Get Social Information By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsSocials>></returns>
        [HttpGet]
        [Route("GetSocials/{PatientId}")]
        public ActionResult<IEnumerable<clsSocials>> GetSocials(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _socialService.GetSocials(patientId);
        }
        #endregion

        #region Get Families History By Patient Id
        /// <summary>
        /// GetFamilies: Get Families History By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsFamilies>></returns>
        [HttpGet]
        [Route("GetFamilies/{PatientId}")]
        public ActionResult<IEnumerable<clsFamilies>> GetFamilies(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _familyService.GetFamilies(patientId);
        }
        #endregion

        #region Get Patient Alert By Patient Id
        /// <summary>
        /// GetFamilies: Get Patient Alert By Patient Id.
        /// </summary>
        /// <param name="patientId">string</param>
        /// <returns>ActionResult<IEnumerable<clsFamilies>></returns>
        [HttpGet]
        [Route("GetAlert/{PatientId}")]
        public ActionResult<IEnumerable<clsAlert>> GetAlert(string patientId)
        {
            patientId = _sharedService.DecodeString(patientId);
            return _familyService.GetAlert(patientId);
        }
        #endregion

        #region Get Category Details
        /// <summary>
        /// GetCategoryDetails: Get Category Details.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsCategory>></returns>
        [HttpGet]
        [Route("GetCategoryDetails")]
        public ActionResult<IEnumerable<clsCategory>> GetCategoryDetails()
        {
            return _categoryService.GetCategoryDetails();
        }
        #endregion

        #region Update Status
        /// <summary>
        /// UpdateStatusOfUser: Update Status.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="status">int</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("UpdateStatusOfUser/{id}/{status}")]
        public string UpdateStatusOfUser(int id, int status)
        {
            string result = "Nil";
            result = _userService.UpdateStatusofUser(id, status);
            return result;
        }
        #endregion
        #endregion

        #region 2nd Phase

        #region Get Charge Date Range
        /// <summary>
        /// Get Charge Date Range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChargeDateRange")]
        public ActionResult<IEnumerable<clsChargeDateRange>> GetChargeDateRange(string fromDate = "", string toDate = "")
        {
            return _chargeDateRangeService.GetChargeDateRange(fromDate, toDate);
        }
        #endregion

        #region Get Charge Details
        /// <summary>
        /// Get Charge Details
        /// </summary>
        /// <param name="chargeYearId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChargeDetails/{chargeYearId}")]
        public ActionResult<IEnumerable<clsChargeMaster>> GetChargeDetails(int chargeYearId)
        {
            return _chargeMasterService.GetChargeDetails(chargeYearId);
        }
        #endregion

        #region Get Appointment List
        /// <summary>
        /// Get Appointment List: Create Charge(statusId = 1), Create Claim(statusId = 2), Resubmit Claim(statusId = 3), Manage Claim(statusId = 4), EOB(statusId = 5)
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="providerId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAppointmentList/{fromDate}/{toDate}/{patientName}/{feeTicket}/{providerId}/{statusId}")]
        public ActionResult<IEnumerable<clsClaimHeader>> GetAppointmentList(string fromDate = "", string toDate = "",string patientName="",string feeTicket="", string providerId = "0", string statusId = "1")
        {
            fromDate = fromDate.Replace("{", "").Replace("}", "").Trim();
            toDate = toDate.Replace("{", "").Replace("}", "").Trim();
            patientName = patientName.Replace("{", "").Replace("}", "").Trim();
            feeTicket = feeTicket.Replace("{", "").Replace("}", "").Trim().Replace('-','/');
            providerId = providerId.Replace("{", "").Replace("}", "").Trim();
            statusId = statusId.Replace("{", "").Replace("}", "").Trim();
            return _claimService.GetAppointmentList(fromDate, toDate, providerId, statusId, patientName, feeTicket);
        }
        #endregion

        #region Get Charge Patient Header Information
        /// <summary>
        /// Get Charge Patient Header Information: Used for Create Charge, Create Claim, Resubmit Claim, Manage Claim, EOB
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChargePatientHeader/{appointmentId}")]
        public ActionResult<clsClaimHeader> GetChargePatientHeader(int appointmentId)
        {
            return _claimService.GetChargePatientHeader(appointmentId);
        }
        #endregion

        #region Get Charge Patient Details Information
        /// <summary>
        /// Get Charge Patient Details Information: Used for Create Charge, Create Claim, Resubmit Claim, Manage Claim, EOB
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChargePatientDetails/{appointmentId}")]
        public ActionResult<IEnumerable<clsClaimDetails>> GetChargePatientDetails(int appointmentId)
        {
            return _claimService.GetChargePatientDetails(appointmentId);
        }
        #endregion

        #region Get Charge Patient Adjustment Information
        /// <summary>
        /// Get Charge Patient Adjustment Information : Used For EOB
        /// </summary>
        /// <param name="chargeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChargePatientAdjustment/{chargeId}")]
        public ActionResult<IEnumerable<clsClaimAdjustment>> GetChargePatientAdjustment(int chargeId)
        {
            return _claimService.GetChargePatientAdjustment(chargeId);
        }
        #endregion

        #region Get Payment Details
        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentDetails/{paymentId}/{patientId}")]
        public ActionResult<clsPayment> GetPaymentDetails(string paymentId, string patientId)
        {
            return _paymentService.GetPaymentDetails(paymentId, patientId);
        }
        #endregion


        #region Get Payment BreakUp
        /// <summary>
        /// Get Payment Details
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentBreakUp/{paymentId}/{patientId}")]
        public ActionResult<List<clsPaymentBreakup>> GetPaymentBreakUp(string paymentId, string patientId)
        {
            return _paymentService.GetPaymentBreakUp(paymentId, patientId);
        }
        #endregion

        #region Get Payment List
        /// <summary>
        /// Get Payment List
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentList/{fromDate}/{toDate}/{patientName}")]
        public ActionResult<IEnumerable<clsPaymentDetails>> GetPaymentList(string fromDate = "", string toDate = "", string patientName = "")
        {
            fromDate = fromDate.Replace("{", "").Replace("}", "").Trim();
            toDate = toDate.Replace("{", "").Replace("}", "").Trim();
            patientName = patientName.Replace("{", "").Replace("}", "").Trim();

            return _paymentService.GetPaymentList(fromDate, toDate, patientName);
        }
        #endregion

        #region Get CPT With ChargeAmount
        /// <summary>
        /// GetCPT: Get CPT With ChargeAmount.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsCPT>></returns>
        [HttpGet]
        [Route("GetCPTWITHChargeAmount")]
        public ActionResult<IEnumerable<clsCPT>> GetCPTWITHChargeAmount()
        {
            return _cptService.GetCPTWITHChargeAmount();
        }
        #endregion

        #region Get Claim Fields Header
        /// <summary>
        /// GetClaimFieldsHeader: Get ClaimFieldsHeader.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsClaimFieldsHeader>></returns>
        [HttpGet]
        [Route("GetClaimFieldsHeader")]
        public ActionResult<IEnumerable<clsClaimFieldsHeader>> GetClaimFieldsHeader()
        {
            return _claimFieldsService.GetClaimFieldsHeader();
        }
        #endregion

        #region Get Claim Fields Details
        /// <summary>
        /// ClaimFieldsDetails: Get ClaimFieldsDetails.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsClaimFieldsDetails>></returns>
        [HttpGet]
        [Route("GetClaimFieldsDetails")]
        public ActionResult<IEnumerable<clsClaimFieldsDetails>> GetClaimFieldsDetails(string id)
        {
            return _claimFieldsService.GetClaimFieldsDetails(id);
        }

        [HttpGet]
        [Route("GetClaimFieldsMaster")]
        public ActionResult<IEnumerable<clsClaimFieldsDetails>> GetClaimFieldsDetails(int id)
        {
            return _claimFieldsService.GetClaimFieldsMasterDetails(id);
        }

        [HttpGet]
        [Route("GetUserAccessDetails")]
        public ActionResult<IEnumerable<UserAccessDetailsDTO>> GetUserAccessDetails(int userTypeId)
        {
            return _userAccessService.GetUserAccessDetailsByUserType(userTypeId);
        }

        [HttpGet]
        [Route("GetDUOSignatureRequest")]
        public ActionResult<string> GetDUOSignatureRequest(string userName)
        {
            return _userAccessService.GetUserSignatureRequest(userName);
        }

        [HttpGet]
        [Route("GetDUOResponseUserName")]
        public ActionResult<string> GetDUOResponseUserName(string sig_response)
        {
            return _userAccessService.GetResponseStatus(sig_response);
        }
        #endregion

        #region Get Insurance Claim Fields
        /// <summary>
        /// InsuranceClaimFields: Get InsuranceClaimFields.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsPatientChargeClaimFields>></returns>
        [HttpGet]
        [Route("GetInsuranceClaimFields")]
        public ActionResult<IEnumerable<clsPatientChargeClaimFields>> GetInsuranceClaimFields(string chargeId)
        {
            return _claimFieldsService.GetInsuranceClaimFields(chargeId);
        }
        #endregion


        #region Get Claim Status
        /// <summary>
        /// ClaimStatus: Get ClaimStatus.
        /// </summary>
        /// <returns>ActionResult<IEnumerable<clsPatientChargeClaimFields>></returns>
        [HttpGet]
        [Route("GetClaimStatus")]
        public ActionResult<IEnumerable<clsClaimStatus>> GetClaimStatus()
        {
            return _claimStatusService.GetClaimStatus();
        }
        #endregion

        //Added by Suvresh
        [HttpGet]
        [Route("GetInusranceStatus/{patientId}")]
        public string getInsuranceStatus(string patientId)
        {
            return _flowSheetService.getInsurenaceStatus(patientId);
        }

        #region Validate Duplicate Patient
        /// <summary>
        /// Validate Duplicate Patient
        /// </summary>
        /// <param name="patientValidation"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ValidatePatient")]
        public string validatePatient([FromQuery]clsPatientValidation patientValidation)
        {
            return _patientInformationService.ValidatePatient(patientValidation.patientId, patientValidation.patientfName, patientValidation.patientmName, patientValidation.patientlName, "", "", patientValidation.dob, patientValidation.ssn, patientValidation.gender);
        }
        #endregion

        #region Get Vitals History
        [HttpGet]
        [Route("PatientVitalsHistory")]
        public ActionResult<IEnumerable<clsObservation>> PatientVitalsHistory(string patientId)
        {
            return _observationService.GetVitalsList(patientId);
        }
        #endregion

        #endregion

        #region 3rd Phase
        [HttpGet]
        [Route("GetClaimDetails")]
        public bool GetClaimDetails()
        {
            return _claimDetailsService.GetClaimDetails();
        }
        #endregion
        #endregion

        #region PUT Api

        #region Update CheckOut Status
        /// <summary>
        /// UpdateCheckOutStatus: Update CheckOut Status.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="reasonCode">string</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("UpdateCheckOutStatus/{AppointmentID}/{ReasonCode}")]
        public string UpdateCheckOutStatus(string appointmentId, string reasonCode)
        {
            return _checkOutService.UpdateCheckOut(appointmentId, reasonCode);
        }
        #endregion

        #region Update Mark Ready
        /// <summary>
        /// UpdateMarkReady: Update Mark Ready.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="flag">string</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("UpdateMarkReady/{AppointmentID}/{Flag}")]
        public string UpdateMarkReady(string appointmentId, string flag)
        {
            return _scheduleAppointmentService.UpdateMarkReady(appointmentId, flag);
        }
        #endregion

        #region Update Flow Area
        /// <summary>
        /// UpdateFloorArea: Update Flow Area.
        /// </summary>
        /// <param name="appointmentId">string</param>
        /// <param name="roomNo">string</param>
        /// <param name="flowArea">string</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("UpdateFloorArea/{AppointmentID}/{RoomNO}/{Flowarea}")]
        public string UpdateFlowArea(string appointmentId, string roomNo, string flowArea)
        {
            return _flowAreaService.UpdateFlowArea(appointmentId, roomNo, flowArea);
        }
        #endregion

        #endregion

        #region POST Api
        #region 1st Phase
        #region Save CoPay
        /// <summary>
        /// SaveCoPay: Save CoPay.
        /// </summary>
        /// <param name="coPay">clsCOPay</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveCoPay")]
        public string SaveCoPay([FromBody] clsCOPay coPay)
        {
            return _copayService.SaveCoPay(coPay);
        }
        #endregion

        #region Save Calendar
        /// <summary>
        /// SaveCalender: Save Calendar.
        /// </summary>
        /// <param name="calender">clsCalender</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveCalender")]
        public string SaveCalender([FromBody] clsCalender calender)
        {
            return _calendarService.SaveCalender(calender);
        }
        #endregion

        #region Save Appointment Details
        /// <summary>
        /// SaveAppointment: Save Appointment Details.
        /// </summary>
        /// <param name="appointment">clsAppointment</param>
        /// <returns>ActionResult<string></returns>
        [HttpPost]
        [Route("SaveAppointment")]
        public ActionResult<string> SaveAppointment([FromBody] clsAppointment appointment)
        {
            return _appointmentService.SaveActiveAppointment(appointment);
        }

        /// <summary>
        /// SaveAppointmentNew: Save Appointment
        /// </summary>
        /// <param name="data">IFormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveAppointmentNew")]
        public async Task<string> SaveAppointmentNew(IFormCollection data)
        {
            clsAppointment appointment = JsonConvert.DeserializeObject<clsAppointment>(data["appointmentDetails"]);
            appointment.fName = _sharedService.Encrypt(appointment.fName.ToUpper());
            appointment.lName = _sharedService.Encrypt(appointment.lName.ToUpper());
            appointment.mName = _sharedService.Encrypt(appointment.mName.ToUpper());
            appointment.patientName = _sharedService.Encrypt(appointment.patientName.ToUpper());
            appointment.contactNo = _sharedService.Encrypt(appointment.contactNo);
            appointment.address = _sharedService.Encrypt(appointment.address);
            appointment.gender = _sharedService.Encrypt(appointment.gender);
            appointment.email = _sharedService.Encrypt(appointment.email);
            appointment = await _fileControlService.SaveAppointmentImage(appointment, data.Files);
            return _appointmentService.SaveActiveAppointment(appointment);
        }
        #endregion

        #region Save Chief Complaint
        /// <summary>
        /// SaveChiefComplaint: Save Chief Complaint.
        /// </summary>
        /// <param name="cc">clsCC</param>
        /// <returns>ActionResult<string> </returns>
        [HttpPost]
        [Route("SaveChiefComplaint")]
        public ActionResult<string> SaveChiefComplaint([FromBody] clsCC cc)
        {
            cc.pccDescription = _sharedService.Encrypt(cc.pccDescription);
            return _chiefComplaintService.SaveChiefComplaint(cc);
        }
        #endregion

        #region Save Impression Plan
        /// <summary>
        /// SaveImpressionPlan: Save Impression Plan.
        /// </summary>
        /// <param name="impression">clsImpression</param>
        /// <returns>ActionResult<string></returns>
        [HttpPost]
        [Route("SaveImpressionPlan")]
        public ActionResult<string> SaveImpressionPlan([FromBody] clsImpression impression)
        {
            impression.description = _sharedService.Encrypt(impression.description);
            return _impressionPlanService.SaveImpressionPlan(impression);
        }
        #endregion

        #region Save User
        /// <summary>
        /// SaveUser: Save User.
        /// </summary>
        /// <param name="iUser">clsUserObj</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveUser")]
        public string SaveUser([FromBody] clsUserObj iUser)
        {
            string xmlUser = null;
            DataTable dt = new DataTable();

            dt = _sharedService.SingleObjToDataTable<clsUserObj>(iUser);
            xmlUser = _sharedService.ConvertDatatableToXML(dt);
            return _userService.SaveUser(iUser.id, xmlUser, iUser.fullName, iUser.email);
        }

        /// <summary>
        /// SaveUserNew: Save user.
        /// </summary>
        /// <param name="data">IFormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveUserNew")]
        public async Task<string> SaveUserNew(IFormCollection data)
        {
            string xmlUser = null;
            DataTable dt = new DataTable();

            clsUserObj iUser = JsonConvert.DeserializeObject<clsUserObj>(data["userDetails"]);
            iUser = await _fileControlService.SaveUserImage(iUser, data.Files);
            dt = _sharedService.SingleObjToDataTable<clsUserObj>(iUser);
            xmlUser = _sharedService.ConvertDatatableToXMLNew(dt);
            return _userService.SaveUser(iUser.id, xmlUser, iUser.fullName,iUser.email);
        }
        #endregion

        #region Save ICD
        /// <summary>
        /// SaveICD: Save ICD.
        /// </summary>
        /// <param name="icd">clsICD[]</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveICD")]
        public string SaveICD([FromBody] clsICD[] icd)
        {
            string xmlICD = null;
            DataTable dt = new DataTable();

            dt = _sharedService.ObjArrayToDataTable<clsICD>(icd);
            xmlICD = _sharedService.ConvertDatatableToXML(dt);

            return _icdService.SaveICD(Convert.ToInt32(icd[0].patientId), xmlICD);
        }
        #endregion

        #region Save Observation
        /// <summary>
        /// SaveObservation: Save Observation.
        /// </summary>
        /// <param name="objObservation">clsObservation</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveObservation")]
        public string SaveObservation([FromBody] clsObservation objObservation)
        {
            objObservation.pBloodPressureL = _sharedService.Encrypt(objObservation.pBloodPressureL);
            objObservation.pBloodPressureR = _sharedService.Encrypt(objObservation.pBloodPressureR);
            objObservation.pTemperature = _sharedService.Encrypt(objObservation.pTemperature);
            objObservation.pHeightL = _sharedService.Encrypt(objObservation.pHeightL);
            objObservation.pHeightR = _sharedService.Encrypt(objObservation.pHeightR);
            objObservation.pWeight = _sharedService.Encrypt(objObservation.pWeight);
            objObservation.pPulse = _sharedService.Encrypt(objObservation.pPulse);
            objObservation.pRespiratory = _sharedService.Encrypt(objObservation.pRespiratory);
            return _observationService.SaveObservation(objObservation);
        }
        #endregion

        #region Save Allergies, Immunizations, Alert, Social and Family History
        /// <summary>
        /// SaveAllergyImmunization: Save Allergies, Immunizations, Alert, Social and Family History.
        /// </summary>
        /// <param name="clsAllergyImmunization">clsAllergyImmunization</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveAllergyImmunization")]
        public string SaveAllergyImmunization([FromBody] clsAllergyImmunization clsAllergyImmunization)
        {
            string xmlAllergy = null;
            string xmlImmunization = null;
            string xmlAlert = null;
            string xmlFamily = null;
            string xmlSocial = null;

            DataTable dtAlert = new DataTable();
            DataTable dtAllergy = new DataTable();
            DataTable dtImmunization = new DataTable();
            DataTable dtFamily = new DataTable();
            DataTable dtSocial = new DataTable();

            dtAllergy = _sharedService.ObjArrayToDataTable<clsAllergies>(clsAllergyImmunization.allergies);
            xmlAllergy = _sharedService.ConvertDatatableToXML(dtAllergy);

            dtImmunization = _sharedService.ObjArrayToDataTable<clsImmunizations>(clsAllergyImmunization.immunizations);
            xmlImmunization = _sharedService.ConvertDatatableToXML(dtImmunization);

            dtAlert = _sharedService.SingleObjToDataTable<clsAlert>(clsAllergyImmunization.alert);
            xmlAlert = _sharedService.ConvertDatatableToXML(dtAlert);

            dtFamily = _sharedService.ObjArrayToDataTable<clsFamilies>(clsAllergyImmunization.families);
            xmlFamily = _sharedService.ConvertDatatableToXML(dtFamily);

            dtSocial = _sharedService.ObjArrayToDataTable<clsSocials>(clsAllergyImmunization.socials);
            xmlSocial = _sharedService.ConvertDatatableToXML(dtSocial);

            return _allergyImmunizationAlertSocialFamilyService.SaveAllergyImmunization(clsAllergyImmunization.patientId, xmlAllergy, xmlImmunization, xmlAlert, xmlFamily, xmlSocial);
        }
        #endregion

        #region Save Patient Details
        /// <summary>
        /// CreatePatient: Save Patient Details.
        /// </summary>
        /// <param name="ptn">clsCreateUpdatePatient</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("CreatePatient")]
        public string CreatePatient([FromBody] clsCreateUpdatePatient ptn)
        {
            string @PatientID = "", @p_Details = "", @p_Billing = "", @p_EmergencyContact = "", @p_EmployerContact = "";
            string @p_Insurance = "", @p_Social = "", @p_Authorization = "";

            DataTable dt = new DataTable();
            @PatientID = ptn.sPatientDetails.patientId;

            dt = _sharedService.SingleObjToDataTable<clsPatientDetails>(ptn.sPatientDetails);
            @p_Details = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientBilling>(ptn.sPatientBilling);
            @p_Billing = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientEmergencyContact>(ptn.sPatientEmergency);
            @p_EmergencyContact = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientEmployerContact>(ptn.sPatientEmpContact);
            @p_EmployerContact = _sharedService.ConvertDatatableToXML(dt);

            //dt = _sharedService.ObjArrayToDataTable<clsPatientInsurance>(Ptn.sPatientInsurance);
            //@p_Insurance = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientSocial>(ptn.sPatientSocial);
            @p_Social = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientAuthorization>(ptn.sPatientAuthorisation);
            @p_Authorization = _sharedService.ConvertDatatableToXML(dt);

            return _patientInformationService.CreatePatient(PatientID, p_Details, p_Billing, p_EmergencyContact, p_EmployerContact, p_EmployerContact, p_Insurance, p_Authorization);
        }

        /// <summary>
        /// CreatePatientNew
        /// </summary>
        /// <returns>string</returns>
        [HttpPost]
        [Route("CreatePatientNew")]
        public async Task<string>  CreatePatientNew()
        {

            clsCreateUpdatePatient objPatient = JsonConvert.DeserializeObject<clsCreateUpdatePatient>(Request.Form["createUpdatePatient"]);
            #region Patient Details 
            objPatient.sPatientDetails.firstName = _sharedService.Encrypt(objPatient.sPatientDetails.firstName.ToUpper());
            objPatient.sPatientDetails.middleName = _sharedService.Encrypt(objPatient.sPatientDetails.middleName.ToUpper());
            objPatient.sPatientDetails.lastName = _sharedService.Encrypt(objPatient.sPatientDetails.lastName.ToUpper());
            objPatient.sPatientDetails.addressLine = _sharedService.Encrypt(objPatient.sPatientDetails.addressLine);
            objPatient.sPatientDetails.addressLine1 = _sharedService.Encrypt(objPatient.sPatientDetails.addressLine1);
            objPatient.sPatientDetails.addressCity = _sharedService.Encrypt(objPatient.sPatientDetails.addressCity);
            objPatient.sPatientDetails.addressState = _sharedService.Encrypt(objPatient.sPatientDetails.addressState);
            objPatient.sPatientDetails.addressPostalCode = _sharedService.Encrypt(objPatient.sPatientDetails.addressPostalCode);
            objPatient.sPatientDetails.addressCountry = _sharedService.Encrypt(objPatient.sPatientDetails.addressCountry);
            objPatient.sPatientDetails.age = objPatient.sPatientDetails.age;
            objPatient.sPatientDetails.gender = _sharedService.Encrypt(objPatient.sPatientDetails.gender);
            objPatient.sPatientDetails.email = _sharedService.Encrypt(objPatient.sPatientDetails.email);
            objPatient.sPatientDetails.mobNo = _sharedService.Encrypt(objPatient.sPatientDetails.mobNo);
            objPatient.sPatientDetails.primaryPhone = _sharedService.Encrypt(objPatient.sPatientDetails.primaryPhone);
            objPatient.sPatientDetails.secondaryPhone = _sharedService.Encrypt(objPatient.sPatientDetails.secondaryPhone);
            #endregion

            #region Billing Details
            objPatient.sPatientBilling.firstName=_sharedService.Encrypt(objPatient.sPatientBilling.firstName.ToUpper());
            objPatient.sPatientBilling.middleName = _sharedService.Encrypt(objPatient.sPatientBilling.middleName.ToUpper());
            objPatient.sPatientBilling.lastName = _sharedService.Encrypt(objPatient.sPatientBilling.lastName.ToUpper());
            objPatient.sPatientBilling.addLine = _sharedService.Encrypt(objPatient.sPatientBilling.addLine);
            objPatient.sPatientBilling.addLine1 = _sharedService.Encrypt(objPatient.sPatientBilling.addLine1);
            objPatient.sPatientBilling.addCity = _sharedService.Encrypt(objPatient.sPatientBilling.addCity);
            objPatient.sPatientBilling.addState = _sharedService.Encrypt(objPatient.sPatientBilling.addState);
            objPatient.sPatientBilling.addZip = _sharedService.Encrypt(objPatient.sPatientBilling.addZip);
            objPatient.sPatientBilling.SSN = _sharedService.Encrypt(objPatient.sPatientBilling.SSN);
            objPatient.sPatientBilling.primaryPhone = _sharedService.Encrypt(objPatient.sPatientBilling.primaryPhone);
            objPatient.sPatientBilling.secondaryPhone = _sharedService.Encrypt(objPatient.sPatientBilling.secondaryPhone);
            #endregion

            #region Emergency Details
            objPatient.sPatientEmergency.contactName = _sharedService.Encrypt(objPatient.sPatientEmergency.contactName);
            objPatient.sPatientEmergency.contactPhone = _sharedService.Encrypt(objPatient.sPatientEmergency.contactPhone);
            //objPatient.sPatientEmergency.relationship = _sharedService.Encrypt(objPatient.sPatientEmergency.relationship);
            #endregion

            #region Employer Contact
            objPatient.sPatientEmpContact.name = _sharedService.Encrypt(objPatient.sPatientEmpContact.name);
            objPatient.sPatientEmpContact.phone = _sharedService.Encrypt(objPatient.sPatientEmpContact.phone);
            objPatient.sPatientEmpContact.address = _sharedService.Encrypt(objPatient.sPatientEmpContact.address);
            #endregion

            #region Insurance
            foreach (clsPatientInsurance item in objPatient.sPatientInsurance)
            {
                item.insurancePolicy= _sharedService.Encrypt(item.insurancePolicy);
                

            }
            #endregion

            #region Social
            objPatient.sPatientSocial.maritalStatus = _sharedService.Encrypt(objPatient.sPatientSocial.maritalStatus);
            objPatient.sPatientSocial.guardianFName = _sharedService.Encrypt(objPatient.sPatientSocial.guardianFName);
            objPatient.sPatientSocial.guardianLName = _sharedService.Encrypt(objPatient.sPatientSocial.guardianLName);
            objPatient.sPatientSocial.addLine = _sharedService.Encrypt(objPatient.sPatientSocial.addLine);
            objPatient.sPatientSocial.addCity = _sharedService.Encrypt(objPatient.sPatientSocial.addCity);
            objPatient.sPatientSocial.addState = _sharedService.Encrypt(objPatient.sPatientSocial.addState);
            objPatient.sPatientSocial.addZip     = _sharedService.Encrypt(objPatient.sPatientSocial.addZip);
            objPatient.sPatientSocial.guardianSSN = _sharedService.Encrypt(objPatient.sPatientSocial.guardianSSN);
            objPatient.sPatientSocial.patientSSN = _sharedService.Encrypt(objPatient.sPatientSocial.patientSSN);
            objPatient.sPatientSocial.phoneNumber = _sharedService.Encrypt(objPatient.sPatientSocial.phoneNumber);
            #endregion

            objPatient.sPatientDetails = await _fileControlService.SavePatientProfileImage(objPatient.sPatientDetails, Request.Form.Files["profile"]);
            objPatient.sPatientSocial = await _fileControlService.SaveSocialImage(objPatient.sPatientSocial, Request.Form.Files["social"]);
            objPatient.sPatientBilling = await  _fileControlService.SaveBillingImage(objPatient.sPatientBilling, Request.Form.Files["billing"]);
            objPatient.sPatientAuthorisation = await _fileControlService.SaveAuthorizationImage(objPatient.sPatientAuthorisation, Request.Form.Files["authorization"]);
            int index = 0;
            foreach (clsPatientInsurance item in objPatient.sPatientInsurance)
            {
                index++;

                await _fileControlService.SaveInsuranceImage(item, Request.Form.Files["insurances_" + index]);
            }
            // other Info
            clsAllergyImmunization objallergyImmunization = JsonConvert.DeserializeObject<clsAllergyImmunization>(Request.Form["allergyImmunization"]);
            // other Info
            string @PatientID = "", @p_Details = "", @p_Billing = "", @p_EmergencyContact = "", @p_EmployerContact = "";
            string @p_Insurance = "", @p_Social = "", @p_Authorization = "";

            DataTable dt = new DataTable();
            @PatientID = objPatient.sPatientDetails.patientId;

            dt = _sharedService.SingleObjToDataTable<clsPatientDetails>(objPatient.sPatientDetails);
            @p_Details = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientBilling>(objPatient.sPatientBilling);
            @p_Billing = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientEmergencyContact>(objPatient.sPatientEmergency);
            @p_EmergencyContact = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientEmployerContact>(objPatient.sPatientEmpContact);
            @p_EmployerContact = _sharedService.ConvertDatatableToXML(dt);

            clsPatientInsurance[] Insurancearray = objPatient.sPatientInsurance.ToArray();
            dt = _sharedService.ObjArrayToDataTable<clsPatientInsurance>(Insurancearray);
            @p_Insurance = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientSocial>(objPatient.sPatientSocial);
            @p_Social = _sharedService.ConvertDatatableToXML(dt);

            dt = _sharedService.SingleObjToDataTable<clsPatientAuthorization>(objPatient.sPatientAuthorisation);
            @p_Authorization = _sharedService.ConvertDatatableToXML(dt);

        var result=     _patientInformationService.CreatePatient(PatientID, p_Details, p_Billing, p_EmergencyContact, p_EmployerContact, p_Insurance, @p_Social, p_Authorization);

            if (result.Length == 36)
            {
                // other info
                objallergyImmunization.patientId = result;
                string xmlAllergy = null;
                string xmlImmunization = null;
                string xmlAlert = null;
                string xmlFamily = null;
                string xmlSocial = null;

                DataTable dtAlert = new DataTable();
                DataTable dtAllergy = new DataTable();
                DataTable dtImmunization = new DataTable();
                DataTable dtFamily = new DataTable();
                DataTable dtSocial = new DataTable();

                dtAllergy = _sharedService.ObjArrayToDataTable<clsAllergies>(objallergyImmunization.allergies);
                if(dtAllergy!=null)
                    xmlAllergy = _sharedService.ConvertDatatableToXML(dtAllergy);
                else
                {
                    xmlAllergy = "";
                }

                dtImmunization = _sharedService.ObjArrayToDataTable<clsImmunizations>(objallergyImmunization.immunizations);
                xmlImmunization = _sharedService.ConvertDatatableToXML(dtImmunization);

                dtAlert = _sharedService.SingleObjToDataTable<clsAlert>(objallergyImmunization.alert);
                xmlAlert = _sharedService.ConvertDatatableToXML(dtAlert);

                dtFamily = _sharedService.ObjArrayToDataTable<clsFamilies>(objallergyImmunization.families);
                xmlFamily = _sharedService.ConvertDatatableToXML(dtFamily);

                dtSocial = _sharedService.ObjArrayToDataTable<clsSocials>(objallergyImmunization.socials);
                if (dtSocial != null)
                {
                    xmlSocial = _sharedService.ConvertDatatableToXML(dtSocial);
                }
                else
                {
                    xmlSocial = "";
                }
                _allergyImmunizationAlertSocialFamilyService.SaveAllergyImmunization(objallergyImmunization.patientId, xmlAllergy, xmlImmunization, xmlAlert, xmlFamily, xmlSocial);
            }
            // other info
            return result +'~'+ objPatient.sPatientDetails.imagePath;
        }


        #endregion

        #region Save CPT
        /// <summary>
        /// SaveCPT: Save CPT.
        /// </summary>
        /// <param name="cpt">clsCPT[]</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveCPT")]
        public string SaveCPT([FromBody] clsCPT[] cpt)
        {
            string xmlCPT = null;
            DataTable dtCPT = new DataTable();

            dtCPT = _sharedService.ObjArrayToDataTable<clsCPT>(cpt);
            xmlCPT = _sharedService.ConvertDatatableToXML(dtCPT);
            return _cptService.SaveCPT(cpt[0].patientId, xmlCPT);
        }
        #endregion

        #region Save Followup
        /// <summary>
        /// SaveFollowup: Save Followup.
        /// </summary>
        /// <param name="objFollowup">clsFollowUp</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveFollowup")]
        public string SaveFollowup([FromBody] clsFollowUp objFollowup)
        {
            return _followupService.SaveFollowup(objFollowup);
        }
        #endregion

        #region Save Encounter
        /// <summary>
        /// SaveEncounter: Save Encounter.
        /// </summary>
        /// <param name="encounter">clsEncounter</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveEncounter")]
        public string SaveEncounter([FromBody] clsEncounter encounter)
        {
            encounter.encounterNote = _sharedService.Encrypt(encounter.encounterNote);
            encounter.summary = _sharedService.Encrypt(encounter.summary);
            return _encounterService.SaveEncounter(encounter);
        }
        #endregion

        #region  Save Others Details
        /// <summary>
        /// SaveOthersDetails: Save Others Details .
        /// </summary>
        /// <param name="sOthers">clsOtherPopulate</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveOthersDetails")]
        public string SaveOthersDetails([FromBody] clsOtherPopulate sOthers)
        {
            return _othersPopulateService.SaveOthersDetails(sOthers);
        }
        #endregion

        #region Delete Others Details
        /// <summary>
        /// DeleteOthersDetails: Delete Others Details.
        /// </summary>
        /// <param name="sOthers">clsOtherPopulate</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteOthersDetails")]
        public string DeleteOthersDetails([FromBody] clsOtherPopulate sOthers)
        {
            return _othersPopulateService.DeleteOthersDetails(sOthers);
        }
        #endregion

        #region Delete Reason
        /// <summary>
        /// DeleteReason: Delete Reason.
        /// </summary>
        /// <param name="sReason">clsReason</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteReason")]
        public string DeleteReason([FromBody] clsReason sReason)
        {
            return _reasonService.DeleteReason(sReason);
        }
        #endregion

        #region Save Reason
        /// <summary>
        /// SaveReason: Save Reason.
        /// </summary>
        /// <param name="sReason">clsReason</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveReason")]
        public string SaveReason([FromBody] clsReason sReason)
        {
            return _reasonService.SaveReason(sReason);
        }
        #endregion

        #region Delete Role
        /// <summary>
        /// DeleteRole: Delete Role.
        /// </summary>
        /// <param name="sReason">clsUType</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteRole")]
        public string DeleteRole([FromBody] clsUType uType)
        {
            return _userTypeService.DeleteRole(uType);
        }
        #endregion

        #region Save Role
        /// <summary>
        /// SaveRole: Save Role.
        /// </summary>
        /// <param name="sReason">clsUType</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveRole")]
        public string SaveRole([FromBody] clsUType uType)
        {
            return _userTypeService.SaveRole(uType);
        }
        #endregion


        #region Save Service Details
        /// <summary>
        /// SaveServiceDetails: Save Service Details.
        /// </summary>
        /// <param name="objService">clsService</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveServiceDetails")]
        public string SaveServiceDetails([FromBody] clsService objService)
        {
            return _serviceDetailsService.SaveServiceDetails(objService);
        }
        #endregion

        #region Delete Service Details
        /// <summary>
        /// DeleteServiceDetails: Delete Service Details.
        /// </summary>
        /// <param name="sservice">clsService</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteServiceDetails")]
        public string DeleteServiceDetails([FromBody] clsService objService)
        {
            return _serviceDetailsService.DeleteServiceDetails(objService);
        }
        #endregion

        #region Save DEPARTMENT
        /// <summary>
        /// SaveDEPARTMENT: Save DEPARTMENT Details.
        /// </summary>
        /// <param name="objDEPARTMENT">clsDept</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveDepartment")]
        public string SaveDepartment([FromBody] clsDept objdept)
        {
            return _departmentService.SaveDepartment(objdept);
        }
        #endregion

        #region Delete DEPARTMENT
        /// <summary>
        /// DeleteDEPARTMENT: Delete DEPARTMENT Details.
        /// </summary>
        /// <param name="sservice">clsDept</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteDepartment")]
        public string DeleteDepartment([FromBody] clsDept objdept)
        {
            return _departmentService.DeleteSAVEDEPARTMENT(objdept);
        }
        #endregion

        #region Delete Calendar
        /// <summary>
        /// DeleteCalender: Delete Calendar.
        /// </summary>
        /// <param name="ID">string</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("DeleteCalender/{ID}")]
        public string DeleteCalender(string id)
        {
            return _calendarService.DeleteCalender(id);
        }
        #endregion

        #region Save Procedure Details
        /// <summary>
        /// SaveProcedure: Save Procedure Details.
        /// </summary>
        /// <param name="data">IFormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveProcedure")]
        public string SaveProcedure(IFormCollection data)
        {
            DataSet dsDocList = new DataSet();
            DataTable dtProc = new DataTable();
            string xmlProcedures = null;
            string xmlDoc = null;

            clsProcedures procDocument = JsonConvert.DeserializeObject<clsProcedures>(data["ProcUploadDetails"]);
            string appId = JsonConvert.DeserializeObject<string>(data["AppointmentId"]);
            procDocument = _procedureService.GetDoctorProfile(appId, procDocument);
            List<clsDocument> docList = new List<clsDocument>();

            IFormFileCollection UploadFiles = data.Files;

            docList = _fileControlService.SaveProcedureImage(procDocument.patientId, UploadFiles);
            dsDocList = _sharedService.ObjListToDataTable<clsDocument>(docList);
            dtProc = _sharedService.SingleObjToDataTable<clsProcedures>(procDocument);

            xmlProcedures = _sharedService.ConvertDatatableToXML(dtProc);
            foreach (DataTable table in dsDocList.Tables)
            {
                xmlDoc += _sharedService.ConvertDatatableToXMLNew(table);
            }
            return _procedureService.SaveProcedure(xmlDoc, xmlProcedures, procDocument.patientId);
        }
        #endregion

        #region Save Diagnosis Details
        /// <summary>
        /// SaveDiagnosis: Save Diagnosis Details.
        /// </summary>
        /// <param name="data">IFormCollection</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("SaveDiagnosis")]
        public string SaveDiagnosis(IFormCollection data)
        {
            DataSet dsDocList = new DataSet();
            DataTable dtDiag = new DataTable();
            string xmlDiagnosis = null;
            string xmlDoc = null;

            clsDiagnosis diagnosis = JsonConvert.DeserializeObject<clsDiagnosis>(data["DiagUploadDetails"]);
            List<clsDocument> docList = new List<clsDocument>();
            IFormFileCollection UploadFiles = data.Files;

            docList = _fileControlService.SaveDiagnosisImage(diagnosis.patientId, UploadFiles);
            dsDocList = _sharedService.ObjListToDataTable<clsDocument>(docList);
            dtDiag = _sharedService.SingleObjToDataTable<clsDiagnosis>(diagnosis);

            xmlDiagnosis = _sharedService.ConvertDatatableToXML(dtDiag);

            foreach (DataTable table in dsDocList.Tables)
            {
                xmlDoc += _sharedService.ConvertDatatableToXMLNew(table);
            }

            return _diagnosisService.SaveDiagnosis(xmlDoc, xmlDiagnosis, diagnosis.patientId);
        }
        #endregion
        #endregion

        #region 2nd Phase
        #region Save Charge Date Range
        /// <summary>
        /// Save Charge Date Range
        /// </summary>
        /// <param name="dateRange"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveChargeDateRange")]
        public string SaveChargeDateRange([FromBody] clsChargeDateRange dateRange)
        {  
                return _chargeDateRangeService.SaveChargeDateRange(dateRange);   
        }
        #endregion
        


        #region Save Charge Details
        /// <summary>
        /// Save Charge Details
        /// </summary>
        /// <param name="chargeMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveChargeDetails")]
        public string SaveChargeDetails([FromBody] clsChargeMaster[] chargeMaster)
        {
            int chargeYearId = chargeMaster[0].chargeYearId;
            DataTable dtChargeDetails = _sharedService.ObjArrayToDataTable<clsChargeMaster>(chargeMaster);
            string chargeDetailsXml = _sharedService.ConvertDatatableToXMLNew(dtChargeDetails);
            return _chargeMasterService.SaveChargeDetails(chargeYearId, chargeDetailsXml);
        }
        #endregion

        #region Save Claim
        /// <summary>
        /// Save Claim Records
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveClaim")]
        public string SaveClaim(IFormCollection data)
        {
            string xmlHeader = null;
            DataTable dtHeader = new DataTable();

            string xmlDetails = null;
            DataTable dtDetails = new DataTable();

            string xmlAdjustment = null;
            DataTable dtAdjustment = new DataTable();

            string isDelete = JsonConvert.DeserializeObject<string>(data["isDelete"]);

            int chargeId = JsonConvert.DeserializeObject<int>(data["chargeId"]);

            clsClaimHeader claimHeader = JsonConvert.DeserializeObject<clsClaimHeader>(data["claimHeader"]);
            if (claimHeader.policy1 !="" || claimHeader.policy1 != "-1")
            {
                claimHeader.policy1 = _sharedService.Encrypt(claimHeader.policy1);
            }
            if (claimHeader.policy2 != "" || claimHeader.policy2 != "-1")
            {
                claimHeader.policy2 = _sharedService.Encrypt(claimHeader.policy2);
            }
            if (claimHeader.policy3 != "" || claimHeader.policy3 != "-1")
            {
                claimHeader.policy3 = _sharedService.Encrypt(claimHeader.policy3);
            }
            claimHeader.statusId =Convert.ToBoolean(claimHeader.denied) ? 1 : 0;

            clsClaimDetails claimDetails = JsonConvert.DeserializeObject<clsClaimDetails>(data["claimDetails"]);
            if (data.ContainsKey("claimAdjustment"))
            {
                clsClaimAdjustment[] claimAdjustments = JsonConvert.DeserializeObject<clsClaimAdjustment[]>(data["claimAdjustment"]);
                dtAdjustment = _sharedService.ObjArrayToDataTable<clsClaimAdjustment>(claimAdjustments);
                if (dtAdjustment != null)
                    xmlAdjustment = _sharedService.ConvertDatatableToXMLNew(dtAdjustment);
                else
                    xmlAdjustment = "";
            }


            dtHeader = _sharedService.SingleObjToDataTable<clsClaimHeader>(claimHeader);
            dtDetails = _sharedService.SingleObjToDataTable<clsClaimDetails>(claimDetails);


            xmlHeader = _sharedService.ConvertDatatableToXMLNew(dtHeader);
            xmlDetails = _sharedService.ConvertDatatableToXMLNew(dtDetails);


            return _claimService.SaveClaim(chargeId, xmlHeader, xmlDetails, xmlAdjustment, isDelete);
        }
        #endregion

        #region Save Payment Details
        /// <summary>
        /// Save Payment Details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePaymentDetails")]
        public string SavePaymentDetails(IFormCollection data)
        {
            string xmlDetails = null;
            DataTable dtDetails = new DataTable();

            string xmlBreakup = null;
            DataTable dtBreakup = new DataTable();

            int paymentId = JsonConvert.DeserializeObject<int>(data["paymentId"]);
            string isdelete = JsonConvert.DeserializeObject<string>(data["isDeleted"]);
            clsPaymentDetails[] paymentDetails = JsonConvert.DeserializeObject<clsPaymentDetails[]>(data["paymentDetails"]);
            clsPaymentBreakup[] paymentBreakup = JsonConvert.DeserializeObject<clsPaymentBreakup[]>(data["paymentBreakup"]);

            dtDetails = _sharedService.ObjArrayToDataTable<clsPaymentDetails>(paymentDetails);
            dtBreakup = _sharedService.ObjArrayToDataTable<clsPaymentBreakup>(paymentBreakup);

            if (dtDetails != null)
                xmlDetails = _sharedService.ConvertDatatableToXMLNew(dtDetails);
            if (dtBreakup != null)
                xmlBreakup = _sharedService.ConvertDatatableToXMLNew(dtBreakup);

            return _paymentService.SavePaymentDetails(paymentId, xmlDetails, xmlBreakup, isdelete);
        }
        #endregion

        #region Save Patient Charge Claim Fields
        /// <summary>
        /// SavePatientChargeClaimFields
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePatientChargeClaimFields")]
        public string SavePatientChargeClaimFields(IFormCollection data)
        {
            string xlmPatientChargeClaimFields = null;
            DataTable dtPatientChargeClaimFields = new DataTable();





            int chargeId = JsonConvert.DeserializeObject<int>(data["chargeId"]);
            string typeem = JsonConvert.DeserializeObject<string>(data["typeem"]);

            clsPatientChargeClaimFields[] patientChargeClaimFields = JsonConvert.DeserializeObject<clsPatientChargeClaimFields []>(data["patientChargeClaimFields"]);



            dtPatientChargeClaimFields = _sharedService.ObjArrayToDataTable<clsPatientChargeClaimFields>(patientChargeClaimFields);



            xlmPatientChargeClaimFields = _sharedService.ConvertDatatableToXMLNew(dtPatientChargeClaimFields);

            return _claimFieldsService.SavePatientChargeClaimFields(chargeId, xlmPatientChargeClaimFields, typeem);
        }
        #endregion

        #region Save Claim Field Master
        /// <summary>
        /// Save Claim Records
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveClaimFieldMaster")]
        public string SaveClaimFieldMaster(IFormCollection data)
        {
            string xmlDetails = null;
            DataTable dtDetails = new DataTable();

            //string isDelete = JsonConvert.DeserializeObject<string>(data["isDelete"]);
            string name = JsonConvert.DeserializeObject<string>(data["name"]);
            int claimFieldId = JsonConvert.DeserializeObject<int>(data["id"]);
            char type = JsonConvert.DeserializeObject<char>(data["type"]);
            string isDeleted = "N";
            if (data.ContainsKey("isDeleted"))
            {
                isDeleted = JsonConvert.DeserializeObject<string>(data["isDeleted"]); 
            }

            if (data.ContainsKey("claimFieldDetails"))
            {
                clsClaimFieldsDetails[] claimAdjustments = JsonConvert.DeserializeObject<clsClaimFieldsDetails[]>(data["claimFieldDetails"]);
                dtDetails = _sharedService.ObjArrayToDataTable<clsClaimFieldsDetails>(claimAdjustments);
                xmlDetails = _sharedService.ConvertDatatableToXMLNew(dtDetails);
            }

            return _claimFieldsService.SaveClaimFieldMaster(claimFieldId, name, type, isDeleted, xmlDetails);
        }
        #endregion

        #endregion
        #endregion

        #region Report API

        #region Get Party Ledger
        /// <summary>
        /// Get Party Ledger
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPartyLedger/{fromDate}/{toDate}/{patientName}")]
        public ActionResult<IEnumerable<clsPartyLedger>> GetPartyLedger(string fromDate = "", string toDate = "", string patientName = "")
        {
            fromDate = fromDate.Replace("{", "").Replace("}", "").Trim();
            toDate = toDate.Replace("{", "").Replace("}", "").Trim();
            patientName = patientName.Replace("{", "").Replace("}", "").Trim();

            return _reportService.GetPartyLedger(fromDate, toDate, patientName);
        }
        #endregion

        #region Get Patient Balance
        /// <summary>
        /// Get Patient Balance
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPatientBalance/{patientName}")]
        public ActionResult<IEnumerable<clsPatientBalance>> GetPatientBalance(string patientName = "")
        {
           
            patientName = patientName.Replace("{", "").Replace("}", "").Trim();

            return _reportService.GetPatientBalance(patientName);
        }
        #endregion

        #endregion

        #region EncryptKEY
        /// <summary>
        /// SaveICD: Save ICD.
        /// </summary>
        /// <param name="icd">clsICD[]</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("EncryptKEY/{key}")]
        public string EncryptKEY(string key)
        {
            

            return _sharedService.EncryptKEY(key);
        }
        #endregion

        #region Data Encrypt
        /// <summary>
        /// SaveICD: Save ICD.
        /// </summary>
        /// <param name="icd">clsICD[]</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("DataEncrypt/{value}")]
        public string DataEncrypt(string value)
        {


            return _sharedService.Encrypt(value);
        }
        #endregion
        #region Data Decrypt
        /// <summary>
        /// SaveICD: Save ICD.
        /// </summary>
        /// <param name="icd">clsICD[]</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("DataDecrypt/{value}")]
        public string DataDecrypt(string value)
        {


            return _sharedService.Decrypt(value);
        }
        #endregion
    }
}