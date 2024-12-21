namespace ExpenseTracker.Web.Requests.Auth;

public sealed record RegisterUserRequest(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword);
