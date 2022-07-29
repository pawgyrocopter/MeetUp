using AutoMapper;
using MeetupAPI.Data;
using MeetupAPI.DTOs;

namespace MeetupAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public async Task<UserDto> GetUserById(int id)
    {
        return _mapper.Map<UserDto>(await _userRepository.GetUserById(id));
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        return _mapper.ProjectTo<UserDto>(await _userRepository.GetUsers());
    }
}