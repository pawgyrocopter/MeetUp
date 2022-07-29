using MeetupAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.Include(x => x.Meetups).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IQueryable<User>> GetUsers()
    {
        return _context.Users.Include(x => x.Meetups);
    }
}