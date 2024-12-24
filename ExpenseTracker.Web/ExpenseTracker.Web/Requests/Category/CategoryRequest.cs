using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Category;

public sealed record CategoryRequest(
    Guid UserId,
    int Id)
    : UserRequest(UserId: UserId);
