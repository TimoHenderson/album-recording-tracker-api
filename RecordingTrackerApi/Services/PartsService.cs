
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models;

namespace RecordingTrackerApi.Services;

public class PartsService : GenericEntityService<Part>
{
    public PartsService(RecordingContext context) : base(context) { }


    public override async Task<IEnumerable<Part>> GetAll(string userId)
    {
        return await _dbSet
        .Include(s => s.Parent)
        .Include(s => s.Instrument)
        .AsNoTracking()
        .ToListAsync();
    }



    public override async Task<Part?> Get(string userId, int id)
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
                .Include(s => s.Instrument)
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }
    }

    public override async Task<Part?> Create(string userId, Part part)
    {
        var song = await _context.Songs.FindAsync(part.ParentNum);
        var instrument = await _context.Instruments.FindAsync(part.InstrumentNum);

        if (song == null || instrument == null) return null;
        else
        {
            part.Parent = song;
            part.Instrument = instrument;
        }
        return await base.Create(userId, part);
    }

}