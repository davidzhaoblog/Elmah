using Elmah.RepositoryContracts;
using Elmah.EFCoreRepositories;
using Elmah.ServiceContracts;
using Elmah.Services;
using Elmah.EFCoreContext;
using Elmah.MvcWebApp.Models;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Elmah.Resx.Resources.UIStrings));
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

// http://blog.mohnady.com/2017/05/how-to-aspnet-core-resource-files-in.html
//
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("fr")
        };
    options.DefaultRequestCulture = new RequestCulture("fr", "fr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddSingleton<Elmah.Resx.IUIStrings, Elmah.Resx.UIStrings>();

// 1.1. IoC Repositories
builder.Services.AddScoped<IElmahErrorRepository, ElmahErrorRepository>();
builder.Services.AddScoped<IElmahApplicationRepository, ElmahApplicationRepository>();
builder.Services.AddScoped<IElmahHostRepository, ElmahHostRepository>();
builder.Services.AddScoped<IElmahSourceRepository, ElmahSourceRepository>();
builder.Services.AddScoped<IElmahStatusCodeRepository, ElmahStatusCodeRepository>();
builder.Services.AddScoped<IElmahTypeRepository, ElmahTypeRepository>();
builder.Services.AddScoped<IElmahUserRepository, ElmahUserRepository>();

// 1.2. IoC Services
builder.Services.AddScoped<IElmahErrorService, ElmahErrorService>();
builder.Services.AddScoped<IElmahApplicationService, ElmahApplicationService>();
builder.Services.AddScoped<IElmahHostService, ElmahHostService>();
builder.Services.AddScoped<IElmahSourceService, ElmahSourceService>();
builder.Services.AddScoped<IElmahStatusCodeService, ElmahStatusCodeService>();
builder.Services.AddScoped<IElmahTypeService, ElmahTypeService>();
builder.Services.AddScoped<IElmahUserService, ElmahUserService>();

// 1.3. Other Services
builder.Services.AddScoped<SelectListHelper>();
builder.Services.AddScoped<IDropDownListService, DropDownListService>();

builder.Services.AddDbContext<EFDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Elmah"), x => { x.UseNetTopologySuite(); x.UseBulk(); }),  ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

