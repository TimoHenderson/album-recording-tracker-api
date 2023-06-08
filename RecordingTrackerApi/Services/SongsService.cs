
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models;

namespace RecordingTrackerApi.Services;

public class SongsService : GenericEntityService<Song>
{
    public SongsService(RecordingContext context) : base(context) { }

    public override async Task<IEnumerable<Song>> GetAll(string userId)
    {
        return await _dbSet
        .Include(s => s.Parent)
        .Include(s => s.Children)
        .AsNoTracking()
        .ToListAsync();
    }

    public override async Task<Song?> Get(string userId, int id)
    {
        var artist = await _dbSet.FindAsync(id);

        if (artist == null)
        {
            return null;
        }
        else
        {
            return await _dbSet
                .Include(s => s.Parent)
                .Include(s => s.Children)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }

    public override async Task<Song?> Create(string userId, Song song)
    {
        var album = await _context.Albums.FindAsync(song.ParentNum);

        if (album == null) return null;
        else song.Parent = album;
        return await base.Create(userId, song);
    }

}