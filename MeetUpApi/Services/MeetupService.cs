using System.Diagnostics;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace MeetupAPI.Services;

public class MeetupService : IMeetupService
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public MeetupService(IMeetupRepository meetupRepository, IMapper mapper, IUserRepository userRepository)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<MeetupDTO> GetMeetupById(int id)
    {
        return _mapper.Map<MeetupDTO>(await _meetupRepository.GetMeetupById(id));
    }

    public async Task<MeetupDTO> CreateMeetup(MeetupRegistrationDto meetupRegistrationDto, int ownerId)
    {
        var meetup = new Meetup()
        {
            Name = meetupRegistrationDto.Name,
            CreationDate = DateTime.Now,
            Description = meetupRegistrationDto.Description,
            Keywords = meetupRegistrationDto.Keywords,
            Place = meetupRegistrationDto.Place,
            UsersRegistred = new List<User>(),
            PlannedTime = DateTime.Parse(meetupRegistrationDto.PlannedTime),
            OwnerId = ownerId
        };
        return _mapper.Map<MeetupDTO>( await _meetupRepository.CreateMeetup(meetup));
    }

    public async Task<PagedList<MeetupDTO>> GetMeetups(MeetupsFilterParams meetupsFilterParams)
    {
        var query = await _meetupRepository.GetMeetups();

        query = query.Where(x => x.Name.Contains(meetupsFilterParams.SearchByName));
        
        //order by switch expression start
        query = meetupsFilterParams.OrderBy switch
        {
            "CreationDate" => query.OrderBy(x => x.CreationDate),
            "PlannedDate" => query.OrderBy(x => x.PlannedTime),
            "Name" => query.OrderBy(x => x.Name),
            _ => query
        };
        //order by end
        
        return await PagedList<MeetupDTO>.CreateAsync(
            query.ProjectTo<MeetupDTO>(_mapper.ConfigurationProvider).AsNoTracking(),
            meetupsFilterParams.PageNumber, meetupsFilterParams.PageSize);
    }
    
    public async Task<bool> RegisterForMeetup(int meetupId, int userId)
    {
        var meetup = await _meetupRepository.GetMeetupById(meetupId);
        var user = await _userRepository.GetUserById(userId);
        return await _meetupRepository.RegisterUserForMeetup(meetup, user);
    }

    public async Task<MeetupDTO> UpdateMeetup(int meetupId, MeetupRegistrationDto meetupDto, int userId )
    {
      var meetup = await _meetupRepository.GetMeetupById(meetupId);
      if (meetup.OwnerId != userId)
      {
          throw new UnauthorizedAccessException("You are not an owner");
      }
      try
      {
          meetup.Description = meetupDto.Description;
          meetup.Keywords = meetupDto.Keywords;
          meetup.Name = meetupDto.Name;
          meetup.PlannedTime = DateTime.Parse(meetupDto.PlannedTime);
          await _meetupRepository.UpdateMeetup(meetup);
      }
      catch
      {
          throw new DbUpdateException("Couldn't update meetup");
      }

      return _mapper.Map<MeetupDTO>(meetup);
    }

    public async Task<bool> DeleteMeetup(int meetupId, int userId )
    {
        var meetup = await _meetupRepository.GetMeetupById(meetupId);
        bool result = false;
        if (meetup.OwnerId != userId)
        {
            throw new UnauthorizedAccessException("You are not an owner");
        }
        try
        {
            result =  await _meetupRepository.DeleteMeetup(meetupId);
        }
        catch
        {
            throw new DbUpdateException("Couldn't update meetup");
        }

        return result;
    }
    
    
}