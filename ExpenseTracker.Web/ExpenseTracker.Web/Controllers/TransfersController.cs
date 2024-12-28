using ExpenseTracker.Web.Requests.Category;
using ExpenseTracker.Web.Requests.Common;
using ExpenseTracker.Web.Requests.Transfer;
using ExpenseTracker.Web.Requests.Wallet;
using ExpenseTracker.Web.Stores.Category;
using ExpenseTracker.Web.Stores.Pdpf;
using ExpenseTracker.Web.Stores.Transfer;
using ExpenseTracker.Web.Stores.Wallet;
using ExpenseTracker.Web.ViewModels.Transfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpenseTracker.Web.Controllers;

public class TransfersController : Controller
{
    public const int MaxFileSize = 2 * 1024 * 1024;
    public const int MinFileSize = 1024;

    private static readonly List<string> _allowedFileTypes =
    [
        "png", "jpeg", "gif", "pdf"
    ];

    private readonly ITransferStore _transferStore;
    private readonly ICategoryStore _categoryStore;
    private readonly IWalletStore _walletStore;
    private readonly PdfStore _pdfStore;

    public TransfersController(PdfStore pdfStore, ITransferStore store, ICategoryStore categoryStore, IWalletStore walletStore)
    {
        _pdfStore = pdfStore ?? throw new ArgumentNullException(nameof(pdfStore));
        _transferStore = store ?? throw new ArgumentNullException(nameof(store));
        _categoryStore = categoryStore ?? throw new ArgumentNullException(nameof(categoryStore));
        _walletStore = walletStore ?? throw new ArgumentNullException(nameof(walletStore));
    }

    public async Task<IActionResult> Index([FromQuery] GetTransfersRequest request)
    {
        var transfers = await _transferStore.GetAll(request);
        var categories = await _categoryStore.GetAll(new GetCategoriesRequest(request.UserId, null));
        var wallets = await _walletStore.GetAll(new GetWalletsRequest(request.UserId, null));


        ViewBag.Search = request.Search;
        ViewBag.Categories = categories;
        ViewBag.SelectedCategory = request.CategoryId;
        ViewBag.Wallets = wallets;

        return View(transfers);
    }

    public async Task<IActionResult> Details([FromRoute] TransferRequest request)
    {
        var transfer = await _transferStore.GetById(request);

        return View(transfer);
    }

    public async Task<IActionResult> Create([FromHeader] UserRequest request)
    {
        await PopulateViewBag(request);

        var model = new CreateTransferRequest(
            UserId: request.UserId,
            CategoryId: default,
            Title: default,
            WalletId: default,
            Notes: default,
            Amount: default,
            Date: DateTime.UtcNow);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateTransferRequest request, [FromForm] List<IFormFile> attachments)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Create");
        }

        foreach (var attachment in attachments)
        {
            if (!TryValidateFile(ModelState, attachment))
            {
                await PopulateViewBag(request, request.CategoryId, request.WalletId);

                return View(request);
            }
        }

        var createdTransfer = await _transferStore.Create(request, attachments);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit([FromRoute] TransferRequest request)
    {
        var transfer = await _transferStore.GetById(request);

        await PopulateViewBag(request, transfer.CategoryId, transfer.WalletId);

        var model = new UpdateTransferRequest(
            Id: request.Id,
            UserId: request.UserId,
            CategoryId: transfer.CategoryId,
            Title: transfer.Title,
            WalletId: transfer.WalletId,
            Notes: transfer.Notes,
            Amount: transfer.Amount,
            Date: transfer.Date);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] UpdateTransferRequest request, [FromForm] List<IFormFile> attachments)
    {

        if (!ModelState.IsValid)
        {
            return View(request);
        }

        await _transferStore.Update(request);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromRoute] TransferRequest request)
    {
        var transfer = await _transferStore.GetById(request);

        return View(transfer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] TransferRequest request)
    {
        await _transferStore.Delete(request);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Filters transfers
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="search"></param>
    /// <returns>List of filtered transfers</returns>
    [Route("getTransfers")]
    public async Task<ActionResult<TransferViewModel>> GetTransfers(GetTransfersRequest request)
    {
        var result = await _transferStore.GetAll(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadPdf(GetTransfersRequest request)
    {
        var stream = await _pdfStore.PdfDownload(request);

        return File(stream, "application/pdf", "Transfers.pdf");
    }
    private static bool TryValidateFile(ModelStateDictionary modelState, IFormFile? formFile)
    {
        if (formFile is null)
        {
            return true;
        }

        if (formFile.Length < MinFileSize)
        {
            modelState.AddModelError(string.Empty, "Image file is too small.");

            return false;
        }

        if (formFile.Length > MaxFileSize)
        {
            modelState.AddModelError(string.Empty, "Image file is too big.");

            return false;
        }

        if (!_allowedFileTypes.Exists(type => formFile.ContentType.Contains(type)))
        {
            modelState.AddModelError(string.Empty, "Invalid image format");

            return false;
        }

        return true;
    }

    private async Task PopulateViewBag(UserRequest request, int? categoryId = null, int? walletId = null)
    {
        var categories = await _categoryStore.GetAll(new GetCategoriesRequest(request.UserId, null));
        var wallets = await _walletStore.GetAll(new GetWalletsRequest(request.UserId, null));

        var defaultCategory = categoryId.HasValue
            ? categories.First(x => x.Id == categoryId.Value)
            : categories.FirstOrDefault();
        var defaultWallet = walletId.HasValue
            ? wallets.First(x => x.Id == walletId.Value)
            : wallets.FirstOrDefault();

        ViewBag.Wallets = wallets;
        ViewBag.Categories = categories;
        ViewBag.DefaultCategory = defaultCategory;
        ViewBag.DefaultWallet = defaultWallet;
    }
}
