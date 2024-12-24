using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Transfer;

public record CreateTransferRequest(
    Guid UserId,
    int CategoryId,
    int WalletId,
    string? Notes,
    decimal Amount,
    DateTime Date)
    : UserRequest(UserId: UserId);
