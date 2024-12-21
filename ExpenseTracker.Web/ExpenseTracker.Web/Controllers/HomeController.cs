using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("Home/Error")]
    public IActionResult Error(int? statusCode = 500) =>
        statusCode switch
        {
            401 => RedirectToAction("Login", "AccountController"),
            404 => View("NotFound"),
            _ => View("Error")
        };
}
