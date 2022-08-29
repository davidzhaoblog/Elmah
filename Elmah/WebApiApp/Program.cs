using Elmah.RepositoryContracts;
using Elmah.EFCoreRepositories;
using Elmah.ServiceContracts;
using Elmah.Services;
using Elmah.Models;
using Framework.Models;
using Elmah.EFCoreContext;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen((Action<SwaggerGenOptions>)(options =>
{
    options.CustomSchemaIds((Func<Type, string>)(x =>
    {
        return GetSwaggerCustomizedSchemaId(x);
    }));
}));
builder.Services.AddSwaggerGen();

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

builder.Services.AddDbContext<EFDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Elmah"), x => { x.UseNetTopologySuite(); x.UseBulk(); }),  ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetSwaggerCustomizedSchemaId(Type x)
{
        // 1. Customized DefaultView SchemaIds

        if (x == typeof(ElmahErrorDataModel.DefaultView))
        {
            return "Elmah.Models.ElmahErrorDataModelDefaultView";
        }
        // 2. Customized PagedResponse SchemaIds

        if (x == typeof(ListResponse<ElmahErrorDataModel.DefaultView[]>))
        {
            return "Elmah.Models.ElmahErrorListResponse";
        }

        if (x == typeof(ListResponse<ElmahApplicationDataModel[]>))
        {
            return "Elmah.Models.ElmahApplicationListResponse";
        }

        if (x == typeof(ListResponse<ElmahHostDataModel[]>))
        {
            return "Elmah.Models.ElmahHostListResponse";
        }

        if (x == typeof(ListResponse<ElmahSourceDataModel[]>))
        {
            return "Elmah.Models.ElmahSourceListResponse";
        }

        if (x == typeof(ListResponse<ElmahStatusCodeDataModel[]>))
        {
            return "Elmah.Models.ElmahStatusCodeListResponse";
        }

        if (x == typeof(ListResponse<ElmahTypeDataModel[]>))
        {
            return "Elmah.Models.ElmahTypeListResponse";
        }

        if (x == typeof(ListResponse<ElmahUserDataModel[]>))
        {
            return "Elmah.Models.ElmahUserListResponse";
        }
        // 3. Customized BulkActionDynamicParamsRequests SchemaIds

        if (x == typeof(BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>))
        {
            return "Elmah.Models.ElmahErrorBulkActionDynamicParamsRequest";
        }
    return x?.FullName;
}

