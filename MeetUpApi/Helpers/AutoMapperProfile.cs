using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;

namespace MeetupAPI.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Meetup, MeetupDTO>();
        CreateMap<MeetupDTO, Meetup>();

        CreateMap<User, MemberDto>();
    }
}