namespace NotificationSystem.Domain.Events
{
    public interface IEvent
    {
        DateTime OccurredAt { get; set; } 
    }
}
