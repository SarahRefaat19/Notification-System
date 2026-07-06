using Microsoft.AspNetCore.SignalR;
using NotificationSystem.Application.Dtos;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Domain;
using NotificationSystem.Insfrastructure;
using NotificationSystem.Insfrastructure.SignlR;

namespace NotificationSystem.Application.Services
{
    public class InAppNotificationService :IInAppNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public InAppNotificationService(
    IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendAsync(NotificationMessage Message)
        {
           
            
            // SendSignlR > call SignalR hub to send notification to users
            await SendWithRetryAsync(Message);

        }


        private async Task SendWithRetryAsync(NotificationMessage Message)
        {
            int retryCount = 3;
            int delayMilliseconds = 1000;

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", Message);
                    break;
                }

                catch
                {
                    if (i == retryCount - 1) throw;
                    await Task.Delay(delayMilliseconds);
                }
            }
        }
    }
}

