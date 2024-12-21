namespace ExpenseTracker.Web.Requests.User;

public sealed record UpdateUserRequest(
    Guid Id,
    string UserName,
    string Email,
    string? PhoneNumber,
    string Password)
    : CreateUserRequest(
        Id,
        UserName,
        Email,
        PhoneNumber,
        Password);