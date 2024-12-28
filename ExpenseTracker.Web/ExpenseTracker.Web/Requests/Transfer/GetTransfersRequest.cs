using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Transfer;

public sealed record GetTransfersRequest(
    Guid UserId,
    int? CategoryId,
    int? WalletId,
    string Title,
    string? Search,
    decimal MaxAmount,
    decimal? MinAmount)
    : UserRequest(UserId: UserId);
