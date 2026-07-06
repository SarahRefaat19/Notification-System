using NotificationSystem.Application.Dtos;
using NotificationSystem.Application.Interfaces;

namespace NotificationSystem.Application
{
    public class NotificationJobs
    {
        private readonly INotificationService _notificationService;
        public NotificationJobs(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
         public async Task ProcessMarketingNotification(NotificationMessage message)
         {
            await _notificationService.SendAsync(message);
         }

    }
}
