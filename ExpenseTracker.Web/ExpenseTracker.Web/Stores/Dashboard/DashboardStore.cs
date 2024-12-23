using ExpenseTracker.Web.Services.Api;
using ExpenseTracker.Web.ViewModels.Dashboard;

namespace ExpenseTracker.Web.Stores.Dashboard;

internal sealed class DashboardStore : IDashboardStore
{
    private readonly IApiClient _client;

    public DashboardStore(IApiClient client)
    {
        _client = client;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var dashboard = await _client.GetAsync<DashboardViewModel>("dashboard");

        return dashboard;
    }
}
