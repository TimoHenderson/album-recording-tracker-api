using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Data;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using System.Linq.Expressions;

namespace RecordingTrackerApi.Services;

public class ArtistsService : GenericEntityService<Artist, ArtistDTO>
{
    private static readonly Expression<Func<Artist, ArtistDTO>> _projectionCriteria
    = s => new ArtistDTO
    {
        Id = s.Id,
        Name = s.Name,
    };
    public ArtistsService(RecordingContext context) : base(context, _projectionCriteria) { }

    // public override async Task<IEnumerable<Artist>> GetAll(string UserId)
    // {
    //     return await _dbSet
    //     .Include(a => a.Children)
    //     .ThenInclude(a => a.Children)
    //     .ThenInclude(s => s.Children)
    //     .AsNoTracking()
    //     .ToListAsync();
    // }



    // public override async Task<Artist?> Get(string UserId, int id)
    // {
    //     var artist = await _dbSet.FindAsync(id);

    //     if (artist == null)
    //     {
    //         return null;
    //     }
    //     else
    //     {
    //         return await _dbSet
    //            .Include(a => a.Children)
    //             .ThenInclude(a => a.Children)
    //             .ThenInclude(s => s.Children)
    //             .AsNoTracking()
    //             .SingleOrDefaultAsync(a => a.Id == id);
    //     }
    // }

}