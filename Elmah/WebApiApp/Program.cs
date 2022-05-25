using Elmah.RepositoryContracts;
using Elmah.EFCoreRepositories;
using Elmah.ServiceContracts;
using Elmah.Services;
using Elmah.EFCoreContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
        options.UseSqlServer(builder.Configuration.GetConnectionString("Elmah"), x => x.UseNetTopologySuite()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

