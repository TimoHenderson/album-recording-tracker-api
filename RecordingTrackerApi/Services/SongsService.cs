
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApi.Services;

public class SongsService : GenericEntityService<Song, SongDTO>
{

    public SongsService(RecordingContext context) : base(context) { }

    public override async Task<ValidateRelationshipsResult> ValidateRelationships(string userId, SongDTO dto)
    {
        var parent = await _context.Artists.FindAsync(dto.AlbumId);
        if (parent == null) return ValidateRelationshipsResult.Invalid("Album not found");
        if (parent.AspNetUserId != userId) return ValidateRelationshipsResult.Invalid("Album not owned by user");
        else return ValidateRelationshipsResult.Valid();
    }

}