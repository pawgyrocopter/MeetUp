using System.Linq;
using System.Threading.Tasks;
using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Interfaces;
using MeetupAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace MeetupAPI.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IAccountService _accountService;

    public AccountController(ITokenService tokenService,IAccountService accountService)
    {
        _tokenService = tokenService;
        _accountService = accountService;
    }
    
    /// <summary>
    /// Sign up as new user
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await _accountService.IfUserExists(registerDto.UserName))
        {
            return BadRequest("User already exists");
        }
        
        var user = new User()
        {
            UserName = registerDto.UserName,
            Password = registerDto.Password,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        return await _accountService.RegisterUser(registerDto);
    }
    /// <summary>
    /// Sing in as already existed user
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        UserDto userDto;
        try
        {
            userDto = await _accountService.Login(loginDto);
        }
        catch
        {
            return BadRequest("Not correct password");
        }
        return userDto;

    }
}