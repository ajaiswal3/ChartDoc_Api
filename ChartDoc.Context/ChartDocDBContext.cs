using ChartDoc.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChartDoc.Context
{
    public class ChartDocDBContext :  DbContext
    {
        #region Constructor************************************************************************************************************************************
        /// <summary>
        /// ChartDocDBContext : Constructor
        /// </summary>
        /// <param name="options">DbContextOptions<ChartDocDBContext> </param>
        public ChartDocDBContext(DbContextOptions<ChartDocDBContext> options): base(options){}
        #endregion

        #region Public Properties******************************************************************************************************************************
        /// <summary>
        /// M_USER : DbSet<clsUser> 
        /// </summary>
        public DbSet<clsUser> M_USER { get; set; }

        /// <summary>
        /// M_Doctors : DbSet<clsDoctor>
        /// </summary>
        public DbSet<clsDoctor> M_Doctors { get; set; }

        /// <summary>
        /// Vw_ICD : DbSet<clsICD>
        /// </summary>
        public DbSet<clsICD> Vw_ICD { get; set; }

        /// <summary>
        /// T_Diagnosis : DbSet<clsDiagnosis>
        /// </summary>
        public DbSet<clsDiagnosis> T_Diagnosis { get; set; }

        /// <summary>
        /// T_Procedure : DbSet<clsProcedures>
        /// </summary>
        public DbSet<clsProcedures> T_Procedure { get; set; }

        /// <summary>
        /// T_PatientDetails : DbSet<clsPatientDetails>
        /// </summary>
        public DbSet<clsPatientDetails> T_PatientDetails { get; set; }

        /// <summary>
        /// Vw_ChiefComplaint : DbSet<clsCC>
        /// </summary>
        public DbSet<clsCC> Vw_ChiefComplaint { get; set; }

        /// <summary>
        /// T_Appointment : DbSet<clsAppointment>
        /// </summary>
        public DbSet<clsAppointment> T_Appointment { get; set; }

        /// <summary>
        /// T_IMPRESSION : DbSet<clsImpression>
        /// </summary>
        public DbSet<clsImpression> T_IMPRESSION { get; set; }

        /// <summary>
        /// M_Department : DbSet<clsDept>
        /// </summary>
        public DbSet<clsDept> M_Department { get; set; }

        /// <summary>
        /// T_Patient_Insurance : DbSet<clsPatientInsurance> 
        /// </summary>
        public DbSet<clsPatientInsurance> T_Patient_Insurance { get; set; }

        /// <summary>
        /// T_OfficeCalander : DbSet<clsCalender>
        /// </summary>
        public DbSet<clsCalender> T_OfficeCalander { get; set; }

        /// <summary>
        /// M_REASON : DbSet<clsReason>
        /// </summary>
        public DbSet<clsReason> M_REASON { get; set; }

        /// <summary>
        /// Vw_COPay : DbSet<clsCOPay>
        /// </summary>
        public DbSet<clsCOPay> Vw_COPay { get; set; }

        /// <summary>
        /// T_ScheduleAppointment : DbSet<clsScheduleAppointment>
        /// </summary>
        public DbSet<clsScheduleAppointment> T_ScheduleAppointment { get; set; }

        /// <summary>
        /// Vw_CPT : DbSet<clsCPT>
        /// </summary>
        public DbSet<clsCPT> Vw_CPT { get; set; }

        /// <summary>
        /// Vw_Encounter_V2 : DbSet<clsEncounter>
        /// </summary>
        public DbSet<clsEncounter> Vw_Encounter_V2 { get; set; }

        /// <summary>
        /// tblMenuList1 : DbSet<clsRole>
        /// </summary>
        public DbSet<clsRole> tblMenuList1 { get; set; }

        /// <summary>
        /// M_USERTYPE : DbSet<clsUType>
        /// </summary>
        public DbSet<clsUType> M_USERTYPE { get; set; }

        /// <summary>
        /// M_OTHERSPOPULATE : DbSet<clsOtherPopulate>
        /// </summary>
        public DbSet<clsOtherPopulate> M_OTHERSPOPULATE { get; set; }

        /// <summary>
        /// T_Patient_Billing : DbSet<clsPatientBilling>
        /// </summary>
        public DbSet<clsPatientBilling> T_Patient_Billing { get; set; }

        /// <summary>
        /// T_Patient_Authorization : DbSet<clsPatientAuthorization>
        /// </summary>
        public DbSet<clsPatientAuthorization> T_Patient_Authorization { get; set; }

        /// <summary>
        /// T_Patient_EmergencyContact : DbSet<clsPatientEmergencyContact>
        /// </summary>
        public DbSet<clsPatientEmergencyContact> T_Patient_EmergencyContact { get; set; }

        /// <summary>
        /// T_Patient_EmployerContact : DbSet<clsPatientEmployerContact>
        /// </summary>
        public DbSet<clsPatientEmployerContact> T_Patient_EmployerContact { get; set; }

        /// <summary>
        /// T_Patient_Social : DbSet<clsPatientSocial>
        /// </summary>
        public DbSet<clsPatientSocial> T_Patient_Social { get; set; }

        /// <summary>
        /// M_SERVICE : DbSet<clsService>
        /// </summary>
        public DbSet<clsService> M_SERVICE { get; set; }

        /// <summary>
        /// T_DOCUMENT : DbSet<clsDocument> 
        /// </summary>
        public DbSet<clsDocument> T_DOCUMENT { get; set; }

        /// <summary>
        /// Vw_Followup : DbSet<clsFollowUp>
        /// </summary>
        public DbSet<clsFollowUp> Vw_Followup { get; set; }

        /// <summary>
        /// Vw_Observation : DbSet<clsObservation>
        /// </summary>
        public DbSet<clsObservation> Vw_Observation { get; set; }

        /// <summary>
        /// t_Allergies : DbSet<clsAllergies>
        /// </summary>
        public DbSet<clsAllergies> t_Allergies { get; set; }

        /// <summary>
        /// t_Immunizations : DbSet<clsImmunizations>
        /// </summary>
        public DbSet<clsImmunizations> t_Immunizations { get; set; }

        /// <summary>
        /// t_Social : DbSet<clsSocials>
        /// </summary>
        public DbSet<clsSocials> t_Social { get; set; }

        /// <summary>
        /// t_Family : DbSet<clsFamilies>
        /// </summary>
        public DbSet<clsFamilies> t_Family { get; set; }

        #endregion

        #region OnModelCreating********************************************************************************************************************************
        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region SaveChanges************************************************************************************************************************************
        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns>int</returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        #endregion
    }
}
