using Azure.Identity;
using ExpenseTracker.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var keyVaultUri = builder.Configuration.GetValue<string>("KeyVaultUri");


var connectionString = builder.Configuration.GetValue<string>("ApplicationInsight:ConnectionString");

if (string.IsNullOrEmpty(connectionString))
{
    Log.Error("Could not load application insight connection string.");
}

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration
    {
        ConnectionString = connectionString,
    }, TelemetryConverter.Traces)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.RegisterServices(builder.Configuration);

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

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();