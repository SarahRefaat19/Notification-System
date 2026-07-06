using System.Linq;
using Microsoft.EntityFrameworkCore;
using NotificationSystem.Domain.Entities;

namespace NotificationSystem.Insfrastructure
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Select(u => new User { Id = u.Id, Name = u.Name }).ToListAsync();
        }

    }
}
