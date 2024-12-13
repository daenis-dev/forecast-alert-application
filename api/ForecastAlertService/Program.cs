using ForecastAlertService.Data;
using ForecastAlertService.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var certificate = new X509Certificate2("certs/forecast_alert_api.p12", "changeit");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=forecast-alert-db;Port=5432;Database=forecast_alerts;Username=postgres;Password=changeitdb"));

builder.Services.AddScoped<IGetAllSpecifications, SpecificationService>();
builder.Services.AddScoped<SpecificationService>();
builder.Services.AddScoped<IGetAllOperators, OperatorService>();
builder.Services.AddScoped<OperatorService>();
builder.Services.AddScoped<IGetAllAlerts, AlertService>();
builder.Services.AddScoped<ICreateAlert, AlertService>();
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<IGetAllAlertSpecifications, AlertSpecificationService>();
builder.Services.AddScoped<ICreateAlertSpecification, AlertSpecificationService>();
builder.Services.AddScoped<AlertSpecificationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ServerCertificate = certificate;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

