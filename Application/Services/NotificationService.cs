using Microsoft.AspNetCore.SignalR;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Application.Dtos;

using NotificationSystem.Domain;
using NotificationSystem.Insfrastructure;
using NotificationSystem.Insfrastructure.SignlR;
using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Application.Services
{
    public class NotificationService :INotificationService
    {
     
            private readonly NotificationRepository _notificationRepository;
            private readonly UserRepository _userRepository;
            private readonly UserNotificationRepository _userNotificationRepository;
            private readonly IInAppNotificationService _inApp;
            private readonly IEmailNotificationService _email;
            public NotificationService(NotificationRepository notificationRepository, UserRepository userRepository, UserNotificationRepository userNotificationRepository, IHubContext<NotificationHub> hubContext, IInAppNotificationService inApp, IEmailNotificationService email)
            {
                _notificationRepository = notificationRepository;
                _userRepository = userRepository;
                _userNotificationRepository = userNotificationRepository;
            _inApp = inApp;
            _email = email;
            }

            public async Task SendAsync(NotificationMessage message)
            {
            //Map To Entity
             var notification = new Notification
            {
               Title = message.Title,
               Message = message.Body
            };
            //save
            await _notificationRepository.CreateAsync(notification);
            //Get Users
                var users = await _userRepository.GetAllAsync();
            //Create UserNotification
                foreach (var user in users)
                {

                await _userNotificationRepository.CreateAsync(new UserNotification { UserId = user.Id, NotificationId = notification.Id, IsRead = false });

                }
                //send inapp
            await _inApp.SendAsync(message);
            foreach(var user in users)
            {
                //sendmail
                await _email.SendAsync(user.Email, message);

            }
        }

    }


}

