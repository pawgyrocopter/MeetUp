namespace MeetupAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    // public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Meetup> Meetups { get; set; }
}