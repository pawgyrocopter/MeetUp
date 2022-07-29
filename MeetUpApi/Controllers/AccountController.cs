using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace MeetupAPI.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public AccountController(ITokenService tokenService, ApplicationDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new User()
        {
            UserName = registerDto.UserName,
            Password = registerDto.Password
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return new UserDto()
        {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(loginDto.UserName));
        if (!user.Password.Equals(loginDto.Password)) return Unauthorized();
        return new UserDto()
        {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user),
        };
    }
}