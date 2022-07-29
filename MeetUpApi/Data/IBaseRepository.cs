namespace MeetupAPI.Data;

public class IBaseRepository
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}