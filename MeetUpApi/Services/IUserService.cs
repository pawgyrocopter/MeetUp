using MeetupAPI.DTOs;

namespace MeetupAPI.Services;

public interface IUserService
{
    Task<UserDto> GetUserById(int id);
    Task<IEnumerable<UserDto>> GetUsers();
}