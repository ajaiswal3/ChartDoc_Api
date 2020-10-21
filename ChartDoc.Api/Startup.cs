using ChartDoc.Api.Extensions;
using ChartDoc.Api.Options;
using ChartDoc.Context;
using ChartDoc.Services.DataService;
using ChartDoc.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.IO;

namespace ChartDoc.Api
{
    public class Startup
    {
        #region Constructor *****************************************************************************************************************************************
        /// <summary>
        /// Startup: Constructor
        /// </summary>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            _env = env;
        }
        #endregion

        #region Property ********************************************************************************************************************************************
        /// <summary>
        /// Configuration: Property
        /// </summary>
        public IConfiguration Configuration { get; }

        public IHostingEnvironment _env { get; }

        #endregion

        #region ConfigureServices ***********************************************************************************************************************************
        /// <summary>
        /// ConfigureServices: This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IIcdService, IcdService>();
            services.AddTransient<IPatientDetailsService, PatientDetailsService>();
            services.AddTransient<IDiagnosisService, DiagnosisService>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IInsuranceService, InsuranceService>();
            services.AddTransient<ICalendarService, CalendarService>();
            services.AddTransient<IReasonService, ReasonService>();
            services.AddTransient<ICoPayService, CoPayService>();
            services.AddTransient<IFlowSheetService, FlowSheetService>();
            services.AddTransient<ICptService, CptService>();
            services.AddTransient<IEncounterService, EncounterService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<IOthersPopulateService, OthersPopulateService>();
            services.AddTransient<IPatientInformationService, PatientInformationService>();
            services.AddTransient<IServiceDetailsService, ServiceDetailsService>();
            services.AddTransient<IImpressionPlanService, ImpressionPlanService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IChiefComplaintService, ChiefComplaintService>();
            services.AddTransient<IFollowupService, FollowupService>();
            services.AddTransient<IObservationService, ObservationService>();
            services.AddTransient<ISharedService, SharedService>();
            services.AddTransient<IAllergiesService, AllergiesService>();
            services.AddTransient<IImmunizationService, ImmunizationService>();
            services.AddTransient<ISocialService, SocialService>();
            services.AddTransient<IFamilyService, FamilyService>();
            services.AddTransient<ICheckOutService, CheckOutService>();
            services.AddTransient<IScheduleAppointmentService, ScheduleAppointmentService>();
            services.AddTransient<IFlowAreaService, FlowAreaService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAllergyImmunizationAlertSocialFamilyService, AllergyImmunizationAlertSocialFamilyService>();
            services.AddTransient<IMedicineService, MedicineService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IFileControlService, FileControlService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IChargeDateRangeService, ChargeDateRangeService>();
            services.AddTransient<IChargeMasterService, ChargeMasterService>();
            services.AddTransient<IClaimService, ClaimService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IClaimFieldsService, ClaimFieldsService>();
            services.AddTransient<IClaimStatusService, ClaimStatusService>();
            services.AddTransient<IReportService, ReportService>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ChartDocDBContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("constring")));

            services.AddMvc().AddJsonOptions(jsonOptions => jsonOptions.UseMemberCasing()).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddMvc().AddJsonOptions(jsonOptions => jsonOptions.UseMemberCasing()).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChartDoc Api", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod().AllowAnyHeader());
            });
        }

        #endregion

        #region Configure *******************************************************************************************************************************************
        /// <summary>
        /// Configure: This method gets called by the runtime. Use this method to configure the HTTP request pipeline..
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="logService">logService</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogService logService)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowMyOrigin");
            app.ConfigureExceptionHandler(logService);
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //Path.Combine(Configuration.GetSection("FolderPath").Value, "Images")),
            //    RequestPath = "/Images"
            //});

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(_env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            app.UseMvc();
        }
        #endregion
    }
}
