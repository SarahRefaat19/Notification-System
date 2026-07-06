namespace NotificationSystem.Domain.Entities
{

    public class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
    }
}
