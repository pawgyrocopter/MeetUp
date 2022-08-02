using MeetupAPI.Data;
using MeetupAPI.Helpers;
using MeetupAPI.Interfaces;
using MeetupAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connStr); });
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMeetupRepository, MeetupRepository>();
        services.AddScoped<IMeetupService, MeetupService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}