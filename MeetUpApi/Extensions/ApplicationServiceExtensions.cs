using MeetupAPI.Data;
using MeetupAPI.Helpers;
using MeetupAPI.Interfaces;
using MeetupAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMeetupRepository, MeetupRepository>();
        services.AddScoped<IMeetupService, MeetupService>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}