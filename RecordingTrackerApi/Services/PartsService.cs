
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApi.Services;

public class PartsService : GenericEntityService<Part, PartDTO>
{

    public PartsService(RecordingContext context) : base(context) { }

    public override async Task<ValidateRelationshipsResult> ValidateRelationships(string userId, PartDTO dto)
    {
        var parent = await _context.Artists.FindAsync(dto.SongId);
        if (parent == null) return ValidateRelationshipsResult.Invalid("Song not found");
        if (parent.AspNetUserId != userId) return ValidateRelationshipsResult.Invalid("Song not owned by user");
        else return ValidateRelationshipsResult.Valid();
    }


}