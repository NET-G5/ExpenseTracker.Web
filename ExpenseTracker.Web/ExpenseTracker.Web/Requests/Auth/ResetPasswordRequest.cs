namespace ExpenseTracker.Web.Requests.Auth;

public sealed record ResetPasswordRequest(string Email, string Password, string PasswordConfirm, string Token);
