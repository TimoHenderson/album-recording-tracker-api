using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{
    public class SongsController : GenericController<Song, SongDTO>
    {
        public SongsController(SongsService service) : base(service) { }
    }
}

