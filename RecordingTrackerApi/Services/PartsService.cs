
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Services;

public class PartsService : GenericEntityService<Part, PartDTO>
{
    private static readonly Expression<Func<Part, PartDTO>> _projectionCriteria
    = s => new PartDTO
    {
        Id = s.Id,
        Name = s.Name,
        ParentId = s.Parent.Id,
        InstrumentId = s.Instrument.Id,
        Completion = s.Completion,
    };

    public PartsService(RecordingContext context) : base(context, _projectionCriteria) { }


    // public override async Task<IEnumerable<Part>> GetAll(string userId)
    // {
    //     return await _dbSet
    //     .Include(s => s.Parent)
    //     .Include(s => s.Instrument)
    //     .AsNoTracking()
    //     .ToListAsync();
    // }



    // public override async Task<Part?> Get(string userId, int id)
    // {
    //     var artist = await _dbSet.FindAsync(id);

    //     if (artist == null)
    //     {
    //         return null;
    //     }
    //     else
    //     {
    //         return await _dbSet
    //             .Include(s => s.Parent)
    //             .Include(s => s.Instrument)
    //             .AsNoTracking()
    //             .SingleOrDefaultAsync(a => a.Id == id);
    //     }
    // }

    // public override async Task<Part?> Create(string userId, Part part)
    // {
    //     var song = await _context.Songs.FindAsync(part.ParentNum);
    //     var instrument = await _context.Instruments.FindAsync(part.InstrumentNum);

    //     if (song == null || instrument == null) return null;
    //     else
    //     {
    //         part.Parent = song;
    //         part.Instrument = instrument;
    //     }
    //     return await base.Create(userId, part);
    // }

}