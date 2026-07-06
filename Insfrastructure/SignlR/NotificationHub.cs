using Microsoft.AspNetCore.SignalR;
using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Insfrastructure.SignlR
{
    public class NotificationHub : Hub
    {

        public Task SendNotificationToAllUsers(Notification notification)
        {
            return Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
