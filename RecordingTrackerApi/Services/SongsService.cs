
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Services;

public class SongsService : GenericEntityService<Song, SongDTO>
{
    // private static readonly Expression<Func<Song, SongDTO>> _projectionCriteria
    // = s => new SongDTO
    // {
    //     Id = s.Id,
    //     Name = s.Name,
    //     ParentId = s.Parent.Id,
    // };

    // private static readonly MapperConfiguration _mapperConfiguration = new(cfg =>
    //     {
    //         cfg.CreateMap<SongDTO, Song>()
    //             .ForMember(dest => dest.Parent, opt =>
    //             opt.MapFrom(src => new Album { Id = src.ParentId }));

    //         cfg.CreateMap<Song, SongDTO>()
    //             .ForMember(dest => dest.ParentId, opt =>
    //             opt.MapFrom(src => src.Parent.Id));
    //     });

    public SongsService(RecordingContext context) : base(context) { }
    // public SongsService(RecordingContext context) : base(context, _projectionCriteria, _mapperConfiguration) { }

    // public override async Task<bool> ValidateRelationshipsAndAttach(Song entity)
    // {
    //     var album = await _context.Albums.FindAsync(entity.Parent.Id);
    //     if (album == null) return false;
    //     else entity.Parent = album;
    //     return true;
    // }

    // public override async Task<IEnumerable<Song>> GetAll(string userId)
    // {
    //     return await _dbSet
    //     .Include(s => s.Parent)
    //     .Include(s => s.Children)
    //     .AsNoTracking()
    //     .ToListAsync();
    // }

    // public override async Task<Song?> Get(string userId, int id)
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
    //             .Include(s => s.Children)
    //             .AsNoTracking()
    //             .SingleOrDefaultAsync(a => a.Id == id);
    //     }
    // }

    // public override async Task<Song?> Create(string userId, Song song)
    // {
    //     var album = await _context.Albums.FindAsync(song.ParentNum);

    //     if (album == null) return null;
    //     else song.Parent = album;
    //     return await base.Create(userId, song);
    // }

}