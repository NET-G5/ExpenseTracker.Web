using ExpenseTracker.Web.Configurations;
using ExpenseTracker.Web.Filters;
using ExpenseTracker.Web.Services.Api;
using ExpenseTracker.Web.Stores;
using ExpenseTracker.Web.Stores.Auth;
using ExpenseTracker.Web.Stores.Category;
using ExpenseTracker.Web.Stores.Dashboard;
using Syncfusion.Licensing;

namespace ExpenseTracker.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddConfigurations(services, configuration);
        AddControllers(services);
        AddStores(services);
        AddServices(services);
        AddProviders(configuration);

        return services;
    }

    private static void AddControllers(IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new ExceptionFilter());
        });
    }

    private static void AddConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ApiConfigurations>()
            .Bind(configuration.GetSection(ApiConfigurations.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddStores(IServiceCollection services)
    {
        services.AddScoped<IAuthStore, AuthStore>();
        services.AddScoped<IDashboardStore, DashboardStore>();
        services.AddScoped<ICategoryStore, CategoryStore>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IApiClient, ApiClient>();
        services.AddHttpClient();
        services.AddHttpContextAccessor();
    }

    private static void AddProviders(IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("SyncfusionKey")
            ?? throw new InvalidOperationException("Syncfusion key is not found.");

        SyncfusionLicenseProvider.RegisterLicense(key);
    }
}
