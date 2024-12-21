namespace ExpenseTracker.Web.Requests.Auth;

public sealed record LoginUserRequest(
    string UserName,
    bool RememberMe,
    string Password);
