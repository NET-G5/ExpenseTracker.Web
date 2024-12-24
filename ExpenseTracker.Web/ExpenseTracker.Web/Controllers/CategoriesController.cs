using ExpenseTracker.Web.Requests.Category;
using ExpenseTracker.Web.Stores.Category;
using ExpenseTracker.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryStore _store;

    public CategoriesController(ICategoryStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public async Task<IActionResult> Index([FromQuery] GetCategoriesRequest request)
    {
        var categories = await _store.GetAll(request);

        ViewBag.Search = request.Search;

        return View(categories);
    }

    public async Task<IActionResult> Details([FromRoute] CategoryRequest request)
    {
        var category = await _store.GetById(request);

        return Json(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            return Json(new { success = false, errors });
        }

        await _store.Create(request);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] UpdateCategoryRequest request)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return Json(new { success = false, errors });
        }

        await _store.Update(request);

        return RedirectToAction(nameof(Index));

    }

    [HttpDelete, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] CategoryRequest request)
    {
        await _store.Delete(request);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Filters categories
    /// </summary>  
    /// <param name="search"></param>
    /// <returns>List of filtered categories</returns>
    [Route("getCategories")]
    public async Task<ActionResult<List<CategoryViewModel>>> GetCategories([FromQuery] GetCategoriesRequest request)
    {
        var categories = await _store.GetAll(request);

        return Ok(categories);
    }
}