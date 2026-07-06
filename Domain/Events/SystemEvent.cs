using NotificationSystem.Domain.Events;

namespace NotificationSystem.Domain.NewFolder
{
    public class ErrorEvent : IEvent
    {
        public string  Message { get; set; } =string.Empty;
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    }
}
