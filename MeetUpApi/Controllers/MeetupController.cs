
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Helpers;
using MeetupAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MeetupAPI.Controllers;

public class MeetupController : BaseApiController
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMeetupService _meetupService;

    public MeetupController(ApplicationDbContext context, IMapper mapper, IMeetupService meetupService)
    {
        _context = context;
        _mapper = mapper;
        _meetupService = meetupService;
    }

    [HttpGet]
    public async Task<IEnumerable<MeetupDTO>> GetAllMeetups([FromQuery]MeetupsFilterParams meetupsFilterParams) //sort by name, date,
    {
        //return _context.Meetups.Include(x => x.UsersRegistred).ProjectTo<MeetupDTO>(_mapper.ConfigurationProvider);
        return await _meetupService.GetMeetups(meetupsFilterParams);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<MeetupDTO>> GetMeetupById(int id)
    {
        MeetupDTO meetup;
        try
        {
            meetup = await _meetupService.GetMeetupById(id);
        }
        catch
        {
            return NoContent();
        }
        return meetup;
    }

    [HttpPost]
    public async Task<MeetupDTO> CreateMeetup([FromBody] MeetupRegistrationDto meetupRegistrationDto)
    {
        return await _meetupService.CreateMeetup(meetupRegistrationDto);
    }

    [Authorize]
    [HttpPost("{id}/register", Name = "register")]
    public async Task<ActionResult> RegisterForMeetup(int id)
    {
        try
        {
            await _meetupService.RegisterForMeetup(id, int.Parse(HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()?.Value));
        }
        catch
        {
            return BadRequest();
        }

        return Ok("Successfully registered to a meetup");
    }
}