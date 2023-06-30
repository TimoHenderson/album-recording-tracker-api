
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApi.Services;

public class AlbumsService : GenericEntityService<Album, AlbumDTO>
{
    public AlbumsService(RecordingContext context) : base(context) { }

    public override async Task<ValidateRelationshipsResult> ValidateRelationships(string userId, AlbumDTO albumDTO)
    {
        var artist = await _context.Artists.FindAsync(albumDTO.ArtistId);
        if (artist == null) return ValidateRelationshipsResult.Invalid("Artist not found");
        if (artist.AspNetUserId != userId) return ValidateRelationshipsResult.Invalid("Artist not owned by user");
        else return ValidateRelationshipsResult.Valid();
    }

}