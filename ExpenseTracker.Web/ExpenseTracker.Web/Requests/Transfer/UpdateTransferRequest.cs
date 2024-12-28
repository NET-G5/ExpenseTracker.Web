namespace ExpenseTracker.Web.Requests.Transfer;

public sealed record UpdateTransferRequest(
    Guid UserId,
    int Id,
    int CategoryId,
    int WalletId,
    string Title,
    string? Notes,
    decimal Amount,
    DateTime Date)
    : CreateTransferRequest(
        UserId: UserId,
        CategoryId: CategoryId,
        WalletId: WalletId,
        Title: Title,
        Notes: Notes,
        Amount: Amount,
        Date: Date);
