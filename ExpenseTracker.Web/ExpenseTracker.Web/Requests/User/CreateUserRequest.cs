using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.User;

public record CreateUserRequest(
    Guid Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string Password)
    : Common.UserRequest(Id);