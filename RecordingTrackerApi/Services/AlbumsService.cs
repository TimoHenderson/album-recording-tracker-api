
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models;

namespace RecordingTrackerApi.Services;

public class AlbumsService : GenericEntityService<Album>
{
    public AlbumsService(RecordingContext context) : base(context) { }


    public override async Task<IEnumerable<Album>> GetAll(string userId)
    {
        return await _dbSet
        .Include(a => a.Parent)
        .Include(a => a.Children)
        .ThenInclude(s => s.Children)
        .AsNoTracking()
        .ToListAsync();
    }

    public override async Task<Album?> Get(string userId, int id)
    {
        var artist = await _dbSet.FindAsync(id);

        if (artist == null)
        {
            return null;
        }
        else
        {
            return await _dbSet
                .Include(a => a.Parent)
                .Include(a => a.Children)
                .ThenInclude(s => s.Children)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }

    public override async Task<Album?> Create(string userId, Album album)
    {
        var artist = await _context.Artists.FindAsync(album.ParentNum);

        if (artist == null) return null;
        else album.Parent = artist;
        return await base.Create(userId, album);
    }

}