namespace ExpenseTracker.Web.Requests.WalletShare;

internal sealed record UpdateWalletShareRequest(
    Guid UserId,
    int WalletId,
    string UsersToShareJson,
    List<string> UsersToShare)
    : CreateWalletShareRequest(
        UserId,
        WalletId,
        UsersToShareJson,
        UsersToShare);
