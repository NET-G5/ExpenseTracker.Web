using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Category;

public sealed record GetCategoriesRequest(Guid UserId, string? Search)
    : UserRequestId(UserId: UserId);
