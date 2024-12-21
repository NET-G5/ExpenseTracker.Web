using ExpenseTracker.Web.Enums;

namespace ExpenseTracker.Web.Requests.WalletShare;

public record WalletUserShare(Guid ShareWithUserId, WalletAccessType AccessType);
