using MeetupAPI.Entities;

namespace MeetupAPI.Data;

public interface IUserRepository
{
    Task<User> GetUserById(int id);
    Task<IQueryable<User>> GetUsers();
}