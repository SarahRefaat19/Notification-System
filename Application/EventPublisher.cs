using Hangfire;
using NotificationSystem.Domain.Events;
using NotificationSystem.Domain.NewFolder;
namespace NotificationSystem.Application
{
    public class EventPublisher
    {
        public void Publish(ErrorEvent @event) 
        {
            BackgroundJob.Enqueue<EventDispatcher>(p => p.Dispatch(@event));

        }
        public void Publish(BookingApprovedEvent @event)
        {
            BackgroundJob.Enqueue<EventDispatcher>(p => p.Dispatch(@event));

        }
    }
}
