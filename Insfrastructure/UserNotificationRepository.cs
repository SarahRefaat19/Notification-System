using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Insfrastructure
{
    public class UserNotificationRepository
    {
        private readonly AppDbContext _context;
        public UserNotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(UserNotification userNotification)
        {
            await _context.UserNotifications.AddAsync(userNotification);
            await _context.SaveChangesAsync();
        }
    }

}
