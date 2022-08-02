using API.Helpers;

namespace MeetupAPI.Helpers;

public class MeetupsFilterParams : PaginationParams
{
    public string OrderBy { get; set; } = String.Empty;
    public string SearchByName { get; set; } = String.Empty;
}