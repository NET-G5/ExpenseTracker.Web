using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.User;

public sealed record UserRequest(
    Guid Id,
    string UserName,
    string Email,
    string? PhoneNumber)
    : UserRequestId(Id);