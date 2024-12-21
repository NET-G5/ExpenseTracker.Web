using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.WalletShare;

public sealed record GetWalletSharesRequest(Guid UserId, string? Search)
    : UserRequestId(UserId: UserId);
