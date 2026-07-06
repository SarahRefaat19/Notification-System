using Microsoft.EntityFrameworkCore;
using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Insfrastructure
{
    public class NotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

    }

}
