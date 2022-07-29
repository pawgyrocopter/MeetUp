using System.Threading.Tasks;
using MeetupAPI.Entities;

namespace MeetupAPI.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);

}