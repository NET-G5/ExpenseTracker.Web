using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Wallet;

public sealed record GetWalletsRequest(Guid UserId, string? Search)
    : UserRequestId(UserId: UserId);
