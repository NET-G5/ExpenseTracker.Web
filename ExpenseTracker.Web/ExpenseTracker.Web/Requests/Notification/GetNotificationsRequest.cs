using ExpenseTracker.Web.Requests.Common;

namespace ExpenseTracker.Web.Requests.Notification;

public sealed record GetNotificationsRequest(Guid UserId, int Something)
    : UserRequestId(UserId: UserId);
