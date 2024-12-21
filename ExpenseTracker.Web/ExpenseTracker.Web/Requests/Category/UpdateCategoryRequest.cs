using ExpenseTracker.Web.Enums;

namespace ExpenseTracker.Web.Requests.Category;

public sealed record UpdateCategoryRequest(
    Guid UserId,
    int Id,
    string Name,
    string? Description,
    CategoryType Type)
    : CreateCategoryRequest(
        UserId: UserId,
        Name: Name,
        Description: Description,
        Type: Type);
