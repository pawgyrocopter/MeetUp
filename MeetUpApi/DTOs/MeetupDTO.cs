namespace MeetupAPI.DTOs;

public class MeetupDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Place { get; set; }
    
    public string CreationDate { get; set; }
    
    public string PlannedTime { get; set; }
    
    public ICollection<MemberDto> UsersRegistred { get; set; }
}