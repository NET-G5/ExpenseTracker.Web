namespace ExpenseTracker.Web.ViewModels.Notification;

public class NotificationViewModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Body { get; set; }
    public required string RedirectUrl { get; set; }
    public DateTime Date { get; set; }
}
