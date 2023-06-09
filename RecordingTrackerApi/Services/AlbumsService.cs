
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Services;

public class AlbumsService : GenericEntityService<Album, AlbumDTO>
{
    private static readonly Expression<Func<Album, AlbumDTO>> _projectionCriteria
    = s => new AlbumDTO
    {
        Id = s.Id,
        Name = s.Name,
        ParentId = s.Parent.Id,
    };
    private static readonly MapperConfiguration _mapperConfiguration = new(cfg =>
    {
        cfg.CreateMap<AlbumDTO, Album>()
            .ForMember(dest => dest.Parent, opt =>
            opt.MapFrom(src => new Artist { Id = src.ParentId }));

        cfg.CreateMap<Album, AlbumDTO>()
            .ForMember(dest => dest.ParentId, opt =>
            opt.MapFrom(src => src.Parent.Id));
    });

    public AlbumsService(RecordingContext context) : base(context, _projectionCriteria, _mapperConfiguration) { }

    public override async Task<bool> ValidateRelationshipsAndAttach(Album entity)
    {
        var artist = await _context.Artists.FindAsync(entity.Parent.Id);
        if (artist == null) return false;
        else entity.Parent = artist;
        return true;
    }

    // public override async Task<IEnumerable<Album>> GetAll(string userId)
    // {
    //     return await _dbSet
    //     .Include(a => a.Parent)
    //     .Include(a => a.Children)
    //     .ThenInclude(s => s.Children)
    //     .AsNoTracking()
    //     .ToListAsync();
    // }

    // public override async Task<Album?> Get(string userId, int id)
    // {
    //     var artist = await _dbSet.FindAsync(id);

    //     if (artist == null)
    //     {
    //         return null;
    //     }
    //     else
    //     {
    //         return await _dbSet
    //             .Include(a => a.Parent)
    //             .Include(a => a.Children)
    //             .ThenInclude(s => s.Children)
    //             .AsNoTracking()
    //             .SingleOrDefaultAsync(a => a.Id == id);
    //     }
    // }

    // public override async Task<AlbumDTO?> Create(string userId, AlbumDTO album)
    // {
    //     var artist = await _context.Artists.FindAsync(album.ParentNum);

    //     if (artist == null) return null;
    //     else album.Parent = artist;
    //     return await base.Create(userId, album);
    // }

}