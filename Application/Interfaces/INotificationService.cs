using NotificationSystem.Application.Dtos;
using NotificationSystem.Domain;
namespace NotificationSystem.Application.Interfaces
{
    public interface INotificationService
    {

        Task SendAsync(NotificationMessage message);

    }
}
