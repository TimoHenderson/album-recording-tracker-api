using RecordingTrackerApi.Models;
using RecordingTrackerApi.Data;
using Microsoft.EntityFrameworkCore;

namespace RecordingTrackerApi.Services;

public class ArtistsService : GenericEntityService<Artist>
{
    public ArtistsService(RecordingContext context) : base(context) { }

    public override async Task<IEnumerable<Artist>> GetAll(string UserId)
    {
        return await _dbSet
        .Include(a => a.Children)
        .ThenInclude(a => a.Children)
        .ThenInclude(s => s.Children)
        .AsNoTracking()
        .ToListAsync();
    }



    public override async Task<Artist?> Get(string UserId, int id)
    {
        var artist = await _dbSet.FindAsync(id);

        if (artist == null)
        {
            return null;
        }
        else
        {
            return await _dbSet
               .Include(a => a.Children)
                .ThenInclude(a => a.Children)
                .ThenInclude(s => s.Children)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }

}