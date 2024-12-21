using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.User;

public sealed record GetUserRequest(
    Guid Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string? Search)
    : UserRequestId(Id);