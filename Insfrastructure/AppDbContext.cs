using Microsoft.EntityFrameworkCore;
using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Insfrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }
    }
}
