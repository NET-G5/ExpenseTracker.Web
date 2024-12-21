﻿//using ExpenseTracker.Web.Requests.Wallet;
//using ExpenseTracker.Web.Requests.WalletShare;
//using ExpenseTracker.Web.Stores.Interfaces;
//using ExpenseTracker.Web.ViewModels.Wallet;
//using ExpenseTracker.Web.ViewModels.WalletShare;
//using Microsoft.AspNetCore.Mvc;
//using NuGet.Common;

//namespace ExpenseTracker.Web.Controllers;

//public class WalletsController : Controller
//{
//    private readonly IWalletStore _store;

//    public WalletsController(IWalletStore store)
//    {
//        _store = store ?? throw new ArgumentNullException(nameof(store));
//    }

//    public IActionResult Index([FromQuery] GetWalletsRequest request)
//    {
//        var wallets = _store.GetAll(request);

//        ViewBag.Search = request.Search;

//        return View(wallets);
//    }

//    public IActionResult Details([FromRoute] WalletRequest request)
//    {
//        var wallet = _store.GetById(request);

//        return Json(wallet);
//    }

//    public IActionResult Create()
//    {
//        return View();
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Create([FromForm] CreateWalletRequest request)
//    {
//        if (!ModelState.IsValid)
//        {
//            return View(request);
//        }

//        var createdWallet = _store.Create(request);


//        return Json(new { success = true });
//    }

//    public IActionResult Edit([FromRoute] WalletRequest request)
//    {
//        var wallet = _store.GetById(request);

//        return View(wallet);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Edit([FromForm] UpdateWalletRequest request)
//    {
//        if (!ModelState.IsValid)
//        {
//            return View(request);
//        }

//        _store.Update(request);

//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult Delete([FromRoute] WalletRequest request)
//    {
//        var wallet = _store.GetById(request);

//        return View(wallet);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    [Route("Delete")]
//    public IActionResult DeleteConfirmed([FromForm] WalletRequest request)
//    {
//        _store.Delete(request);

//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult Share([FromRoute] WalletRequest request)
//    {
//        var shareRequest = new CreateWalletShareRequest(request.UserId, request.Id, string.Empty, []);

//        return View(shareRequest);
//    }

//    [HttpPost]
//    public IActionResult Share([FromForm] CreateWalletShareRequest request)
//    {
//        if (!ModelState.IsValid)
//        {
//            return View(request);
//        }

//        _store.Share(request);

//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult Shares([FromRoute] WalletShareRequest request)
//    {
//        var walletShare = _store.GetWalletShareById(request);

//        return View(walletShare);
//    }

//    /// <summary>
//    /// Filters wallets
//    /// </summary>
//    /// <param name="request"></param>
//    /// <returns>List of filtered wallets</returns>
//    [Route("getWallets")]
//    public ActionResult<WalletViewModel> GetWallets([FromQuery] GetWalletsRequest request)
//    {
//        var wallets = _store.GetAll(request);

//        return Ok(wallets);
//    }

//    [HttpGet]
//    [Route("shareRequest/{requestId:int}")]
//    public ActionResult<WalletShareViewModel> GetWalletShare(WalletShareRequest request)
//    {
//        var walletShare = _store.GetWalletShareById(request);

//        return Json(walletShare);
//    }
//}
