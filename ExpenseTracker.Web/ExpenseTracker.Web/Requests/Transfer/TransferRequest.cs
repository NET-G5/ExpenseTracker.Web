using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Transfer;

public sealed record TransferRequest(Guid UserId, int Id)
    : UserRequestId(UserId: UserId);
