namespace MeetupAPI.DTOs;

public class MeetupDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CreationDate { get; set; }
    public ICollection<MemberDto> UsersRegistred { get; set; }
}