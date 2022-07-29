using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetupAPI.Entities;

namespace MeetupAPI.Data;

public interface IMeetupRepository
{
    Task<IEnumerable<Meetup>> GetAllMeetups();
    Task<Meetup> CreateMeetup(Meetup meetup);
    Task<Meetup> GetMeetupById(int id);

    Task<IQueryable<Meetup>> GetMeetups();
    Task<bool> Complete();
}
