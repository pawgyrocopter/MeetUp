using System.Security.Claims;
using AutoMapper;
using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Helpers;
using MeetupAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Controllers;

[Route("/api/Meetups")]
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

    /// <summary>
    /// Return all meetups
    /// </summary>
    /// <param name="OrderBy">Take it easy</param>
    [HttpGet]
    public async Task<IEnumerable<MeetupDTO>> GetAllMeetups([FromQuery]MeetupsFilterParams meetupsFilterParams) //sort by name, date,
    {
        return await _meetupService.GetMeetups(meetupsFilterParams);
    }
    /// <summary>
    /// Returns meetup by id
    /// </summary>
    /// <param name="meetupId">Id of existing meetup</param>
    /// <returns></returns>
    /// <response code="200">Successfully returned meetup</response>
    /// <response code="204">No such a meetup</response>
    [HttpGet("{meetupId}")]
    public async Task<ActionResult<MeetupDTO>> GetMeetupById(int meetupId)
    {
        MeetupDTO meetup;
        try
        {
            meetup = await _meetupService.GetMeetupById(meetupId);
        }
        catch
        {
            return NoContent();
        }
        return meetup;
    }

    /// <summary>
    /// Creates meetup
    /// </summary>
    /// <param name="meetupRegistrationDto.OrderBy">qweqwe</param>
    /// <remarks>
    ///     Sample Request:
    ///
    ///         POST /api/Meetup
    ///         {
    ///               "name": "test",
    ///               "plannedTime": "12/12/2022",
    ///               "description": "some test",
    ///               "keywords": "qwe qwe test qwe",
    ///               "place": "Belarus"
    ///         }
    ///     
    /// </remarks>
    [Authorize]
    [HttpPost]
    public async Task<MeetupDTO> CreateMeetup([FromBody] MeetupRegistrationDto meetupRegistrationDto)
    {
        return await _meetupService.CreateMeetup(meetupRegistrationDto, GetUserId());
    }

    /// <summary>
    /// Register user for the meetup
    /// </summary>
    /// <param name="meetupId">Id of existing meetup</param>
    /// <response code="200">Successfully registered for the meetup</response>
    /// <response code="400">Incorrect id</response>
    /// <response code="401">Unauthorized access</response>
    [Authorize]
    [HttpPost("{meetupId}/register", Name = "register")]
    public async Task<ActionResult> RegisterForMeetup(int meetupId)
    {
        try
        {
            await _meetupService.RegisterForMeetup(meetupId, GetUserId());
        }
        catch
        {
            return BadRequest();
        }

        return Ok("Successfully registered to a meetup");
    }

    /// <summary>
    /// Updates existing meetup
    /// </summary>
    /// <param name="meetupId">Id of existing meetup</param>
    /// <param name="meetupRegistrationDto"></param>
    /// <remarks>
    ///     Sample Request:
    ///
    ///         PUT /api/Meetup/{meetupId : int}
    ///         {
    ///               "name": "test",
    ///               "plannedTime": "12/12/2022",
    ///               "description": "some test",
    ///               "keywords": "qwe qwe test qwe",
    ///               "place": "Belarus"
    ///         }
    ///     
    /// </remarks>
    /// <returns></returns>
    /// <response code="200">Successfully updated the meetup</response>
    /// <response code="400">Incorrect id</response>
    /// <response code="401">Unauthorized access</response>
    [Authorize]
    [HttpPut("{meetupId}")]
    public async Task<ActionResult<MeetupDTO>> UpdateMeetup(int meetupId,[FromBody]MeetupRegistrationDto meetupRegistrationDto)
    {
        MeetupDTO meetupDto;
        try
        {
            meetupDto = await _meetupService.UpdateMeetup(meetupId, meetupRegistrationDto, GetUserId());
        }
        catch (DbUpdateException dbUpdateException)
        {
            return BadRequest("Couldn't update meetup");
        }
        catch
        {
            return Unauthorized("You are not the owner of meetup");
        }

        return meetupDto;
    }

    /// <summary>
    /// Deletes existing meetup
    /// </summary>
    /// <param name="meetupId">Id of existing meetup</param>
    /// <returns></returns>
    /// <response code="200">Successfully deleted the meetup</response>
    /// <response code="400">Incorrect id</response>
    /// <response code="401">Unauthorized access</response>
    [Authorize]
    [HttpDelete("{meetupId}")]
    public async Task<ActionResult> DeleteMeetup(int meetupId)
    {
        try
        {
            await _meetupService.DeleteMeetup(meetupId, GetUserId());
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized("You are not the owner");
        }
        catch
        {
            return BadRequest("Couldn't delete meetup");
        }

        return Ok();
    }
    [NonAction]
    public int GetUserId()
    {
        return int.Parse(HttpContext.User.Claims
            .Where(x => x.Type == ClaimTypes.NameIdentifier)
            .FirstOrDefault()?.Value);
    }
}