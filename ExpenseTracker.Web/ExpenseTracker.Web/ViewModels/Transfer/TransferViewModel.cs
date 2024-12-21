using ExpenseTracker.Web.ViewModels.Category;
using ExpenseTracker.Web.ViewModels.Wallet;

namespace ExpenseTracker.Web.ViewModels.Transfer;

public class TransferViewModel
{
    public int Id { get; init; }
    public string? Note { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }

    public required WalletViewModel Wallet { get; set; }
    public required CategoryViewModel Category { get; init; }

    public List<string> Images { get; init; }

    public TransferViewModel()
    {
        Images = [];
    }
}
