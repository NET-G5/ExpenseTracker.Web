using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Wallet;

public sealed record WalletRequest(Guid UserId, int Id)
    : UserRequestId(UserId: UserId);
