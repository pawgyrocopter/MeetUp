
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
        if (id > _context.Meetups.Count()) return NoContent();
        return await _meetupService.GetMeetupById(id);
    }

    [HttpPost]
    public async Task<MeetupDTO> CreateMeetup([FromBody] MeetupRegistrationDto meetupRegistrationDto)
    {
        return await _meetupService.CreateMeetup(meetupRegistrationDto);
    }

    [HttpPost("{id}/register", Name = "register")]
    public async Task<MeetupDTO> RegisterForMeetup(int id)
    {
        var meetup = _context.Meetups.Include(x => x.UsersRegistred).FirstOrDefault(x => x.Id == id);
        var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(HttpContext.User.Identity.Name));
        meetup.UsersRegistred.Add(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<MeetupDTO>(meetup);
    }
}