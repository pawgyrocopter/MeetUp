﻿using System;
using System.Collections.Generic;

namespace MeetupAPI.Entities;

public class Meetup
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    public DateTime PlannedTime { get; set; }
    
    public ICollection<User> UsersRegistred { get; set; }
}