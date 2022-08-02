using MeetupAPI.DTOs;

namespace MeetupAPI.Services;

public interface IAccountService
{
    Task<bool> IfUserExists(string userName);
    Task<UserDto> RegisterUser(RegisterDto RegisterDto);
    Task<UserDto> Login(LoginDto loginDto);
}