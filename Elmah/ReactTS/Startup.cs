using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

using Elmah.EntityFrameworkContext;
using Elmah.DALContracts;
using Elmah.EntityFrameworkDAL;
using Elmah.WcfContracts;
using Elmah.CoreCommonBLL;
using Elmah.AspNetMvcCoreViewModel;
using Elmah.AspNetMvcCoreApiController;

namespace Elmah.MvcCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStringGetter.ConStr = Configuration.GetConnectionString("ElmahModelEntities");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Framework.Helpers.GeoHelperSinglton.Instance.Init("2");

            // 1. Localization and Globalization
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                 /* your configurations*/
                 var supportedCultures = new List<CultureInfo>
                 {
                        new CultureInfo("en"),
                        new CultureInfo("fr")
                 };

                 opts.DefaultRequestCulture = new RequestCulture("en", "en");
                 // Formatting numbers, dates, etc.
                 opts.SupportedCultures = supportedCultures;
                 // UI strings that we have localized.
                 opts.SupportedUICultures = supportedCultures;
             });

            // 2. Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                 // Please read "EU General Data Protection Regulation (GDPR) support in ASP.NET Core" https://docs.microsoft.com/en-us/aspnet/core/security/gdpr?view=aspnetcore-2.1
                 // Enabled Session/TempData now to cache ui query for X.PagedList.
                 // may need save current TempData(for X.PagedList) to database, or use Ajax Call.
                 options.CheckConsentNeeded = context => false; // true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 3. Database
            services.Configure<ConnectionStringOption>(options =>
            {
                // set connection string from configuration
                options.ConnectionString = ConnectionStringGetter.ConStr;
            });

            services.AddDbContext<ElmahModelEntities>(options =>
                options.UseSqlServer(ConnectionStringGetter.ConStr, x => x.UseNetTopologySuite()), ServiceLifetime.Transient);

            // 4. ASPNET CORE Identity

            services.AddDbContext<Elmah.MvcCore.Data.ApplicationDbContext>(options =>
                           options.UseSqlServer(ConnectionStringGetter.ConStr));

            services.AddIdentity<Elmah.MvcCore.Models.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Elmah.MvcCore.Data.ApplicationDbContext>()
                .AddDefaultTokenProviders()
                //.AddSignInManager<ApplicationSignInManager>()
                .AddClaimsPrincipalFactory<Elmah.MvcCore.Security.ApplicationClaimsPrincipalFactory>()
                ;

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                //options.Cookie.h.HttpOnly = true;
                // If the LoginPath isn't set, ASP.NET Core defaults
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(150);
            });

            // Add application services.
            services.AddTransient<Elmah.MvcCore.Services.IEmailSender, Elmah.MvcCore.Services.EmailSender>();

            // 5. Mvc
            services.AddSession();

            // Add Cors
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.WithOrigins("http://localhost:7813", "https://localhost:7814", "http://localhost:3456", "http://localhost:3457")
                       //.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            /*
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddDataAnnotationsLocalization(options => {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(Elmah.Resx.UIStringResourcePerEntity));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddApplicationPart(typeof(MapApiController).Assembly)
                .AddControllersAsServices()
                .AddSessionStateTempDataProvider();
            */

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSingleton<IConfiguration>(Configuration); //add Configuration to our services collection

            #region 1. DI/IoC Repositories

            // 1.1 ElmahModel.ELMAH_Error.Repository
            services.AddScoped<IELMAH_ErrorRepository, ELMAH_ErrorRepository>();

            // 1.2 ElmahModel.ElmahApplication.Repository
            services.AddScoped<IElmahApplicationRepository, ElmahApplicationRepository>();

            // 1.3 ElmahModel.ElmahHost.Repository
            services.AddScoped<IElmahHostRepository, ElmahHostRepository>();

            // 1.4 ElmahModel.ElmahSource.Repository
            services.AddScoped<IElmahSourceRepository, ElmahSourceRepository>();

            // 1.5 ElmahModel.ElmahStatusCode.Repository
            services.AddScoped<IElmahStatusCodeRepository, ElmahStatusCodeRepository>();

            // 1.6 ElmahModel.ElmahType.Repository
            services.AddScoped<IElmahTypeRepository, ElmahTypeRepository>();

            // 1.7 ElmahModel.ElmahUser.Repository
            services.AddScoped<IElmahUserRepository, ElmahUserRepository>();

            #endregion 1. DI/IoC Repositories

            #region 2. DI/IoC Services

            // 2.1 ElmahModel.ELMAH_Error.Service
            services.AddScoped<IELMAH_ErrorService, ELMAH_ErrorService>();

            // 2.2 ElmahModel.ElmahApplication.Service
            services.AddScoped<IElmahApplicationService, ElmahApplicationService>();

            // 2.3 ElmahModel.ElmahHost.Service
            services.AddScoped<IElmahHostService, ElmahHostService>();

            // 2.4 ElmahModel.ElmahSource.Service
            services.AddScoped<IElmahSourceService, ElmahSourceService>();

            // 2.5 ElmahModel.ElmahStatusCode.Service
            services.AddScoped<IElmahStatusCodeService, ElmahStatusCodeService>();

            // 2.6 ElmahModel.ElmahType.Service
            services.AddScoped<IElmahTypeService, ElmahTypeService>();

            // 2.7 ElmahModel.ElmahUser.Service
            services.AddScoped<IElmahUserService, ElmahUserService>();

            #endregion 2. DI/IoC Services

            #region 3. DI/IoC AspNetMvcCoreViewModel.ItemViewModels

            // 3.1 ElmahModel.ELMAH_Error.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ELMAH_Error.ItemVM>();

            // 3.2 ElmahModel.ElmahApplication.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM>();

            // 3.3 ElmahModel.ElmahHost.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahHost.ItemVM>();

            // 3.4 ElmahModel.ElmahSource.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahSource.ItemVM>();

            // 3.5 ElmahModel.ElmahStatusCode.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahStatusCode.ItemVM>();

            // 3.6 ElmahModel.ElmahType.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahType.ItemVM>();

            // 3.7 ElmahModel.ElmahUser.ItemViewModel
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahUser.ItemVM>();

            #endregion 3. DI/IoC AspNetMvcCoreViewModel.ItemViewModels


            #region 4. DI/IoC AspNetMvcCoreViewModel.WorkspaceViewModels

            // 4.1.1 ElmahModel.ELMAH_Error.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ELMAH_Error.IndexVM>();

            // 4.1.2 ElmahModel.ELMAH_Error.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ELMAH_Error.DashboardVM>();

            // 4.2.1 ElmahModel.ElmahApplication.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahApplication.IndexVM>();

            // 4.2.2 ElmahModel.ElmahApplication.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahApplication.DashboardVM>();

            // 4.3.1 ElmahModel.ElmahHost.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahHost.IndexVM>();

            // 4.3.2 ElmahModel.ElmahHost.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahHost.DashboardVM>();

            // 4.4.1 ElmahModel.ElmahSource.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahSource.IndexVM>();

            // 4.4.2 ElmahModel.ElmahSource.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahSource.DashboardVM>();

            // 4.5.1 ElmahModel.ElmahStatusCode.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahStatusCode.IndexVM>();

            // 4.5.2 ElmahModel.ElmahStatusCode.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahStatusCode.DashboardVM>();

            // 4.6.1 ElmahModel.ElmahType.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahType.IndexVM>();

            // 4.6.2 ElmahModel.ElmahType.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahType.DashboardVM>();

            // 4.7.1 ElmahModel.ElmahUser.SearchResult
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahUser.IndexVM>();

            // 4.7.2 ElmahModel.ElmahUser.FullDetails
            services.AddScoped<Elmah.AspNetMvcCoreViewModel.ElmahUser.DashboardVM>();

            #endregion 4. DI/IoC AspNetMvcCoreViewModel.WorkspaceViewModels

            services.Configure<FrameworkCore.Services.GoogleMapOptions>(Configuration.GetSection("GoogleMap"));
            services.AddTransient<FrameworkCore.Services.GoogleMapHelper>();

            services.AddScoped<IMapRepository, MapRepository>();
            services.AddScoped<IMapService, MapService>();

            services.AddScoped<MapVM>();
            services.AddScoped<SystemDashboardVM>();
            services.AddScoped<UISharedViewModel>();
            services.AddScoped<DashboardVM>();

            // ViewModelsHelper.RegisterExtendedViewModels(services);

            //services.AddControllersWithViews().AddJsonOptions(
            //    options => { options.JsonSerializerOptions.IgnoreNullValues = true;
            //         options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; }) ;

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // ReferenceLoopHandling.Ignore
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //// Camel Case
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //// Date Format
                //options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                // Ingore null
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            services.AddSwaggerGenNewtonsoftSupport();

            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressConsumesConstraintForFormFileParameters = true;
            //    options.SuppressInferBindingSourcesForParameters = true;
            //    options.SuppressModelStateInvalidFilter = true;
            //    options.SuppressMapClientErrors = true;
            //    //options.ClientErrorMapping[404].Link =
            //    //    "https://httpstatuses.com/404";
            //});

            var assembly = typeof(Elmah.AspNetMvcCoreApiController.HomeApiController).Assembly;
            services.AddControllersWithViews()
                .AddApplicationPart(assembly);
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseCookiePolicy();

            app.UseRequestLocalization();

            // Enable Cors
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
    public static class ConnectionStringGetter
    {
        public static string ConStr { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}

