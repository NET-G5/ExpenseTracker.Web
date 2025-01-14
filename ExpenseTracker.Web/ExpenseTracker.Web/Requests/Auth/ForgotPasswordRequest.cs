namespace ExpenseTracker.Web.Requests.Auth;

public sealed class ForgotPasswordRequest
{
    public string Email { get; set; }
    public string RedirectUrl { get; set; }
    public string? OS { get; set; }
    public string? Browser { get; set; }
}
