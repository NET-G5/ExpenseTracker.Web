using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.WalletShare;

public sealed record WalletShareRequest(Guid UserId, int Id)
    : UserRequestId(UserId: UserId);
