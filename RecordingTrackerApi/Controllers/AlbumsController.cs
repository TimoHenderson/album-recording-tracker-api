using System;
using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{
    public class AlbumsController : GenericController<Album, AlbumDTO>
    {
        public AlbumsController(AlbumsService service) : base(service) { }
    }
}

