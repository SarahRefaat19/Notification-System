using NotificationSystem.Application.Dtos;
using NotificationSystem.Domain;
namespace NotificationSystem.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendAsync(string email, NotificationMessage message);

    }
}
