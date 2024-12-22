using ExpenseTracker.Web.Stores.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class HomeController : Controller
{
    private readonly IDashboardStore _store;

    public HomeController(IDashboardStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public async Task<IActionResult> Index()
    {
        var dashboard = await _store.GetDashboardAsync();

        return View(dashboard);
    }

    [Route("Home/Error")]
    public IActionResult Error(int? statusCode = 500) =>
        statusCode switch
        {
            401 => RedirectToAction("Login", "Account"),
            404 => View("NotFound"),
            _ => View("Error")
        };
}
