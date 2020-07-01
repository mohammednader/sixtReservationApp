using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SIXTReservationApp.BackgroundServices;
using SIXTReservationApp.Hubs;
using SIXTReservationApp.SignalR;
using SIXTReservationBL;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Repositories;

namespace SIXTReservationApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //adding context
            var connection = Configuration.GetConnectionString("SixtContext") ?? "server=.;Database=SixtReservation;trusted_Connection=True;";
            services.AddDbContextPool<SixtReservationContext>(option => option.UseSqlServer(connection));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.MinimumSameSitePolicy = SameSiteMode.Strict;
            //    options.HttpOnly = HttpOnlyPolicy.None;
            //    options.Secure = _environment.IsDevelopment()
            //      ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            //});

            #region UnitOfWork Repo
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IJobTitleRepository, JobTitleRepository>();
            services.AddScoped<IReasonRepository, ReasonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
            services.AddScoped<IStatusStepRepository, StatusStepRepository>();
            services.AddScoped<INotificationSettingRepository, NotificationSettingRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IWeekDayRepository, WeekDayRepository>();
            services.AddScoped<IActionSettingRepository, ActionSettingRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IReservationStepLogRepository, ReservationStepLogRepository>();
            services.AddScoped<IReservationAssignmentRepository, ReservationAssignmentRepository>();
            services.AddScoped<IFormSubmittedRepository, FormSubmittedRepository>();



            services.AddScoped<IRateSegmentRepository, RateSegmentRepository>();
            services.AddScoped<IRateSegmentCategoryRepository, RateSegmentCategoryRepository>();
            services.AddScoped<IUploadLogRepository, UploadLogRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();
            services.AddScoped<IVReservationLogRepository, VReservationLogRepository>();
            services.AddScoped<IVReservationListRepository, VReservationListRepository>();
            services.AddScoped<IEmailSettingRepository, EmailSettingRepository>();
            services.AddScoped<IVReservationHistoryRepository, VReservationHistoryRepository>();

            #endregion
            services.AddHostedService<SIXTCancellationBackgroundHostedService>();

            services.AddScoped<IPushNotificationService, PushNotificationService>();

            services.AddSignalR();
            services.AddSingleton<Notify>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseAuthentication();
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "MyCookieMiddlewareInstance",
            //    LoginPath = new PathString("/Account/Unauthorized/"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden/"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapHub<Notify>("/Notify");
            });

        }
    }
}
