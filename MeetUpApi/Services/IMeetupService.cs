using System.Threading.Tasks;
using API.Helpers;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Helpers;

namespace MeetupAPI.Services;

public interface IMeetupService
{
    Task<MeetupDTO> GetMeetupById(int id);
    Task<MeetupDTO> CreateMeetup(MeetupRegistrationDto meetupRegistrationDto);

    Task<PagedList<MeetupDTO>> GetMeetups(MeetupsFilterParams meetupsFilterParams);
}