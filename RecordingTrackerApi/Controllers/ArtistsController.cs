using System;
using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Models;
using RecordingTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using RecordingTrackerApi.Data.Helpers;

namespace RecordingTrackerApi.Controllers
{


    public class ArtistsController : GenericController<Artist>
    {
        public ArtistsController(ArtistsService service) : base(service) { }

    }
}
