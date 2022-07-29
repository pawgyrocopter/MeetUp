using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Data;

public class MeetupRepository : IMeetupRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MeetupRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Meetup> CreateMeetup(Meetup meetup)
    {
        await _context.Meetups.AddAsync(meetup);
        await _context.SaveChangesAsync();
        return meetup;
    }

    public async Task<Meetup> GetMeetupById(int id)
    {
        return await _context.Meetups.Include(x => x.UsersRegistred).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> RegisterUserForMeetup(Meetup meetup, User user)
    {
        meetup.UsersRegistred.Add(user);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<IEnumerable<Meetup>> GetAllMeetups()
    {
        return _context.Meetups;
    }

    public async Task<IQueryable<Meetup>> GetMeetups()
    {
        return _context.Meetups.AsQueryable();
    }
}