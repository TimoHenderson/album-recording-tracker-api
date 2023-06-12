
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

    private static readonly MapperConfiguration _mapperConfiguration = new(cfg =>
    {
        cfg.CreateMap<PartDTO, Part>()
            .ForMember(dest => dest.Parent, opt =>
            opt.MapFrom(src => new Song { Id = src.ParentId }))
            .ForMember(dest => dest.Instrument, opt =>
            opt.MapFrom(src => new Instrument { Id = src.InstrumentId }));

        cfg.CreateMap<Part, PartDTO>()
            .ForMember(dest => dest.ParentId, opt =>
            opt.MapFrom(src => src.Parent.Id))
             .ForMember(dest => dest.InstrumentId, opt =>
            opt.MapFrom(src => src.Instrument.Id));
    });

    public PartsService(RecordingContext context) : base(context, _projectionCriteria, _mapperConfiguration) { }

    public override async Task<bool> ValidateRelationshipsAndAttach(Part entity)
    {
        y
        if (song == null) return false;
        else entity.Parent = song;

        var instrument = await _context.Instruments.FindAsync(entity.Instrument.Id);
        if (instrument == null) return false;
        else entity.Instrument = instrument;
        return true;

    }

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