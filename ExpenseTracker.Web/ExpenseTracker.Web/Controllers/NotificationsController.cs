//using ExpenseTracker.Web.Requests.Notification;
//using ExpenseTracker.Web.Stores.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace ExpenseTracker.Web.Controllers
//{
//    public class NotificationsController : Controller
//    {
//        private readonly INotificationStore _notificationStore;

//        public NotificationsController(INotificationStore notificationStore)
//        {
//            _notificationStore = notificationStore ?? throw new ArgumentNullException(nameof(notificationStore));
//        }

//        public IActionResult Index(GetNotificationsRequest request)
//        {
//            var notifications = _notificationStore.GetAll(request);

//            return View(notifications);
//        }
//    }
//}
