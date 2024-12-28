using ExpenseTracker.Web.Requests.Wallet;
using ExpenseTracker.Web.Requests.WalletShare;
using ExpenseTracker.Web.Stores.Wallet;
using ExpenseTracker.Web.ViewModels.Wallet;
using ExpenseTracker.Web.ViewModels.WalletShare;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class WalletsController : Controller
{
    private readonly IWalletStore _store;

    public WalletsController(IWalletStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public async Task<IActionResult> Index([FromQuery] GetWalletsRequest request)
    {
        var wallets = await _store.GetAll(request);

        ViewBag.Search = request.Search;

        return View(wallets);
    }

    public async Task<IActionResult> Details([FromRoute] WalletRequest request)
    {
        var wallet = await _store.GetById(request);

        return View(wallet);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateWalletRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _store.Create(request);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit([FromRoute] WalletRequest request)
    {
        var wallet = await _store.GetById(request);

        return View(wallet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] UpdateWalletRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _store.Update(request);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromRoute] WalletRequest request)
    {
        var wallet = await _store.GetById(request);

        return View(wallet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Delete")]
    public async Task<IActionResult> DeleteConfirmed([FromForm] WalletRequest request)
    {
        await _store.Delete(request);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Share([FromRoute] WalletRequest request)
    {
        var shareRequest = new CreateWalletShareRequest(request.UserId, request.Id, string.Empty, []);

        return View(shareRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Share([FromForm] CreateWalletShareRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _store.Share(request);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Shares([FromRoute] WalletShareRequest request)
    {
        var walletShare = _store.GetWalletShareById(request);

        return View(walletShare);
    }

    /// <summary>
    /// Filters wallets
    /// </summary>
    /// <param name="request"></param>
    /// <returns>List of filtered wallets</returns>
    [Route("getWallets")]
    public async Task<ActionResult<WalletViewModel>> GetWallets([FromQuery] GetWalletsRequest request)
    {
        var wallets = await _store.GetAll(request);

        return Ok(wallets);
    }

    [HttpGet]
    [Route("shareRequest/{requestId:int}")]
    public async Task<ActionResult<WalletShareViewModel>> GetWalletShare(WalletShareRequest request)
    {
        var walletShare = await _store.GetWalletShareById(request);

        return Json(walletShare);
    }
}
