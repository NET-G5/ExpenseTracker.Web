namespace ExpenseTracker.Web.Requests.Auth;

public sealed record ResetPasswordRequest(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmPassword);
