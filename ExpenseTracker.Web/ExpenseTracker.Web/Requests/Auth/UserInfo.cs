namespace ExpenseTracker.Web.Requests.Auth;

public sealed class UserInfo
{
    public string? Browser { get; set; }
    public string? OS { get; set; }
    public string? Device { get; set; }
}
