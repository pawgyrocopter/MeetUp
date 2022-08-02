using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Interfaces;

namespace MeetupAPI.Services;

public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AccountService(ITokenService tokenService,IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<bool> IfUserExists(string userName)
    {
        return (await _userRepository.GetUsers()).Any(x => x.UserName == userName);
    }

    public async Task<UserDto> RegisterUser(RegisterDto registerDto)
    {
        var user = new User()
        {
            UserName = registerDto.UserName,
            Password = registerDto.Password,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        await _userRepository.CreateUser(user);
        return new UserDto()
        {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByUserName(loginDto.UserName);
        if (!user.Password.Equals(loginDto.Password)) throw new UnauthorizedAccessException();
        return new UserDto()
        {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user),
        };
    }
}