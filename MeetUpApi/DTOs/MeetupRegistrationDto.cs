namespace MeetupAPI.DTOs;

public class MeetupRegistrationDto
{
    public string Name { get; set; }
    
    public string PlannedTime { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Place { get; set; }
}