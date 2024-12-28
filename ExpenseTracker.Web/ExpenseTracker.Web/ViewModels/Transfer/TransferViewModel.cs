namespace ExpenseTracker.Web.ViewModels.Transfer;

public class TransferViewModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Notes { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public int CategoryId { get; init; }
    public string CategoryName { get; set; }
    public int WalletId { get; init; }
    public string WalletName { get; set; }

    public List<string> Images { get; init; }

    public TransferViewModel()
    {
        Images = [];
    }
}
