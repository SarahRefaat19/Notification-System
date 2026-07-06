using NotificationSystem.Application.Dtos;
using NotificationSystem.Domain;

namespace NotificationSystem.Application.Interfaces
{
    public interface IInAppNotificationService
    {
        Task SendAsync(NotificationMessage message);

    }
}
