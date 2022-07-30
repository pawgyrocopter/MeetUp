using System;
using System.Collections.Generic;

namespace MeetupAPI.Entities;

public class Meetup
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Place { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    public DateTime PlannedTime { get; set; }
    public ICollection<User> UsersRegistred { get; set; }
    public int OwnerId { get; set; }
}