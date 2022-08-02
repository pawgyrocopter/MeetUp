using API.Helpers;
using MeetupAPI.DTOs;
using MeetupAPI.Helpers;

namespace MeetupAPI.Services;

public interface IMeetupService
{
    Task<MeetupDTO> GetMeetupById(int id);
    Task<MeetupDTO> CreateMeetup(MeetupRegistrationDto meetupRegistrationDto, int ownerId);

    Task<PagedList<MeetupDTO>> GetMeetups(MeetupsFilterParams meetupsFilterParams);

    Task<bool> RegisterForMeetup(int meetupId, int userId);

    Task<MeetupDTO> UpdateMeetup(int meetupId, MeetupRegistrationDto meetupRegistrationDto, int userId);
    Task<bool> DeleteMeetup(int meetupId, int userId);
}