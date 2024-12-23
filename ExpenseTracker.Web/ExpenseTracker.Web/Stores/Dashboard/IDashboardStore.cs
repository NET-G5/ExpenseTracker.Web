using ExpenseTracker.Web.ViewModels.Dashboard;

namespace ExpenseTracker.Web.Stores.Dashboard;

public interface IDashboardStore
{
    Task<DashboardViewModel> GetDashboardAsync();
}
