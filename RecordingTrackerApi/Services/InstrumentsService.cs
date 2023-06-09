
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Services;

public class InstrumentsService : GenericEntityService<Instrument, InstrumentDTO>
{
    private static readonly Expression<Func<Instrument, InstrumentDTO>> _projectionCriteria
    = s => new InstrumentDTO
    {
        Id = s.Id,
        Name = s.Name,
    };

    private static readonly MapperConfiguration _mapperConfiguration = new(cfg =>
        {
            cfg.CreateMap<InstrumentDTO, Instrument>();
            cfg.CreateMap<Instrument, InstrumentDTO>();
        });

    public InstrumentsService(RecordingContext context) : base(context, _projectionCriteria, _mapperConfiguration) { }


}