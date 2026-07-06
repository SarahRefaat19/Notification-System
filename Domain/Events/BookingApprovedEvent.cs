namespace NotificationSystem.Domain.Events
{
    public class BookingApprovedEvent :IEvent
    {
       public int bookingId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
