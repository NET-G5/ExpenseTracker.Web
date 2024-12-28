using ExpenseTracker.Web.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/logs_.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
.CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == "Development")
{
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/logs_.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day);
    });
}
else
{
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/logs_.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
        .WriteTo.ApplicationInsights(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration
        {
            InstrumentationKey = "82ce4973-abc0-4cba-b3fd-b3184f89a841"
        },
        TelemetryConverter.Traces);
    });
}

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