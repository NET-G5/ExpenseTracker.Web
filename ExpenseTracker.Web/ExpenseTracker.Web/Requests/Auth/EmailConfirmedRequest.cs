namespace ExpenseTracker.Web.Requests.Auth;

public sealed record EmailConfirmedRequest(
    string Email,
    string Token);
