using NotificationSystem.Application.Dtos;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Domain.Entities;
using NotificationSystem.Domain.Events;

namespace NotificationSystem.Application.Handlers
{
    public class BookingApprovedHandler : IEventHandler<BookingApprovedEvent>
    {
        private readonly INotificationService _notificationService;
        public BookingApprovedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(BookingApprovedEvent @event)
        {
            var notificationmessage = new NotificationMessage
            {
                Title = "BookingApproved",
                Body = @event.Message
            };
       await  _notificationService.SendAsync(notificationmessage);
           
        }

    }
}
