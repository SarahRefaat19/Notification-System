using NotificationSystem.Application.Dtos;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Domain.Entities;
using NotificationSystem.Domain.Events;
using NotificationSystem.Domain.NewFolder;

namespace NotificationSystem.Application.Handlers
{
    public class ErrorEventHandler :IEventHandler<ErrorEvent> 
    {
        private readonly INotificationService _notificationService;
        public ErrorEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(ErrorEvent @event)
        {
            var notification = new NotificationMessage
            {
                Title = "SystemError",
                Body =@event.Message
            };
            await _notificationService.SendAsync(notification);

        }
    }
}
