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

        if (x == typeof(PagedResponse<ElmahErrorDataModel.DefaultView[]>))
        {
            return "Elmah.Models.ElmahErrorPagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahApplicationDataModel[]>))
        {
            return "Elmah.Models.ElmahApplicationPagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahHostDataModel[]>))
        {
            return "Elmah.Models.ElmahHostPagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahSourceDataModel[]>))
        {
            return "Elmah.Models.ElmahSourcePagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahStatusCodeDataModel[]>))
        {
            return "Elmah.Models.ElmahStatusCodePagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahTypeDataModel[]>))
        {
            return "Elmah.Models.ElmahTypePagedResponse";
        }

        if (x == typeof(PagedResponse<ElmahUserDataModel[]>))
        {
            return "Elmah.Models.ElmahUserPagedResponse";
        }
        // 3. Customized BulkActionDynamicParamsRequests SchemaIds

        if (x == typeof(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>))
        {
            return "Elmah.Models.ElmahErrorBulkActionDynamicParamsRequest";
        }
        // 4. Customized BulkActionNoParamRequests SchemaIds

        if (x == typeof(BatchActionViewModel<ElmahErrorIdentifier>))
        {
            return "Elmah.Models.ElmahErrorBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahApplicationIdentifier>))
        {
            return "Elmah.Models.ElmahApplicationBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahHostIdentifier>))
        {
            return "Elmah.Models.ElmahHostBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahSourceIdentifier>))
        {
            return "Elmah.Models.ElmahSourceBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahStatusCodeIdentifier>))
        {
            return "Elmah.Models.ElmahStatusCodeBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahTypeIdentifier>))
        {
            return "Elmah.Models.ElmahTypeBulkActionNoParamsRequest";
        }

        if (x == typeof(BatchActionViewModel<ElmahUserIdentifier>))
        {
            return "Elmah.Models.ElmahUserBulkActionNoParamsRequest";
        }
    return x?.FullName;
}

