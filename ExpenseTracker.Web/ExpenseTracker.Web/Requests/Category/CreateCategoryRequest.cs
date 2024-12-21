using ExpenseTracker.Web.Enums;
using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Category;

public record CreateCategoryRequest(
    Guid UserId,
    string Name,
    string? Description,
    CategoryType Type)
    : UserRequestId(UserId: UserId);
