using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MeetupAPI.Data;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Services;

public class MeetupService : IMeetupService
{
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;

    public MeetupService(IMeetupRepository meetupRepository, IMapper mapper)
    {
        _meetupRepository = meetupRepository;
        _mapper = mapper;
    }

    public async Task<MeetupDTO> GetMeetupById(int id)
    {
        return _mapper.Map<MeetupDTO>(await _meetupRepository.GetMeetupById(id));
    }

    public async Task<MeetupDTO> CreateMeetup(MeetupRegistrationDto meetupRegistrationDto)
    {
        var meetup = new Meetup()
        {
            Name = meetupRegistrationDto.Name,
            CreationDate = DateTime.Now,
            UsersRegistred = new List<User>(),
            PlannedTime = DateTime.Parse(meetupRegistrationDto.PlannedTime)
        };
        return _mapper.Map<MeetupDTO>( await _meetupRepository.CreateMeetup(meetup));
    }

    public async Task<PagedList<MeetupDTO>> GetMeetups(MeetupsFilterParams meetupsFilterParams)
    {
        var query = await _meetupRepository.GetMeetups();

        query = query.Where(x => x.Name.Contains(meetupsFilterParams.SearchByName));
        
        //order by switch expression start
        

        //order by end
        
        return await PagedList<MeetupDTO>.CreateAsync(
            query.ProjectTo<MeetupDTO>(_mapper.ConfigurationProvider).AsNoTracking(),
            meetupsFilterParams.PageNumber, meetupsFilterParams.PageSize);
    }
}