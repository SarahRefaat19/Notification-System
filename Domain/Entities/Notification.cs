namespace NotificationSystem.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        //    public Notification( string title, string message)
        //    {
        //        Title = title; Message = message;
        //    }
    }
}
