using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{
    public class PartsController : GenericController<Part, PartDTO>
    {
        public PartsController(PartsService service) : base(service) { }
    }
}

